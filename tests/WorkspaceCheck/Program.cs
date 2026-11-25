using PhotoEditor.UI;

internal class WorkspaceCheck : EditorForm
{
    [STAThread]
    private static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        using var editor = new WorkspaceCheck();
        editor.Show();
        editor.WindowState = FormWindowState.Normal;
        var preview = (PictureBox)editor.Controls.Find("picturePreview", true).Single();
        var tools = (ComboBox)editor.Controls.Find("comboAction", true).Single();
        var files = (ListBox)editor.Controls.Find("listSelectFile", true).Single();
        editor.ClientSize = new Size(820, 560);
        Application.DoEvents();
        var small = preview.Size;
        editor.ClientSize = new Size(1280, 800);
        Application.DoEvents();
        Require(preview.Width > small.Width && preview.Height > small.Height, "Canvas did not grow");
        var data = new DataObject(DataFormats.FileDrop, new[] { Path.GetFullPath("examples/preview.jpg") });
        editor.OnDragDrop(new DragEventArgs(data, 0, 0, 0, DragDropEffects.Copy, DragDropEffects.Copy));
        Require(preview.Image != null && files.Items.Count == 1, "Photo did not load");
        tools.SelectedIndex = 4;
        Application.DoEvents();
        Require(editor.Controls.Find("grpColorBalance", true).Single().Visible, "Tool panel missing");
        var compare = (CheckBox)editor.Controls.Find("comparePhoto", true).Single();
        var divider = (TrackBar)editor.Controls.Find("comparisonDivider", true).Single();
        Require(compare.Enabled && !divider.Visible, "Unexpected comparison controls");
        var adjustments = (TrackBar)editor.Controls.Find("trkBrightness", true).Single();
        adjustments.Value = 50;
        var deadline = Environment.TickCount64 + 600;
        while (Environment.TickCount64 < deadline)
        {
            Application.DoEvents();
            Thread.Sleep(10);
        }
        var edited = preview.Image;
        compare.Checked = true;
        Require(divider.Visible, "Comparison slider missing");
        using (var original = new Bitmap(preview.Width, preview.Height))
        using (var result = new Bitmap(preview.Width, preview.Height))
        {
            divider.Value = 100;
            preview.DrawToBitmap(original, preview.ClientRectangle);
            divider.Value = 0;
            preview.DrawToBitmap(result, preview.ClientRectangle);
            Require(original.GetPixel(original.Width / 2, original.Height / 2) !=
                    result.GetPixel(result.Width / 2, result.Height / 2), "Comparison did not reveal original");
            Require(ReferenceEquals(edited, preview.Image), "Comparison replaced export image");
        }
        divider.Value = 50;
        using (var screenshot = new Bitmap(editor.Width, editor.Height))
        {
            editor.DrawToBitmap(screenshot, new Rectangle(Point.Empty, editor.Size));
            screenshot.Save(args[0], System.Drawing.Imaging.ImageFormat.Png);
        }
        var language = (ComboBox)editor.Controls.Find("languageChoice", true).Single();
        language.SelectedIndex = 1;
        Require(editor.Text == "Photo Editor" && compare.Text == "Compare with original", "English captions missing");
        language.SelectedIndex = 0;
        Require(editor.Text == "Обробник фотографій", "Ukrainian captions missing");
        var before = preview.Size;
        Message message = default;
        editor.ProcessCmdKey(ref message, Keys.F11);
        Application.DoEvents();
        Require(!files.Visible && preview.Size == editor.ClientSize, "Fullscreen did not fill window");
        editor.ProcessCmdKey(ref message, Keys.Escape);
        Application.DoEvents();
        Require(files.Visible && before == preview.Size, "Editor layout did not restore");
    }

    private static void Require(bool ok, string message)
    {
        if (!ok) throw new InvalidOperationException(message);
    }
}
