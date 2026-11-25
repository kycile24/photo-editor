using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Timer = System.Windows.Forms.Timer;
using PhotoEditor.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private void ResetPreviewBaseToOriginal()
    {
        if (previewBaseBitmap != null)
        {
            previewBaseBitmap.Dispose();
            previewBaseBitmap = null;
        }

        if (originalBitmapForPreview != null)
        {
            previewBaseBitmap = BitmapOperations.Ensure32bpp(originalBitmapForPreview);
            previewBaseFileIndex = listSelectFile.SelectedIndex;
            UpdateWatermarkCoordinateLimits();
        }
        else
        {
            previewBaseFileIndex = -1;
        }
    }

    private void CaptureCurrentPreviewAsBase()
    {
        if (picturePreview.Image == null) return;

        if (previewBaseBitmap != null)
            previewBaseBitmap.Dispose();

        previewBaseBitmap = BitmapOperations.Ensure32bpp(picturePreview.Image);
        previewBaseFileIndex = listSelectFile.SelectedIndex;
        UpdateWatermarkCoordinateLimits();
    }

    private void SetPreviewImage(Bitmap bitmap)
    {

        if (picturePreview.Image != null)
            picturePreview.Image.Dispose();
        picturePreview.Image = bitmap;
        UpdateComparisonAvailability();
    }

    private void ResetToOriginalImage()
    {
        if (originalBitmapForPreview == null)
            return;

        if (slideshowTimer != null)
            slideshowTimer.Stop();

        cropSelectionRel = null;
        currentSelectionControl = Rectangle.Empty;
        isSelecting = false;

        ResetPreviewBaseToOriginal();
        SetPreviewImage(BitmapOperations.Ensure32bpp(originalBitmapForPreview));
        Log((UiLanguage.English ? "Original image restored." : "Зображення повернуто до початкового стану."));
    }

    private void AttachLivePreviewEvents()
    {
        livePreviewTimer = new System.Windows.Forms.Timer();
        livePreviewTimer.Interval = 250;

        livePreviewTimer.Tick += (s, e) =>
        {
            livePreviewTimer.Stop();
            UpdateLivePreview();
        };

        trkR.ValueChanged += (s, e) => ScheduleLivePreview();
        trkG.ValueChanged += (s, e) => ScheduleLivePreview();
        trkB.ValueChanged += (s, e) => ScheduleLivePreview();
        trkBrightness.ValueChanged += (s, e) => ScheduleLivePreview();

        trkAlpha.ValueChanged += (s, e) => ScheduleLivePreview();

        trkContrast.ValueChanged += (s, e) => ScheduleLivePreview();

        EventHandler updateColor = (s, e) =>
        {
            UpdatePreviewColor(
                numTR,
                numTG,
                numTB,
                pnlTargetPreview);

            UpdatePreviewColor(
                numNR,
                numNG,
                numNB,
                pnlNewPreview);

            ScheduleLivePreview();
        };

        numTR.ValueChanged += updateColor;
        numTG.ValueChanged += updateColor;
        numTB.ValueChanged += updateColor;

        numNR.ValueChanged += updateColor;
        numNG.ValueChanged += updateColor;
        numNB.ValueChanged += updateColor;

        numTolerance.ValueChanged += updateColor;

        numSplitRows.ValueChanged += (s, e) =>
        {
            if (originalBitmapForPreview != null && IsActionSelected(grpCrop))
                UpdateLivePreview("split");
        };

        numSplitCols.ValueChanged += (s, e) =>
        {
            if (originalBitmapForPreview != null && IsActionSelected(grpCrop))
                UpdateLivePreview("split");
        };
        numSigma.ValueChanged += (s, e) => UpdateSigmaFilterPreview();
    }

    private void ScheduleLivePreview()
    {
        if (originalBitmapForPreview == null || activeActionGroup == null)
            return;

        livePreviewTimer.Stop();
        livePreviewTimer.Start();
    }

    private void Log(string message)
    {
        textLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listSelectFile.SelectedIndex >= 0 && listSelectFile.SelectedIndex < selectedFiles.Count)
        {
            string filePath = selectedFiles[listSelectFile.SelectedIndex];
            if (File.Exists(filePath))
            {
                if (originalBitmapForPreview != null)
                {
                    originalBitmapForPreview.Dispose();
                    originalBitmapForPreview = null;
                }

                try
                {
                    using (Image tempImg = Image.FromFile(filePath))
                    {
                        originalBitmapForPreview = BitmapOperations.Ensure32bpp(tempImg);
                    }

                    ResetPreviewBaseToOriginal();
                    UpdateWatermarkCoordinateLimits();

                    if (activeActionGroup != null)
                    {
                        if (activeActionGroup == grpMerge) UpdateMergePreview();
                        else if (activeActionGroup == grpWatermark) UpdateWatermarkPreview();
                        else if (activeActionGroup == grpSlideshow) UpdateSlideshowPreviewFromSelectedFile();
                        else if (activeActionGroup == grpOriginalColor) UpdateOriginalColorPreview();
                        else if (activeActionGroup == grpBrightnessMatrix) UpdateBrightnessMatrix();
                        else if (activeActionGroup == grpBrightnessHistogram) UpdateBrightnessHistogram();
                        else if (activeActionGroup == grpColorTransforms) UpdateColorTransformPreview();
                        else if (activeActionGroup == grpSigmaFilter) UpdateSigmaFilterPreview();
                        else if (activeActionGroup == grpNoiseFiltering) UpdateNoiseFilterPreview();
                        else UpdateLivePreview();
                    }
                    else SetPreviewImage(BitmapOperations.Ensure32bpp(originalBitmapForPreview));
                }
                catch (Exception ex)
                {
                    ClearTaskPreview(picturePreview);
                    Log((UiLanguage.English ? $"Unable to open {Path.GetFileName(filePath)}: {ex.Message}" : $"Не вдалося відкрити {Path.GetFileName(filePath)}: {ex.Message}"));
                }
            }
        }
    }

    private void UpdateLivePreview(string activeMode = "all")
    {
        if (originalBitmapForPreview == null || activeActionGroup == null) return;

        Bitmap resultBmp = previewBaseBitmap != null
            ? BitmapOperations.Ensure32bpp(previewBaseBitmap)
            : BitmapOperations.Ensure32bpp(originalBitmapForPreview);

        if (IsActionSelected(grpResize))
        {
            int newW = (int)numberWidth.Value;
            int newH = (int)numberHeight.Value;

            if (checkKeepAspect.Checked)
            {
                double scale = Math.Min((double)newW / resultBmp.Width, (double)newH / resultBmp.Height);
                newW = Math.Max(1, (int)Math.Round(resultBmp.Width * scale));
                newH = Math.Max(1, (int)Math.Round(resultBmp.Height * scale));
            }

            Bitmap resized = BitmapOperations.ResizeBitmap(resultBmp, newW, newH);
            resultBmp.Dispose();
            resultBmp = resized;
        }

        if (activeMode == "crop" && cropSelectionRel != null)
        {
            Rectangle? rectNullable = GetAbsoluteSelection(resultBmp.Width, resultBmp.Height);
            if (rectNullable.HasValue)
            {
                Bitmap cropped = resultBmp.Clone(rectNullable.Value, resultBmp.PixelFormat);
                resultBmp.Dispose();
                resultBmp = cropped;
            }
        }

        if (activeMode == "remove_area" && cropSelectionRel != null)
        {
            Rectangle? rectNullable = GetAbsoluteSelection(resultBmp.Width, resultBmp.Height);
            if (rectNullable.HasValue)
            {
                bool supportsAlpha = listSelectFile.SelectedIndex >= 0 &&
                    listSelectFile.SelectedIndex < selectedFiles.Count &&
                    GetOriginalFormat(selectedFiles[listSelectFile.SelectedIndex]) == ImageFormat.Png;
                using (Graphics g = Graphics.FromImage(resultBmp))
                using (Brush brush = new SolidBrush(supportsAlpha ? Color.Transparent : Color.White))
                {
                    if (supportsAlpha) g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.FillRectangle(brush, rectNullable.Value);
                }
            }
        }

        Color targetColor = Color.FromArgb((int)numTR.Value, (int)numTG.Value, (int)numTB.Value);
        Color newColor = Color.FromArgb((int)numNR.Value, (int)numNG.Value, (int)numNB.Value);
        int tolerance = (int)numTolerance.Value;

        double factorR = trkR.Value / 100.0;
        double factorG = trkG.Value / 100.0;
        double factorB = trkB.Value / 100.0;
        double factorBr = trkBrightness.Value / 100.0;
        double factorAlpha = trkAlpha.Value / 100.0;
        double factorContrast = trkContrast.Value / 100.0;

        bool isColorReplacementActive = IsActionSelected(grpReplaceColor) && targetColor != newColor;
        bool isBalanceActive = IsActionSelected(grpColorBalance) &&
                                (factorR != 1.0 || factorG != 1.0 || factorB != 1.0 || factorBr != 1.0);
        bool isAlphaActive = IsActionSelected(grpTransparency) && factorAlpha != 1.0;
        bool isContrastActive = IsActionSelected(grpContrast) && factorContrast != 1.0;

        if (isColorReplacementActive)
            BitmapOperations.ReplaceColor(resultBmp, targetColor, newColor, tolerance);
        else if (isBalanceActive)
            BitmapOperations.AdjustBalance(resultBmp, factorR, factorG, factorB, factorBr);
        else if (isAlphaActive)
            BitmapOperations.AdjustAlpha(resultBmp, factorAlpha);
        else if (isContrastActive)
            BitmapOperations.AdjustContrast(resultBmp, factorContrast);

        if (activeMode == "split" && IsActionSelected(grpCrop))
        {
            int rows = (int)numSplitRows.Value;
            int cols = (int)numSplitCols.Value;
            using (Graphics g = Graphics.FromImage(resultBmp))
                DrawSplitGridOverlay(g, resultBmp.Width, resultBmp.Height, rows, cols);
        }
        SetPreviewImage(resultBmp);
    }

    private (double scale, double offsetX, double offsetY) GetPreviewTransform()
    {
        int imgW = originalBitmapForPreview.Width;
        int imgH = originalBitmapForPreview.Height;
        int boxW = picturePreview.ClientSize.Width;
        int boxH = picturePreview.ClientSize.Height;

        double scale = Math.Min((double)boxW / imgW, (double)boxH / imgH);
        double drawnW = imgW * scale;
        double drawnH = imgH * scale;
        double offsetX = (boxW - drawnW) / 2.0;
        double offsetY = (boxH - drawnH) / 2.0;

        return (scale, offsetX, offsetY);
    }

    private void PickColorFromPreview(int px, int py)
    {
        if (!IsActionSelected(grpReplaceColor)) return;

        Color picked = originalBitmapForPreview.GetPixel(px, py);

        numTR.Value = picked.R;
        numTG.Value = picked.G;
        numTB.Value = picked.B;
        pnlTargetPreview.BackColor = Color.FromArgb(picked.R, picked.G, picked.B);

        Log((UiLanguage.English ? $"Sampled image color (pixel {px},{py}): R={picked.R} G={picked.G} B={picked.B}" : $"Обрано колір з зображення (піксель {px},{py}): R={picked.R} G={picked.G} B={picked.B}"));

        UpdateLivePreview();
    }

    private void picturePreview_MouseDown(object sender, MouseEventArgs e)
    {
        if (originalBitmapForPreview == null || activeActionGroup == null) return;
        if (!IsActionSelected(grpCrop) && !IsActionSelected(grpReplaceColor)) return;

        isSelecting = true;
        selectionStartControl = e.Location;
        currentSelectionControl = new Rectangle(e.Location, Size.Empty);
    }

    private void picturePreview_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isSelecting) return;

        int x = Math.Min(selectionStartControl.X, e.X);
        int y = Math.Min(selectionStartControl.Y, e.Y);
        int w = Math.Abs(e.X - selectionStartControl.X);
        int h = Math.Abs(e.Y - selectionStartControl.Y);

        currentSelectionControl = new Rectangle(x, y, w, h);
        picturePreview.Invalidate();
    }

    private void picturePreview_MouseUp(object sender, MouseEventArgs e)
    {
        if (!isSelecting || originalBitmapForPreview == null)
        {
            isSelecting = false;
            return;
        }

        isSelecting = false;

        var (scale, offsetX, offsetY) = GetPreviewTransform();
        int imgW = originalBitmapForPreview.Width;
        int imgH = originalBitmapForPreview.Height;

        int dragDistance = Math.Abs(e.X - selectionStartControl.X) + Math.Abs(e.Y - selectionStartControl.Y);

        if (dragDistance < 5)
        {
            if (!IsActionSelected(grpReplaceColor))
            {
                currentSelectionControl = Rectangle.Empty;
                picturePreview.Invalidate();
                return;
            }

            double relXClick = (e.X - offsetX) / scale;
            double relYClick = (e.Y - offsetY) / scale;

            if (relXClick >= 0 && relYClick >= 0 && relXClick < imgW && relYClick < imgH)
            {
                PickColorFromPreview((int)relXClick, (int)relYClick);
            }

            currentSelectionControl = Rectangle.Empty;
            picturePreview.Invalidate();
            return;
        }

        double x1 = (currentSelectionControl.Left - offsetX) / scale;
        double y1 = (currentSelectionControl.Top - offsetY) / scale;
        double x2 = (currentSelectionControl.Right - offsetX) / scale;
        double y2 = (currentSelectionControl.Bottom - offsetY) / scale;

        x1 = Math.Max(0, Math.Min(imgW, x1));
        x2 = Math.Max(0, Math.Min(imgW, x2));
        y1 = Math.Max(0, Math.Min(imgH, y1));
        y2 = Math.Max(0, Math.Min(imgH, y2));

        if (x2 - x1 < 2 || y2 - y1 < 2)
        {
            Log((UiLanguage.English ? "Selection too small. Try again." : "Виділена область занадто мала - спробуйте ще раз."));
            currentSelectionControl = Rectangle.Empty;
            picturePreview.Invalidate();
            return;
        }

        cropSelectionRel = ((x1 / imgW), (y1 / imgH), ((x2 - x1) / imgW), ((y2 - y1) / imgH));

        Log((UiLanguage.English ? $"Selected crop area: ({(int)x1},{(int)y1}) - ({(int)x2},{(int)y2}) in preview coordinates" : $"Виділено область для кадрування: ({(int)x1},{(int)y1}) - ({(int)x2},{(int)y2}) у координатах прев'ю"));

        picturePreview.Invalidate();
    }

    private void picturePreview_Paint(object sender, PaintEventArgs e)
    {
        using (Pen dashPen = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dash })
        {
            if (isSelecting && currentSelectionControl.Width > 0 && currentSelectionControl.Height > 0)
            {
                e.Graphics.DrawRectangle(dashPen, currentSelectionControl);
            }
            else if (cropSelectionRel != null && originalBitmapForPreview != null)
            {
                var (scale, offsetX, offsetY) = GetPreviewTransform();
                var sel = cropSelectionRel.Value;
                int imgW = originalBitmapForPreview.Width;
                int imgH = originalBitmapForPreview.Height;

                float rx = (float)(offsetX + sel.x * imgW * scale);
                float ry = (float)(offsetY + sel.y * imgH * scale);
                float rw = (float)(sel.w * imgW * scale);
                float rh = (float)(sel.h * imgH * scale);

                e.Graphics.DrawRectangle(dashPen, rx, ry, rw, rh);
            }
        }
    }

    private Rectangle? GetAbsoluteSelection(int width, int height)
    {
        if (cropSelectionRel == null) return null;

        var s = cropSelectionRel.Value;

        int x = (int)Math.Round(s.x * width);
        int y = (int)Math.Round(s.y * height);
        int w = (int)Math.Round(s.w * width);
        int h = (int)Math.Round(s.h * height);

        x = Math.Max(0, Math.Min(x, width - 1));
        y = Math.Max(0, Math.Min(y, height - 1));
        w = Math.Max(1, Math.Min(w, width - x));
        h = Math.Max(1, Math.Min(h, height - y));

        return new Rectangle(x, y, w, h);
    }
}
