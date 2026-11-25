using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Timer = System.Windows.Forms.Timer;
using PhotoEditor.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm : Form
{
    private List<string> selectedFiles = new List<string>();

    private Bitmap? originalBitmapForPreview = null;

    private Bitmap? previewBaseBitmap = null;

    private int previewBaseFileIndex = -1;

    private (double x, double y, double w, double h)? cropSelectionRel = null;

    private bool isSelecting = false;

    private Point selectionStartControl;

    private Rectangle currentSelectionControl;

    private Color watermarkColor = Color.Red;

    private GroupBox? activeActionGroup = null;

    private string activeColorTransform = "grayscale";

    private Timer slideshowTimer = null!;

    private int slideshowIndex = 0;

    private List<string> slideshowFiles = new List<string>();

    private DataGridView dgvNoiseResults = null!;

    private Button buttonSaveNoiseResult = null!;

    private Button buttonProcessAllNoiseImages = null!;

    private Button buttonSaveNoiseCsv = null!;

    public EditorForm()
    {
        InitializeComponent();
        InitializeNoiseResultsUi();
        AttachLivePreviewEvents();
        ConfigureWorkspace();
        ApplyLanguage(this);

        slideshowTimer = new Timer();
        slideshowTimer.Tick += slideshowTimer_Tick;
        AttachTaskPreviewEvents();
        comboAction.SelectedIndex = 0;
        SetActionControlsEnabled();
    }

    private void SetActionControlsEnabled()
    {
        toolTray.AutoScrollPosition = Point.Empty;
        foreach (GroupBox group in toolTray.Controls.OfType<GroupBox>())
            group.Visible = group == activeActionGroup;
        SizeWorkspace();
    }

    private void comboAction_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (activeActionGroup != null &&
            activeActionGroup != grpOriginalColor &&
            activeActionGroup != grpBrightnessMatrix &&
            activeActionGroup != grpBrightnessHistogram &&
            originalBitmapForPreview != null &&
            picturePreview.Image != null &&
            previewBaseFileIndex == listSelectFile.SelectedIndex)
        {
            CaptureCurrentPreviewAsBase();
        }

        GroupBox[] groups =
        {
            grpConvert, grpResize, grpReplaceColor, grpColorBalance,
            grpTransparency, grpContrast, grpCrop, grpMerge,
            grpWatermark, grpSlideshow, grpOriginalColor, grpBrightnessMatrix,
            grpBrightnessHistogram, grpColorTransforms, grpSigmaFilter, grpNoiseFiltering
        };

        int index = comboAction.SelectedIndex - 1;
        activeActionGroup = (index >= 0 && index < groups.Length) ? groups[index] : null;
        SetActionControlsEnabled();
        UpdateComparisonAvailability();

        cropSelectionRel = null;
        currentSelectionControl = Rectangle.Empty;
        isSelecting = false;

        if (slideshowTimer != null)
            slideshowTimer.Stop();

        if (activeActionGroup == null)
        {
            if (originalBitmapForPreview != null)
                SetPreviewImage(BitmapOperations.Ensure32bpp(originalBitmapForPreview));
            else
                ClearTaskPreview(picturePreview);
            return;
        }

        Log((UiLanguage.English ? $"Selected tool: {activeActionGroup.Text}" : $"Обрано дію: {activeActionGroup.Text}"));

        if (originalBitmapForPreview == null) return;

        if (previewBaseBitmap == null || previewBaseFileIndex != listSelectFile.SelectedIndex)
            ResetPreviewBaseToOriginal();

        if (activeActionGroup == grpMerge)
            UpdateMergePreview();
        else if (activeActionGroup == grpWatermark)
            UpdateWatermarkPreview();
        else if (activeActionGroup == grpSlideshow)
            UpdateSlideshowPreviewFromSelectedFile();
        else if (activeActionGroup == grpOriginalColor)
            UpdateOriginalColorPreview();
        else if (activeActionGroup == grpBrightnessMatrix)
            UpdateBrightnessMatrix();
        else if (activeActionGroup == grpBrightnessHistogram)
            UpdateBrightnessHistogram();
        else if (activeActionGroup == grpColorTransforms)
            UpdateColorTransformPreview();
        else if (activeActionGroup == grpSigmaFilter)
            UpdateSigmaFilterPreview();
        else if (activeActionGroup == grpNoiseFiltering)
            UpdateNoiseFilterPreview();
        else
            UpdateLivePreview();
    }

    private void SelectAction(GroupBox group)
    {

        if (group == null) return;
        int index = Array.IndexOf(new[] {
            grpConvert, grpResize, grpReplaceColor, grpColorBalance,
            grpTransparency, grpContrast, grpCrop, grpMerge,
            grpWatermark, grpSlideshow, grpOriginalColor, grpBrightnessMatrix,
            grpBrightnessHistogram, grpColorTransforms, grpSigmaFilter, grpNoiseFiltering
        }, group);
        if (index >= 0)
            comboAction.SelectedIndex = index + 1;
    }

    private bool IsActionSelected(GroupBox group)
    {
        return activeActionGroup == group;
    }

    private bool RequireAction(GroupBox group)
    {
        if (activeActionGroup == group) return true;

        MessageBox.Show(
            (UiLanguage.English ? $"First select the tool «{group.Text}» in the dropdown." : $"Спочатку виберіть дію «{group.Text}» у випадаючому списку."),
            (UiLanguage.English ? "No tool selected" : "Дію не вибрано"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        return false;
    }

    private void EditorForm_Load(object sender, EventArgs e)
    {

        if (comboFormat.Items.Count > 0)
            comboFormat.SelectedIndex = 0;

        Log((UiLanguage.English ? "Editor ready. Choose an image to begin." : "Програму запущено. Оберіть зображення для початку."));
    }

    private System.Windows.Forms.Timer livePreviewTimer = null!;

    private void UpdatePreviewColor(NumericUpDown numR, NumericUpDown numG, NumericUpDown numB, Panel preview)
    {
        preview.BackColor = Color.FromArgb((int)numR.Value, (int)numG.Value, (int)numB.Value);
    }

    private void PickColor(NumericUpDown numR, NumericUpDown numG, NumericUpDown numB, Panel preview)
    {
        using (ColorDialog cd = new ColorDialog())
        {
            cd.Color = Color.FromArgb((int)numR.Value, (int)numG.Value, (int)numB.Value);
            if (cd.ShowDialog() == DialogResult.OK)
            {
                numR.Value = cd.Color.R;
                numG.Value = cd.Color.G;
                numB.Value = cd.Color.B;
                preview.BackColor = cd.Color;
                UpdateLivePreview();
            }
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Multiselect = true;
            ofd.Filter = (UiLanguage.English ? "Image|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*" : "Зображення|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Всі файли|*.*");
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFiles(ofd.FileNames);
            }
        }
    }

    private void LoadFiles(string[] paths)
    {
        slideshowTimer.Stop();
        livePreviewTimer.Stop();
        selectedFiles.Clear();

        if (previewBaseBitmap != null)
        {
            previewBaseBitmap.Dispose();
            previewBaseBitmap = null;
        }
        previewBaseFileIndex = -1;

        cropSelectionRel = null;
        currentSelectionControl = Rectangle.Empty;

        selectedFiles.AddRange(paths);
        listSelectFile.Items.Clear();

        foreach (string file in selectedFiles)
        {
            listSelectFile.Items.Add(Path.GetFileName(file));
        }

        if (listSelectFile.Items.Count > 0)
        {
            listSelectFile.SelectedIndex = 0;
        }

        UpdateMergeImageSelectors();
        Log((UiLanguage.English ? $"Files selected: {selectedFiles.Count}" : $"Обрано файлів: {selectedFiles.Count}"));
    }

    private void buttonResetOriginal_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpWatermark)) return;
        ResetToOriginalImage();
    }

    private void buttonResetToOriginal_Click(object sender, EventArgs e)
    {
        if (originalBitmapForPreview == null)
        {
            MessageBox.Show((UiLanguage.English ? "Select an image first!" : "Спочатку оберіть зображення!"), (UiLanguage.English ? "Notice" : "Увага"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ResetToOriginalImage();
    }

    private void DrawSplitGridOverlay(Graphics g, int width, int height, int rows, int cols)
    {
        using (Pen pen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
        {
            int baseW = width / cols;
            int baseH = height / rows;

            for (int i = 1; i < cols; i++)
                g.DrawLine(pen, i * baseW, 0, i * baseW, height);

            for (int j = 1; j < rows; j++)
                g.DrawLine(pen, 0, j * baseH, width, j * baseH);
        }
    }

    private ImageFormat GetOriginalFormat(string path)
    {
        string ext = Path.GetExtension(path).ToLower();
        switch (ext)
        {
            case ".png": return ImageFormat.Png;
            case ".bmp": return ImageFormat.Bmp;
            case ".gif": return ImageFormat.Gif;
            default: return ImageFormat.Jpeg;
        }
    }

    private void btnPickTarget_Click(object sender, EventArgs e)
    {
        PickColor(numTR, numTG, numTB, pnlTargetPreview);
    }

    private void btnPickNew_Click(object sender, EventArgs e)
    {
        PickColor(numNR, numNG, numNB, pnlNewPreview);
    }
}
