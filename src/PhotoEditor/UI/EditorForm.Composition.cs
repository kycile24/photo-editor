using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PhotoEditor.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private void AttachTaskPreviewEvents()
    {
        comboMergeFirst.SelectedIndexChanged += (s, e) => UpdateMergePreview();
        comboMergeSecond.SelectedIndexChanged += (s, e) => UpdateMergePreview();
        radioMergeHorizontal.CheckedChanged += (s, e) => UpdateMergePreview();
        radioMergeVertical.CheckedChanged += (s, e) => UpdateMergePreview();

        txtWatermarkText.TextChanged += (s, e) => UpdateWatermarkPreview();
        comboWatermarkPosition.SelectedIndexChanged += (s, e) => UpdateWatermarkPreview();
        numWatermarkSize.ValueChanged += (s, e) => UpdateWatermarkPreview();
        trkWatermarkOpacity.ValueChanged += (s, e) => UpdateWatermarkPreview();
        chkWatermarkBold.CheckedChanged += (s, e) => UpdateWatermarkPreview();
        chkWatermarkItalic.CheckedChanged += (s, e) => UpdateWatermarkPreview();
        chkWatermarkCoordinates.CheckedChanged += (s, e) => UpdateWatermarkPreview();
        numWatermarkX.ValueChanged += (s, e) => UpdateWatermarkPreview();
        numWatermarkY.ValueChanged += (s, e) => UpdateWatermarkPreview();

        numSigma.ValueChanged += (s, e) => UpdateSigmaFilterPreview();
    }

    private void UpdateTaskPreviews()
    {
        UpdateLivePreview();
    }

    private void SetTaskPreview(PictureBox box, Bitmap bitmap)
    {
        if (box == null) return;
        if (box.Image != null) box.Image.Dispose();
        box.Image = bitmap;
    }

    private void ClearTaskPreview(PictureBox box)
    {
        if (box == null) return;
        if (box.Image != null)
        {
            box.Image.Dispose();
            box.Image = null;
            UpdateComparisonAvailability();
        }
    }

    private void UpdateMergePreview()
    {
        if (!IsActionSelected(grpMerge)) return;

        if (picturePreview == null ||
            comboMergeFirst.SelectedIndex < 0 || comboMergeSecond.SelectedIndex < 0 ||
            comboMergeFirst.SelectedIndex >= selectedFiles.Count ||
            comboMergeSecond.SelectedIndex >= selectedFiles.Count ||
            comboMergeFirst.SelectedIndex == comboMergeSecond.SelectedIndex)
        {
            ClearTaskPreview(picturePreview);
            return;
        }

        try
        {
            Bitmap first = null;
            Bitmap second = null;

            if (comboMergeFirst.SelectedIndex == listSelectFile.SelectedIndex &&
                previewBaseBitmap != null)
            {
                first = BitmapOperations.Ensure32bpp(previewBaseBitmap);
            }
            else
            {
                using (Image firstImg = Image.FromFile(selectedFiles[comboMergeFirst.SelectedIndex]))
                    first = BitmapOperations.Ensure32bpp(firstImg);
            }

            using (first)
            using (Image secondImg = Image.FromFile(selectedFiles[comboMergeSecond.SelectedIndex]))
            {
                second = BitmapOperations.Ensure32bpp(secondImg);
                using (second)
                {
                    bool horizontal = radioMergeHorizontal.Checked;
                    int targetWidth = first.Width;
                    int targetHeight = first.Height;

                    using (Bitmap secondFit = BitmapOperations.ResizeBitmap(second, targetWidth, targetHeight))
                    {
                        int width = horizontal ? targetWidth * 2 : targetWidth;
                        int height = horizontal ? targetHeight : targetHeight * 2;
                        Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                        using (Graphics g = Graphics.FromImage(result))
                        {
                            g.Clear(Color.White);
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            if (horizontal)
                            {
                                g.DrawImage(first, 0, 0, targetWidth, targetHeight);
                                g.DrawImage(secondFit, targetWidth, 0, targetWidth, targetHeight);
                            }
                            else
                            {
                                g.DrawImage(first, 0, 0, targetWidth, targetHeight);
                                g.DrawImage(secondFit, 0, targetHeight, targetWidth, targetHeight);
                            }
                        }

                        SetPreviewImage(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClearTaskPreview(picturePreview);
            Log($"Preview об'єднання: {ex.Message}");
        }
    }

    private Bitmap CreateWatermarkPreviewBitmap(string filePath)
    {
        Bitmap source = previewBaseBitmap != null
            ? BitmapOperations.Ensure32bpp(previewBaseBitmap)
            : null;

        if (source == null)
        {
            using (Image img = Image.FromFile(filePath))
                source = BitmapOperations.Ensure32bpp(img);
        }

        Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
        FontStyle style = FontStyle.Regular;
        if (chkWatermarkBold.Checked) style |= FontStyle.Bold;
        if (chkWatermarkItalic.Checked) style |= FontStyle.Italic;

        using (source)
        using (Graphics g = Graphics.FromImage(result))
        using (Font font = new Font("Segoe UI", (float)numWatermarkSize.Value, style, GraphicsUnit.Point))
        using (Brush brush = new SolidBrush(Color.FromArgb((int)(trkWatermarkOpacity.Value * 255 / 100), watermarkColor)))
        {
            g.DrawImage(source, 0, 0, source.Width, source.Height);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            string text = txtWatermarkText.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                SizeF size = g.MeasureString(text, font);
                PointF point;
                if (chkWatermarkCoordinates.Checked)
                {

                    point = new PointF((float)numWatermarkX.Value, (float)numWatermarkY.Value);
                }
                else
                {
                    point = GetWatermarkPosition(size, result.Width, result.Height,
                        comboWatermarkPosition.SelectedItem?.ToString() ?? "Верх ліворуч", 15f);
                }

                float maxX = Math.Max(0, result.Width - size.Width);
                float maxY = Math.Max(0, result.Height - size.Height);
                point = new PointF(
                    Math.Max(0, Math.Min(point.X, maxX)),
                    Math.Max(0, Math.Min(point.Y, maxY)));
                g.DrawString(text, font, brush, point);
            }
        }

        return result;
    }

    private void UpdateWatermarkCoordinateLimits()
    {
        int width = previewBaseBitmap?.Width ?? originalBitmapForPreview?.Width ?? 1;
        int height = previewBaseBitmap?.Height ?? originalBitmapForPreview?.Height ?? 1;

        numWatermarkX.Maximum = Math.Max(0, width - 1);
        numWatermarkY.Maximum = Math.Max(0, height - 1);
    }

    private void UpdateWatermarkPreview()
    {
        if (!IsActionSelected(grpWatermark)) return;

        if (picturePreview == null || selectedFiles.Count == 0)
        {
            ClearTaskPreview(picturePreview);
            return;
        }
        try
        {
            int index = listSelectFile.SelectedIndex >= 0 ? listSelectFile.SelectedIndex : 0;
            UpdateWatermarkCoordinateLimits();
            SetPreviewImage(CreateWatermarkPreviewBitmap(selectedFiles[index]));
        }
        catch (Exception ex)
        {
            ClearTaskPreview(picturePreview);
            Log($"Preview водяного знаку: {ex.Message}");
        }
    }

    private void UpdateSlideshowPreviewFromSelectedFile()
    {
        if (!IsActionSelected(grpSlideshow)) return;

        if (picturePreview == null || selectedFiles.Count == 0)
        {
            ClearTaskPreview(picturePreview);
            return;
        }
        try
        {
            int index = listSelectFile.SelectedIndex >= 0 ? listSelectFile.SelectedIndex : 0;

            if (index == listSelectFile.SelectedIndex && previewBaseBitmap != null)
            {
                SetPreviewImage(BitmapOperations.Ensure32bpp(previewBaseBitmap));
                return;
            }

            using (Image img = Image.FromFile(selectedFiles[index]))
                SetPreviewImage(BitmapOperations.Ensure32bpp(img));
        }
        catch (Exception ex)
        {
            ClearTaskPreview(picturePreview);
            Log($"Preview слайд-шоу: {ex.Message}");
        }
    }

    private void UpdateMergeImageSelectors()
    {
        if (comboMergeFirst == null || comboMergeSecond == null)
            return;

        comboMergeFirst.Items.Clear();
        comboMergeSecond.Items.Clear();

        foreach (string file in selectedFiles)
        {
            comboMergeFirst.Items.Add(Path.GetFileName(file));
            comboMergeSecond.Items.Add(Path.GetFileName(file));
        }

        if (comboMergeFirst.Items.Count > 0)
            comboMergeFirst.SelectedIndex = 0;

        if (comboMergeSecond.Items.Count > 1)
            comboMergeSecond.SelectedIndex = 1;
        else if (comboMergeSecond.Items.Count > 0)
            comboMergeSecond.SelectedIndex = 0;
    }

    private void buttonMergeImages_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpMerge)) return;

        if (selectedFiles.Count < 2)
        {
            MessageBox.Show(
                "Оберіть щонайменше два зображення!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (comboMergeFirst.SelectedIndex < 0 || comboMergeSecond.SelectedIndex < 0)
        {
            MessageBox.Show(
                "Оберіть два зображення для об'єднання!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string firstPath = selectedFiles[comboMergeFirst.SelectedIndex];
        string secondPath = selectedFiles[comboMergeSecond.SelectedIndex];

        if (string.Equals(firstPath, secondPath, StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(
                "Перше і друге зображення мають бути різними!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        bool horizontal = radioMergeHorizontal.Checked;

        try
        {
            Bitmap first = null;
            if (comboMergeFirst.SelectedIndex == listSelectFile.SelectedIndex && previewBaseBitmap != null)
            {
                first = BitmapOperations.Ensure32bpp(previewBaseBitmap);
            }
            else
            {
                using (Image firstImg = Image.FromFile(firstPath))
                    first = BitmapOperations.Ensure32bpp(firstImg);
            }

            using (first)
            using (Image secondImg = Image.FromFile(secondPath))
            using (Bitmap second = BitmapOperations.Ensure32bpp(secondImg))
            {

                int targetWidth = first.Width;
                int targetHeight = first.Height;
                int resultWidth = horizontal ? targetWidth * 2 : targetWidth;
                int resultHeight = horizontal ? targetHeight : targetHeight * 2;

                ImageFormat fmt = GetOriginalFormat(firstPath);
                PixelFormat pixelFormat = PixelFormat.Format32bppArgb;

                using (Bitmap result = new Bitmap(resultWidth, resultHeight, pixelFormat))
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.Clear(fmt == ImageFormat.Png ? Color.Transparent : Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.DrawImage(first, 0, 0, targetWidth, targetHeight);
                    if (horizontal)
                        g.DrawImage(second, targetWidth, 0, targetWidth, targetHeight);
                    else
                        g.DrawImage(second, 0, targetHeight, targetWidth, targetHeight);

                    string direction = horizontal ? "horizontal" : "vertical";
                    string fileName =
                        Path.GetFileNameWithoutExtension(firstPath) +
                        "_merged_" + direction +
                        Path.GetExtension(firstPath);

                    string savePath = Path.Combine(outputDir, fileName);
                    result.Save(savePath, fmt);

                    Log($"Об'єднано: {Path.GetFileName(firstPath)} + {Path.GetFileName(secondPath)} " +
                        $"({(horizontal ? "горизонтально" : "вертикально")}) -> {fileName}");
                }
            }

            MessageBox.Show(
                "Зображення успішно об'єднано!",
                "Готово",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            Log($"Помилка об'єднання зображень: {ex.Message}");
            MessageBox.Show(
                $"Не вдалося об'єднати зображення:\n{ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnWatermarkColor_Click(object sender, EventArgs e)
    {
        using (ColorDialog cd = new ColorDialog())
        {
            cd.Color = watermarkColor;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                watermarkColor = cd.Color;
                pnlWatermarkColor.BackColor = watermarkColor;
                UpdateWatermarkPreview();
            }
        }
    }

    private PointF GetWatermarkPosition(
        SizeF textSize,
        int imageWidth,
        int imageHeight,
        string position,
        float margin)
    {
        float x;
        float y;

        switch (position)
        {
            case "Верх по центру":
                x = (imageWidth - textSize.Width) / 2f;
                y = margin;
                break;

            case "Верх праворуч":
                x = imageWidth - textSize.Width - margin;
                y = margin;
                break;

            case "По центру ліворуч":
                x = margin;
                y = (imageHeight - textSize.Height) / 2f;
                break;

            case "По центру":
                x = (imageWidth - textSize.Width) / 2f;
                y = (imageHeight - textSize.Height) / 2f;
                break;

            case "По центру праворуч":
                x = imageWidth - textSize.Width - margin;
                y = (imageHeight - textSize.Height) / 2f;
                break;

            case "Внизу ліворуч":
                x = margin;
                y = imageHeight - textSize.Height - margin;
                break;

            case "Внизу по центру":
                x = (imageWidth - textSize.Width) / 2f;
                y = imageHeight - textSize.Height - margin;
                break;

            case "Внизу праворуч":
                x = imageWidth - textSize.Width - margin;
                y = imageHeight - textSize.Height - margin;
                break;

            default:
                x = margin;
                y = margin;
                break;
        }

        return new PointF(
            Math.Max(margin, x),
            Math.Max(margin, y));
    }

    private void buttonApplyWatermark_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpWatermark)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show(
                "Спочатку оберіть зображення!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string watermarkText = txtWatermarkText.Text.Trim();

        if (string.IsNullOrEmpty(watermarkText))
        {
            MessageBox.Show(
                "Введіть текст водяного знаку!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        float fontSize = (float)numWatermarkSize.Value;
        int alpha = (int)(trkWatermarkOpacity.Value * 255 / 100);
        string position = comboWatermarkPosition.SelectedItem?.ToString() ?? "Верх ліворуч";

        FontStyle fontStyle = FontStyle.Regular;
        if (chkWatermarkBold.Checked)
            fontStyle |= FontStyle.Bold;
        if (chkWatermarkItalic.Checked)
            fontStyle |= FontStyle.Italic;

        int selectedIndex = listSelectFile.SelectedIndex;
        if (selectedIndex < 0 || selectedIndex >= selectedFiles.Count)
        {
            MessageBox.Show("Оберіть файл у списку зліва!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string filePath = selectedFiles[selectedIndex];
        try
        {
            Bitmap source = null;
            if (previewBaseBitmap != null && previewBaseFileIndex == selectedIndex)
            {
                source = BitmapOperations.Ensure32bpp(previewBaseBitmap);
            }
            else
            {
                using (Image img = Image.FromFile(filePath))
                    source = BitmapOperations.Ensure32bpp(img);
            }

            using (source)
            using (Bitmap result = BitmapOperations.Ensure32bpp(source))
            using (Graphics g = Graphics.FromImage(result))
            using (Font font = new Font("Segoe UI", fontSize, fontStyle, GraphicsUnit.Point))
            using (Brush brush = new SolidBrush(Color.FromArgb(alpha, watermarkColor)))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                SizeF textSize = g.MeasureString(watermarkText, font);
                PointF positionPoint = chkWatermarkCoordinates.Checked
                    ? new PointF((float)numWatermarkX.Value, (float)numWatermarkY.Value)
                    : GetWatermarkPosition(textSize, result.Width, result.Height, position, 15f);
                positionPoint = new PointF(
                    Math.Max(0, Math.Min(positionPoint.X, result.Width - textSize.Width)),
                    Math.Max(0, Math.Min(positionPoint.Y, result.Height - textSize.Height)));
                g.DrawString(watermarkText, font, brush, positionPoint);

                string fileName = Path.GetFileNameWithoutExtension(filePath) + "_watermark" + Path.GetExtension(filePath);
                string savePath = Path.Combine(outputDir, fileName);
                result.Save(savePath, GetOriginalFormat(filePath));
                Log($"{Path.GetFileName(filePath)}: водяний знак додано ({positionPoint.X:0}, {positionPoint.Y:0}, {trkWatermarkOpacity.Value}%)");
            }
        }
        catch (Exception ex)
        {
            Log($"Помилка водяного знаку {Path.GetFileName(filePath)}: {ex.Message}");
        }

        Log("Додавання водяного знаку завершено!");
    }

    private void buttonSlideshowStart_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpSlideshow)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show(
                "Спочатку оберіть зображення!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        slideshowFiles = new List<string>(selectedFiles);
        slideshowIndex = 0;
        slideshowTimer.Interval = (int)numSlideshowDelay.Value;
        slideshowTimer.Start();

        ShowSlideshowImage();
        Log($"Слайд-шоу запущено: {slideshowFiles.Count} зображень, затримка {slideshowTimer.Interval} мс.");
    }

    private void buttonSlideshowStop_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpSlideshow)) return;

        StopSlideshow(true);
    }

    private void slideshowTimer_Tick(object? sender, EventArgs e)
    {
        if (slideshowFiles.Count == 0)
        {
            StopSlideshow(false);
            return;
        }

        slideshowIndex++;

        if (slideshowIndex >= slideshowFiles.Count)
            slideshowIndex = 0;

        ShowSlideshowImage();
    }

    private void ShowSlideshowImage()
    {
        if (slideshowFiles.Count == 0 ||
            slideshowIndex < 0 ||
            slideshowIndex >= slideshowFiles.Count)
            return;

        string filePath = slideshowFiles[slideshowIndex];

        if (!File.Exists(filePath))
            return;

        try
        {
            int selectedIndex = listSelectFile.SelectedIndex;
            int slideIndex = slideshowFiles.IndexOf(filePath);

            if (slideIndex == selectedIndex && previewBaseBitmap != null)
            {
                SetPreviewImage(BitmapOperations.Ensure32bpp(previewBaseBitmap));
                return;
            }

            using (Image img = Image.FromFile(filePath))
            {
                Bitmap slide = BitmapOperations.Ensure32bpp(img);
                SetPreviewImage(slide);
            }
        }
        catch (Exception ex)
        {
            Log($"Помилка показу слайда {Path.GetFileName(filePath)}: {ex.Message}");
        }
    }

    private void StopSlideshow(bool restorePreview)
    {
        if (slideshowTimer != null)
            slideshowTimer.Stop();

        bool wasRunning = slideshowFiles.Count > 0;
        slideshowFiles.Clear();
        slideshowIndex = 0;

        if (restorePreview && originalBitmapForPreview != null)
        {
            if (activeActionGroup == grpSlideshow)
            {

                if (previewBaseBitmap != null)
                    SetPreviewImage(BitmapOperations.Ensure32bpp(previewBaseBitmap));
                else
                    SetPreviewImage(BitmapOperations.Ensure32bpp(originalBitmapForPreview));
            }
            else
            {
                UpdateLivePreview();
            }
        }
        else if (activeActionGroup == grpSlideshow)
        {
            UpdateSlideshowPreviewFromSelectedFile();
        }

        if (wasRunning)
            Log("Слайд-шоу зупинено.");
    }
}
