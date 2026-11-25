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
        using (var screenshot = new Bitmap(editor.Width, editor.Height))
        {
            editor.DrawToBitmap(screenshot, new Rectangle(Point.Empty, editor.Size));
            screenshot.Save(args[0], System.Drawing.Imaging.ImageFormat.Png);
        }
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
