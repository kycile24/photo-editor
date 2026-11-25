using System.Drawing.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private readonly TableLayoutPanel workspace = new() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3 };
    private readonly Panel toolTray = new() { Dock = DockStyle.Fill, AutoScroll = true, Margin = Padding.Empty };
    private readonly Color canvasColor = Color.FromArgb(28, 31, 35);
    private TableLayoutPanel header = null!;
    private TableLayoutPanel sidebar = null!;
    private readonly CheckBox compare = new() { Name = "comparePhoto", Text = "Порівняти з оригіналом" };
    private readonly TrackBar divider = new()
    {
        Name = "comparisonDivider", AccessibleName = "Межа оригіналу й результату",
        Maximum = 100, Value = 50, TickStyle = TickStyle.None
    };
    private bool focusMode;
    private FormWindowState previousState;
    private Rectangle previousBounds;
    private Size previousClientSize;

    private void ConfigureWorkspace()
    {
        SuspendLayout();
        AutoScroll = false;
        MinimumSize = new Size(640, 460);
        BackColor = Color.FromArgb(247, 247, 243);
        var groups = Controls.OfType<GroupBox>().ToArray();
        lblPickHint.Visible = false;
        lblAction.Visible = false;
        lblPreview.Visible = false;
        workspace.Margin = Padding.Empty;
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 216));
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));

        header = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, BackColor = Color.FromArgb(36, 44, 44),
            Padding = new Padding(12, 8, 12, 8), Margin = Padding.Empty,
            ColumnCount = 5, RowCount = 1
        };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        foreach (int width in new[] { 116, 105, 130, 68 })
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, width));
        header.Controls.Add(new Label
        {
            Text = "Обробник фотографій", ForeColor = Color.White, AutoEllipsis = true,
            Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(Font.FontFamily, 14, FontStyle.Bold)
        }, 0, 0);
        buttonSelectPicture.Text = "Відкрити…";
        var focus = new Button { Text = "Екран · F11", Name = "focusButton" };
        var export = new Button { Text = "Експорт PNG…" };
        focus.Click += (_, _) => ToggleFocusMode();
        export.Click += ExportPreview;
        int column = 1;
        foreach (Button button in new[] { buttonSelectPicture, focus, export })
        {
            button.Dock = DockStyle.Fill;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.BackColor = button == export ? Color.FromArgb(172, 223, 196) : Color.FromArgb(59, 71, 70);
            button.ForeColor = button == export ? Color.FromArgb(23, 49, 37) : Color.White;
            header.Controls.Add(button, column++, 0);
        }
        var language = new ComboBox { Name = "languageChoice", AccessibleName = "Language / Мова",
            DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        language.Items.AddRange(new object[] { "УКР", "EN" });
        language.SelectedIndex = UiLanguage.English ? 1 : 0;
        language.SelectedIndexChanged += (_, _) =>
        {
            UiLanguage.Select(language.SelectedIndex == 1);
            ApplyLanguage(this);
            picturePreview.Invalidate();
        };
        header.Controls.Add(language, 4, 0);
        workspace.Controls.Add(header, 0, 0);
        workspace.SetColumnSpan(header, 2);

        sidebar = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, Padding = new Padding(12), Margin = Padding.Empty,
            ColumnCount = 1, RowCount = 9, BackColor = BackColor
        };
        sidebar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        foreach (var row in new[] { 28, 38, 30, -62, 42, 32, 0, 30, -38 })
            sidebar.RowStyles.Add(new RowStyle(row < 0 ? SizeType.Percent : SizeType.Absolute, Math.Abs(row)));
        lblFilesList.Text = "Фотографії";
        lblLog.Text = "Історія дій";
        listSelectFile.IntegralHeight = false;
        textLog.BorderStyle = BorderStyle.None;
        textLog.BackColor = sidebar.BackColor;
        buttonResetToOriginal.Text = "Відновити оригінал";
        var controls = new Control[] { new Label { Text = "Інструмент" }, comboAction,
            lblFilesList, listSelectFile, buttonResetToOriginal, compare, divider, lblLog, textLog };
        for (int row = 0; row < controls.Length; row++)
        {
            Control control = controls[row];
            control.Dock = DockStyle.Fill;
            if (control is Label label)
            {
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
            }
            sidebar.Controls.Add(control, 0, row);
        }
        compare.Enabled = false;
        divider.Visible = false;
        compare.CheckedChanged += (_, _) =>
        {
            divider.Visible = compare.Checked;
            sidebar.RowStyles[6].Height = compare.Checked ? 30 : 0;
            picturePreview.Invalidate();
        };
        divider.ValueChanged += (_, _) => picturePreview.Invalidate();
        comboAction.DropDownWidth = 340;
        workspace.Controls.Add(sidebar, 0, 1);

        picturePreview.Dock = DockStyle.Fill;
        picturePreview.Margin = Padding.Empty;
        picturePreview.BorderStyle = BorderStyle.None;
        picturePreview.BackColor = canvasColor;
        picturePreview.Paint += (_, e) =>
        {
            if (picturePreview.Image == null)
                TextRenderer.DrawText(e.Graphics, (UiLanguage.English ? "Drop a photo here or press Ctrl+O" : "Перетягніть фото сюди або натисніть Ctrl+O"),
                    Font, picturePreview.ClientRectangle, Color.Silver,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        };
        picturePreview.Paint += PaintComparison;
        workspace.Controls.Add(picturePreview, 1, 1);
        toolTray.BackColor = Color.White;
        workspace.Controls.Add(toolTray, 0, 2);
        workspace.SetColumnSpan(toolTray, 2);
        foreach (GroupBox group in groups)
        {
            group.Visible = false;
            group.Location = new Point(8, 4);
            toolTray.Controls.Add(group);
        }
        Controls.Add(workspace);
        workspace.BringToFront();
        workspace.SizeChanged += (_, _) => SizeWorkspace();
        ResumeLayout(true);
        AllowDrop = true;
        DragEnter += (_, e) => e.Effect = e.Data?.GetDataPresent(DataFormats.FileDrop) == true
            ? DragDropEffects.Copy : DragDropEffects.None;
        DragDrop += (_, e) =>
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is not string[] paths) return;
            string[] photos = paths.Where(path => File.Exists(path) &&
                Path.GetExtension(path).ToLowerInvariant() is ".jpg" or ".jpeg" or ".bmp" or ".gif" or ".png").ToArray();
            if (photos.Length > 0) LoadFiles(photos);
            else MessageBox.Show(this, (UiLanguage.English ? "Choose a file JPEG, PNG, BMP or GIF." : "Оберіть файл JPEG, PNG, BMP або GIF."));
        };
    }

    private void UpdateComparisonAvailability()
    {
        compare.Enabled = originalBitmapForPreview != null && picturePreview.Image != null &&
            activeActionGroup != grpSlideshow;
        if (!compare.Enabled) compare.Checked = false;
    }

    private void PaintComparison(object? sender, PaintEventArgs e)
    {
        if (!compare.Checked || originalBitmapForPreview is not Bitmap original ||
            picturePreview.Image == null) return;

        Rectangle canvas = picturePreview.ClientRectangle;
        int split = canvas.Width * divider.Value / 100;
        if (split == 0 || canvas.Height == 0) return;
        float scale = Math.Min((float)canvas.Width / original.Width, (float)canvas.Height / original.Height);
        var width = Math.Max(1, (int)(original.Width * scale));
        var height = Math.Max(1, (int)(original.Height * scale));
        var bounds = new Rectangle((canvas.Width - width) / 2, (canvas.Height - height) / 2, width, height);
        var state = e.Graphics.Save();
        e.Graphics.SetClip(new Rectangle(0, 0, split, canvas.Height), System.Drawing.Drawing2D.CombineMode.Intersect);
        using (var background = new SolidBrush(canvasColor))
            e.Graphics.FillRectangle(background, canvas);
        e.Graphics.DrawImage(original, bounds);
        e.Graphics.Restore(state);
        if (split < canvas.Width)
            e.Graphics.DrawLine(Pens.White, split, 0, split, canvas.Height);
        if (split > 88)
            TextRenderer.DrawText(e.Graphics, (UiLanguage.English ? "Original" : "Оригінал"), Font, new Point(10, 10), Color.White, canvasColor);
        if (canvas.Width - split > 96)
            TextRenderer.DrawText(e.Graphics, (UiLanguage.English ? "Result" : "Результат"), Font, new Point(split + 10, 10), Color.White, canvasColor);
    }

    private void SizeWorkspace()
    {
        int side = focusMode ? 0 : Math.Clamp(workspace.ClientSize.Width / 6, 160, 236);
        int tools = focusMode || activeActionGroup == null ? 0 :
            Math.Min(activeActionGroup.Height + SystemInformation.HorizontalScrollBarHeight + 10,
                     workspace.ClientSize.Height / 3);
        workspace.ColumnStyles[0].Width = side;
        workspace.RowStyles[0].Height = focusMode ? 0 : 60;
        workspace.RowStyles[2].Height = tools;
    }

    private void ToggleFocusMode()
    {
        SuspendLayout();
        focusMode = !focusMode;
        if (focusMode)
        {
            previousClientSize = ClientSize;
            previousState = WindowState;
            previousBounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
            var screen = Screen.FromControl(this);
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = screen.Bounds;
            picturePreview.Focus();
        }
        else
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            Bounds = previousBounds;
            WindowState = previousState;
            if (previousState == FormWindowState.Normal) ClientSize = previousClientSize;
        }
        header.Visible = sidebar.Visible = toolTray.Visible = !focusMode;
        SizeWorkspace();
        ResumeLayout(true);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.F11 || (focusMode && keyData == Keys.Escape))
            ToggleFocusMode();
        else if (keyData == (Keys.Control | Keys.O))
            button1_Click(this, EventArgs.Empty);
        else if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            ExportPreview(this, EventArgs.Empty);
        else
            return base.ProcessCmdKey(ref msg, keyData);
        return true;
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
            MessageBox.Show(this, (UiLanguage.English ? "Open a photo first." : "Спочатку відкрийте фотографію."));
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
            MessageBox.Show(this, (UiLanguage.English ? "Choose a new name for the edited copy." : "Оберіть нове ім’я для відредагованої копії."));
            return;
        }
        try
        {
            snapshot.Save(dialog.FileName, ImageFormat.Png);
            Log((UiLanguage.English ? $"Exported {Path.GetFileName(dialog.FileName)} · {snapshot.Width} × {snapshot.Height}" : $"Експортовано {Path.GetFileName(dialog.FileName)} · {snapshot.Width} × {snapshot.Height}"));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, (UiLanguage.English ? "Unable to save PNG" : "Не вдалося зберегти PNG"));
        }
    }
}
