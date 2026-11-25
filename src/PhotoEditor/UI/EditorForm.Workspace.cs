using System.Drawing.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private void ConfigureWorkspace()
    {
        BackColor = Color.FromArgb(247, 248, 250);
        picturePreview.BackColor = Color.FromArgb(34, 38, 45);
        lblPickHint.Visible = false;
        SetCaption(lblFilesList, "Фотографії", 20);
        SetCaption(lblPreview, "Попередній перегляд", 290);
        SetCaption(lblLog, "Історія дій", 950);

        var menu = new MenuStrip();
        var file = new ToolStripMenuItem("Файл");
        file.DropDownItems.Add(new ToolStripMenuItem("Відкрити фотографії…", null, button1_Click, Keys.Control | Keys.O));
        file.DropDownItems.Add(new ToolStripMenuItem("Експортувати перегляд у PNG…", null, ExportPreview, Keys.Control | Keys.Shift | Keys.S));
        menu.Items.Add(file);
        Controls.Add(menu);
        MainMenuStrip = menu;

        var export = new Button
        {
            Text = "Експорт у PNG…", Location = new Point(1055, 55),
            Size = new Size(165, 38), BackColor = Color.FromArgb(31, 87, 108),
            ForeColor = Color.White, FlatStyle = FlatStyle.Flat
        };
        export.Click += ExportPreview;
        Controls.Add(export);
        AllowDrop = true;
        DragEnter += (_, e) => e.Effect = e.Data?.GetDataPresent(DataFormats.FileDrop) == true
            ? DragDropEffects.Copy : DragDropEffects.None;
        DragDrop += (_, e) =>
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is not string[] paths) return;
            string[] photos = paths.Where(path => File.Exists(path) &&
                Path.GetExtension(path).ToLowerInvariant() is ".jpg" or ".jpeg" or ".bmp" or ".gif" or ".png").ToArray();
            if (photos.Length > 0) LoadFiles(photos);
            else MessageBox.Show(this, "Оберіть файл JPEG, PNG, BMP або GIF.");
        };
    }

    private static void SetCaption(Label label, string text, int x)
    {
        label.Text = text;
        label.Location = new Point(x, 102);
        label.AutoSize = true;
    }

    private void ExportPreview(object? sender, EventArgs e)
    {
        if (livePreviewTimer.Enabled)
        {
            livePreviewTimer.Stop();
            UpdateLivePreview();
        }
        if (picturePreview.Image is not Image image || originalBitmapForPreview == null)
        {
            MessageBox.Show(this, "Спочатку відкрийте фотографію.");
            return;
        }

        // Freeze the displayed frame before opening a modal dialog during a slideshow.
        using var snapshot = new Bitmap(image);
        using var dialog = new SaveFileDialog
        {
            Filter = "PNG (*.png)|*.png", DefaultExt = "png",
            FileName = Path.GetFileNameWithoutExtension(listSelectFile.SelectedItem?.ToString()) + "-edit.png"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        if (selectedFiles.Any(path => string.Equals(Path.GetFullPath(path), dialog.FileName, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "Оберіть нове ім’я для відредагованої копії.");
            return;
        }
        try
        {
            snapshot.Save(dialog.FileName, ImageFormat.Png);
            Log($"Експортовано {Path.GetFileName(dialog.FileName)} · {snapshot.Width} × {snapshot.Height}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Не вдалося зберегти PNG");
        }
    }
}
