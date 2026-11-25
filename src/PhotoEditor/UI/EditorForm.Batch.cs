using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PhotoEditor.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private string? GetOutputFolder()
    {
        using (FolderBrowserDialog fbd = new FolderBrowserDialog())
        {
            fbd.Description = "Оберіть папку для збереження оброблених зображень";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
        }
        return null;
    }

    private void buttonConvertFormat_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpConvert)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string targetFormatStr = comboFormat.SelectedItem?.ToString() ?? "JPEG";
        ImageFormat format = ImageFormat.Jpeg;
        string ext = ".jpg";

        switch (targetFormatStr)
        {
            case "PNG": format = ImageFormat.Png; ext = ".png"; break;
            case "BMP": format = ImageFormat.Bmp; ext = ".bmp"; break;
            case "GIF": format = ImageFormat.Gif; ext = ".gif"; break;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log($"--- Початок конвертації у {targetFormatStr} ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {

                FileInfo originalInfo = new FileInfo(filePath);
                long originalSize = originalInfo.Length;

                string fileName = Path.GetFileNameWithoutExtension(filePath) + "_converted" + ext;
                string savePath = Path.Combine(outputDir, fileName);

                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    bmp.Save(savePath, format);
                }

                FileInfo newInfo = new FileInfo(savePath);
                long newSize = newInfo.Length;
                double diff = originalSize > 0 ? ((double)(newSize - originalSize) / originalSize) * 100 : 0;

                Log($"{Path.GetFileName(filePath)}: {originalSize / 1024.0:F1} КБ -> {newSize / 1024.0:F1} КБ ({diff:+0.0;-0.0}%)");
            }
            catch (Exception ex)
            {
                Log($"Помилка конвертації {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Конвертацію завершено!");
    }

    private void buttonResize_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpResize)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int targetWidth = (int)numberWidth.Value;
        int targetHeight = (int)numberHeight.Value;
        bool keepAspect = checkKeepAspect.Checked;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log("--- Початок зміни розмірів ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image originalImg = Image.FromFile(filePath))
                {
                    int newWidth = targetWidth;
                    int newHeight = targetHeight;

                    if (keepAspect)
                    {

                        double ratioX = (double)targetWidth / originalImg.Width;
                        double ratioY = (double)targetHeight / originalImg.Height;
                        double ratio = Math.Min(ratioX, ratioY);

                        newWidth = (int)Math.Round(originalImg.Width * ratio);
                        newHeight = (int)Math.Round(originalImg.Height * ratio);
                    }

                    newWidth = Math.Max(1, newWidth);
                    newHeight = Math.Max(1, newHeight);

                    using (Bitmap resized = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics g = Graphics.FromImage(resized))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(originalImg, 0, 0, newWidth, newHeight);
                        }

                        string fileName = Path.GetFileNameWithoutExtension(filePath) + $"_resized_{newWidth}x{newHeight}" + Path.GetExtension(filePath);
                        string savePath = Path.Combine(outputDir, fileName);

                        resized.Save(savePath, GetOriginalFormat(filePath));
                        Log($"Змінено: {Path.GetFileName(filePath)} ({originalImg.Width}x{originalImg.Height}) -> {newWidth}x{newHeight} px");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка зміни розміру {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Зміну розмірів завершено!");
    }

    private void buttonReplaceColor_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpReplaceColor)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Color targetColor = Color.FromArgb((int)numTR.Value, (int)numTG.Value, (int)numTB.Value);
        Color newColor = Color.FromArgb((int)numNR.Value, (int)numNG.Value, (int)numNB.Value);
        int tolerance = (int)numTolerance.Value;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log("--- Початок заміни кольору ---");
        Log($"Шукаємо колір R={targetColor.R} G={targetColor.G} B={targetColor.B} (допуск {tolerance}), " +
            $"замінюємо на R={newColor.R} G={newColor.G} B={newColor.B}");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    int replacedPixels = BitmapOperations.ReplaceColor(bmp, targetColor, newColor, tolerance);

                    string fileName = Path.GetFileNameWithoutExtension(filePath) + "_color_replaced" + Path.GetExtension(filePath);
                    string savePath = Path.Combine(outputDir, fileName);

                    bmp.Save(savePath, GetOriginalFormat(filePath));
                    Log($"{Path.GetFileName(filePath)}: замінено пікселів - {replacedPixels}");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка заміни кольору {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Заміну кольору завершено! Файли збережено з суфіксом _color_replaced у папці: " + outputDir);
    }

    private void buttonAdjustColor_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpColorBalance)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        double factorR = trkR.Value / 100.0;
        double factorG = trkG.Value / 100.0;
        double factorB = trkB.Value / 100.0;
        double factorBr = trkBrightness.Value / 100.0;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log($"--- Початок корекції балансу (R={trkR.Value}%, G={trkG.Value}%, B={trkB.Value}%, Яскравість={trkBrightness.Value}%) ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    BitmapOperations.AdjustBalance(bmp, factorR, factorG, factorB, factorBr);

                    string fileName = Path.GetFileNameWithoutExtension(filePath) + "_balanced" + Path.GetExtension(filePath);
                    string savePath = Path.Combine(outputDir, fileName);

                    bmp.Save(savePath, GetOriginalFormat(filePath));
                    Log($"Скориговано баланс: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка корекції балансу {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Корекцію балансу завершено!");
    }

    private void buttonApplyTransparency_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpTransparency)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        double factorAlpha = trkAlpha.Value / 100.0;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log($"--- Початок застосування прозорості ({trkAlpha.Value}%) ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                ImageFormat fmt = GetOriginalFormat(filePath);

                if (fmt != ImageFormat.Png)
                {
                    Log($"Попередження: {Path.GetFileName(filePath)} буде збережено у форматі, " +
                        $"що не підтримує прозорість - альфа-канал відкинеться при збереженні.");
                }

                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    BitmapOperations.AdjustAlpha(bmp, factorAlpha);

                    string fileName = Path.GetFileNameWithoutExtension(filePath) + "_transparent" + Path.GetExtension(filePath);
                    string savePath = Path.Combine(outputDir, fileName);

                    bmp.Save(savePath, fmt);
                    Log($"Застосовано прозорість {trkAlpha.Value}%: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка застосування прозорості {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Застосування прозорості завершено!");
    }

    private void buttonApplyContrast_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpContrast)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        double factorContrast = trkContrast.Value / 100.0;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log($"--- Початок зміни контрастності ({trkContrast.Value}%) ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    BitmapOperations.AdjustContrast(bmp, factorContrast);

                    string fileName = Path.GetFileNameWithoutExtension(filePath) + "_contrast" + Path.GetExtension(filePath);
                    string savePath = Path.Combine(outputDir, fileName);

                    bmp.Save(savePath, GetOriginalFormat(filePath));
                    Log($"Скориговано контраст: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка зміни контрастності {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Зміну контрастності завершено!");
    }

    private void buttonSplitParts_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpCrop)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int rows = (int)numSplitRows.Value;
        int cols = (int)numSplitCols.Value;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log($"--- Початок розбиття на частини (сітка {rows}x{cols} = {rows * cols} частин) ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    int baseW = bmp.Width / cols;
                    int baseH = bmp.Height / rows;
                    int savedParts = 0;

                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            int x = c * baseW;
                            int y = r * baseH;

                            int w = (c == cols - 1) ? bmp.Width - x : baseW;
                            int h = (r == rows - 1) ? bmp.Height - y : baseH;

                            using (Bitmap part = bmp.Clone(new Rectangle(x, y, w, h), bmp.PixelFormat))
                            {
                                string fileName = Path.GetFileNameWithoutExtension(filePath)
                                    + $"_part_r{r + 1}_c{c + 1}"
                                    + Path.GetExtension(filePath);

                                string savePath = Path.Combine(outputDir, fileName);
                                part.Save(savePath, GetOriginalFormat(filePath));
                                savedParts++;
                            }
                        }
                    }

                    Log($"{Path.GetFileName(filePath)}: розбито на {savedParts} частин");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка розбиття {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Розбиття на частини завершено!");
    }

    private void buttonCropKeepSelection_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpCrop)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cropSelectionRel == null)
        {
            MessageBox.Show(
                "Спочатку виділіть область мишею на попередньому перегляді (перетягніть прямокутник)!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log("--- Початок кадрування (залишаємо лише виділену область) ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    Rectangle rect = GetAbsoluteSelection(bmp.Width, bmp.Height).Value;

                    using (Bitmap cropped = bmp.Clone(rect, bmp.PixelFormat))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath) + "_cropped" + Path.GetExtension(filePath);
                        string savePath = Path.Combine(outputDir, fileName);

                        cropped.Save(savePath, GetOriginalFormat(filePath));
                        Log($"{Path.GetFileName(filePath)}: вирізано область {rect.Width}x{rect.Height}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка кадрування {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Кадрування завершено!");
    }

    private void buttonRemoveSelection_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpCrop)) return;

        if (selectedFiles.Count == 0)
        {
            MessageBox.Show("Спочатку оберіть зображення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cropSelectionRel == null)
        {
            MessageBox.Show(
                "Спочатку виділіть область мишею на попередньому перегляді (перетягніть прямокутник)!",
                "Увага",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        Log("--- Початок видалення виділеної області ---");

        foreach (string filePath in selectedFiles)
        {
            try
            {
                ImageFormat fmt = GetOriginalFormat(filePath);
                bool supportsAlpha = fmt == ImageFormat.Png;

                using (Image img = Image.FromFile(filePath))
                using (Bitmap bmp = BitmapOperations.Ensure32bpp(img))
                {
                    Rectangle rect = GetAbsoluteSelection(bmp.Width, bmp.Height).Value;

                    using (Graphics g = Graphics.FromImage(bmp))
                    using (Brush brush = new SolidBrush(supportsAlpha ? Color.Transparent : Color.White))
                    {
                        if (supportsAlpha)
                        {

                            g.CompositingMode = CompositingMode.SourceCopy;
                        }

                        g.FillRectangle(brush, rect);
                    }

                    string fileName = Path.GetFileNameWithoutExtension(filePath) + "_area_removed" + Path.GetExtension(filePath);
                    string savePath = Path.Combine(outputDir, fileName);

                    bmp.Save(savePath, fmt);
                    Log($"{Path.GetFileName(filePath)}: видалено область {rect.Width}x{rect.Height}");
                }
            }
            catch (Exception ex)
            {
                Log($"Помилка видалення області {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Log("Видалення виділеної області завершено!");
    }
}
