namespace PhotoEditor.UI
{
    partial class EditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            buttonSelectPicture = new Button();
            lblAction = new Label();
            comboAction = new ComboBox();
            buttonResetToOriginal = new Button();
            listSelectFile = new ListBox();
            picturePreview = new PictureBox();
            textLog = new TextBox();
            comboFormat = new ComboBox();
            buttonConvertFormat = new Button();
            numberWidth = new NumericUpDown();
            numberHeight = new NumericUpDown();
            checkKeepAspect = new CheckBox();
            buttonResize = new Button();
            numTR = new NumericUpDown();
            numTG = new NumericUpDown();
            numTB = new NumericUpDown();
            numNR = new NumericUpDown();
            numNG = new NumericUpDown();
            numNB = new NumericUpDown();
            numTolerance = new NumericUpDown();
            buttonReplaceColor = new Button();
            trkR = new TrackBar();
            trkG = new TrackBar();
            trkB = new TrackBar();
            trkBrightness = new TrackBar();
            buttonAdjustColor = new Button();
            trkAlpha = new TrackBar();
            buttonApplyTransparency = new Button();
            trkContrast = new TrackBar();
            buttonApplyContrast = new Button();
            numSplitRows = new NumericUpDown();
            numSplitCols = new NumericUpDown();
            buttonSplitParts = new Button();
            buttonCropKeepSelection = new Button();
            buttonRemoveSelection = new Button();
            pnlTargetPreview = new Panel();
            pnlNewPreview = new Panel();
            grpConvert = new GroupBox();
            lblFormat = new Label();
            grpResize = new GroupBox();
            lblWidth = new Label();
            lblHeight = new Label();
            grpReplaceColor = new GroupBox();
            lblTargetColor = new Label();
            btnPickTarget = new Button();
            lblTR = new Label();
            lblTG = new Label();
            lblTB = new Label();
            lblNewColor = new Label();
            btnPickNew = new Button();
            lblNR = new Label();
            lblNG = new Label();
            lblNB = new Label();
            lblTolerance = new Label();
            grpColorBalance = new GroupBox();
            lblR = new Label();
            lblG = new Label();
            lblB = new Label();
            lblBrightness = new Label();
            grpTransparency = new GroupBox();
            lblAlpha = new Label();
            grpContrast = new GroupBox();
            lblContrast = new Label();
            grpCrop = new GroupBox();
            lblSplitHint = new Label();
            lblSplitRows = new Label();
            lblSplitCols = new Label();
            lblCropSelectionHint = new Label();

            grpMerge = new GroupBox();
            lblMergeFirst = new Label();
            comboMergeFirst = new ComboBox();
            lblMergeSecond = new Label();
            comboMergeSecond = new ComboBox();
            radioMergeHorizontal = new RadioButton();
            radioMergeVertical = new RadioButton();
            buttonMergeImages = new Button();
            grpWatermark = new GroupBox();
            lblWatermarkText = new Label();
            txtWatermarkText = new TextBox();
            lblWatermarkPosition = new Label();
            comboWatermarkPosition = new ComboBox();
            lblWatermarkSize = new Label();
            numWatermarkSize = new NumericUpDown();
            lblWatermarkOpacity = new Label();
            trkWatermarkOpacity = new TrackBar();
            btnWatermarkColor = new Button();
            pnlWatermarkColor = new Panel();
            chkWatermarkBold = new CheckBox();
            chkWatermarkItalic = new CheckBox();
            buttonApplyWatermark = new Button();
            chkWatermarkCoordinates = new CheckBox();
            lblWatermarkX = new Label();
            numWatermarkX = new NumericUpDown();
            lblWatermarkY = new Label();
            numWatermarkY = new NumericUpDown();
            buttonResetOriginal = new Button();
            grpSlideshow = new GroupBox();
            lblSlideshowDelay = new Label();
            numSlideshowDelay = new NumericUpDown();
            buttonSlideshowStart = new Button();
            buttonSlideshowStop = new Button();
            lblSlideshowHint = new Label();
            grpOriginalColor = new GroupBox();
            lblOriginalColorHint = new Label();
            grpBrightnessMatrix = new GroupBox();
            txtBrightnessMatrix = new TextBox();
            grpBrightnessHistogram = new GroupBox();
            pnlBrightnessHistogram = new Panel();
            lblBrightnessStats = new Label();
            grpColorTransforms = new GroupBox();
            buttonBinary = new Button();
            buttonGrayscale = new Button();
            buttonNegative = new Button();
            buttonSaveColorTransform = new Button();
            pnlGrayHistogram = new Panel();
            lblGrayStats = new Label();
            grpSigmaFilter = new GroupBox();
            lblSigma = new Label();
            numSigma = new NumericUpDown();
            buttonSaveSigmaFilter = new Button();
            lblSigmaHint = new Label();
            lblFilesList = new Label();
            lblPreview = new Label();
            lblPickHint = new Label();
            lblLog = new Label();
            grpNoiseFiltering = new GroupBox();
            comboNoiseFilter = new ComboBox();
            lblNoiseFilter = new Label();
            buttonApplyNoiseFilter = new Button();
            buttonRunAllFilters = new Button();
            lblNoiseMse = new Label();
            textNoiseMse = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTolerance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSplitRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSplitCols).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkWatermarkOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSlideshowDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSigma).BeginInit();
            grpConvert.SuspendLayout();
            grpResize.SuspendLayout();
            grpReplaceColor.SuspendLayout();
            grpColorBalance.SuspendLayout();
            grpTransparency.SuspendLayout();
            grpContrast.SuspendLayout();
            grpCrop.SuspendLayout();
            grpSigmaFilter.SuspendLayout();
            SuspendLayout();

            buttonSelectPicture.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            buttonSelectPicture.Location = new Point(20, 55);
            buttonSelectPicture.Name = "buttonSelectPicture";
            buttonSelectPicture.Size = new Size(245, 38);
            buttonSelectPicture.TabIndex = 1;
            buttonSelectPicture.Text = "Обрати зображення";
            buttonSelectPicture.UseVisualStyleBackColor = true;
            buttonSelectPicture.Click += button1_Click;

            lblAction.AutoSize = true;
            lblAction.Location = new Point(290, 64);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(45, 20);
            lblAction.TabIndex = 2;
            lblAction.Text = "Дія:";

            comboAction.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAction.FormattingEnabled = true;
            comboAction.Items.AddRange(new object[] {
                "Оберіть дію",
                "Конвертація формату",
                "Зміна розмірів",
                "Заміна кольору",
                "Колірний баланс та яскравість",
                "Прозорість зображення",
                "Контрастність",
                "Кадрування / розбиття",
                "Об'єднання зображень",
                "Водяний знак",
                "Слайд-шоу",
                "Оригінал",
                "Матриця значень яскравості",
                "Гістограма яскравості кольорового зображення",
                "Кольорові ефекти",
                "Фільтрація зображення — сигма-фільтр",
                "Зменшення шуму"});
                 
            comboAction.Location = new Point(340, 55);
            comboAction.Name = "comboAction";
            comboAction.Size = new Size(460, 28);
            comboAction.TabIndex = 4;
            comboAction.SelectedIndexChanged += comboAction_SelectedIndexChanged;

            buttonResetToOriginal.Location = new Point(820, 55);
            buttonResetToOriginal.Name = "buttonResetToOriginal";
            buttonResetToOriginal.Size = new Size(220, 38);
            buttonResetToOriginal.TabIndex = 19;
            buttonResetToOriginal.Text = "Повернути початкове фото";
            buttonResetToOriginal.UseVisualStyleBackColor = true;
            buttonResetToOriginal.Click += buttonResetToOriginal_Click;

            listSelectFile.FormattingEnabled = true;
            listSelectFile.HorizontalScrollbar = true;
            listSelectFile.Location = new Point(20, 128);
            listSelectFile.Name = "listSelectFile";
            listSelectFile.Size = new Size(245, 334);
            listSelectFile.TabIndex = 3;
            listSelectFile.SelectedIndexChanged += listBox1_SelectedIndexChanged;

            picturePreview.BorderStyle = BorderStyle.FixedSingle;
            picturePreview.Cursor = Cursors.Cross;
            picturePreview.Location = new Point(290, 128);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(640, 335);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 5;
            picturePreview.TabStop = false;
            picturePreview.Paint += picturePreview_Paint;
            picturePreview.MouseDown += picturePreview_MouseDown;
            picturePreview.MouseMove += picturePreview_MouseMove;
            picturePreview.MouseUp += picturePreview_MouseUp;

            textLog.BackColor = Color.White;
            textLog.BorderStyle = BorderStyle.FixedSingle;
            textLog.Location = new Point(950, 128);
            textLog.Multiline = true;
            textLog.Name = "textLog";
            textLog.ReadOnly = true;
            textLog.ScrollBars = ScrollBars.Vertical;
            textLog.Size = new Size(270, 334);
            textLog.TabIndex = 8;

            comboFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFormat.Items.AddRange(new object[] { "JPEG", "PNG", "BMP", "GIF" });
            comboFormat.Location = new Point(75, 27);
            comboFormat.Name = "comboFormat";
            comboFormat.Size = new Size(120, 28);
            comboFormat.TabIndex = 1;

            buttonConvertFormat.Location = new Point(210, 25);
            buttonConvertFormat.Name = "buttonConvertFormat";
            buttonConvertFormat.Size = new Size(355, 36);
            buttonConvertFormat.TabIndex = 2;
            buttonConvertFormat.Text = "Зберегти всі у вибраному форматі";
            buttonConvertFormat.UseVisualStyleBackColor = true;
            buttonConvertFormat.Click += buttonConvertFormat_Click;

            numberWidth.Location = new Point(78, 27);
            numberWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numberWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberWidth.Name = "numberWidth";
            numberWidth.Size = new Size(75, 27);
            numberWidth.TabIndex = 1;
            numberWidth.Value = new decimal(new int[] { 800, 0, 0, 0 });

            numberHeight.Location = new Point(225, 27);
            numberHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numberHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberHeight.Name = "numberHeight";
            numberHeight.Size = new Size(75, 27);
            numberHeight.TabIndex = 3;
            numberHeight.Value = new decimal(new int[] { 600, 0, 0, 0 });

            checkKeepAspect.AutoSize = true;
            checkKeepAspect.Checked = true;
            checkKeepAspect.CheckState = CheckState.Checked;
            checkKeepAspect.Location = new Point(315, 30);
            checkKeepAspect.Name = "checkKeepAspect";
            checkKeepAspect.Size = new Size(173, 24);
            checkKeepAspect.TabIndex = 4;
            checkKeepAspect.Text = "Зберігати пропорції";
            checkKeepAspect.UseVisualStyleBackColor = true;

            buttonResize.Location = new Point(455, 24);
            buttonResize.Name = "buttonResize";
            buttonResize.Size = new Size(120, 38);
            buttonResize.TabIndex = 5;
            buttonResize.Text = "Зберегти";
            buttonResize.UseVisualStyleBackColor = true;
            buttonResize.Click += buttonResize_Click;

            numTR.Location = new Point(108, 51);
            numTR.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numTR.Name = "numTR";
            numTR.Size = new Size(55, 27);
            numTR.TabIndex = 4;

            numTG.Location = new Point(190, 51);
            numTG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numTG.Name = "numTG";
            numTG.Size = new Size(55, 27);
            numTG.TabIndex = 6;

            numTB.Location = new Point(272, 51);
            numTB.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numTB.Name = "numTB";
            numTB.Size = new Size(55, 27);
            numTB.TabIndex = 8;

            numNR.Location = new Point(453, 51);
            numNR.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNR.Name = "numNR";
            numNR.Size = new Size(55, 27);
            numNR.TabIndex = 13;

            numNG.Location = new Point(535, 51);
            numNG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNG.Name = "numNG";
            numNG.Size = new Size(55, 27);
            numNG.TabIndex = 15;

            numNB.Location = new Point(617, 51);
            numNB.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNB.Name = "numNB";
            numNB.Size = new Size(55, 27);
            numNB.TabIndex = 17;

            numTolerance.Location = new Point(700, 51);
            numTolerance.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numTolerance.Name = "numTolerance";
            numTolerance.Size = new Size(65, 27);
            numTolerance.TabIndex = 19;
            numTolerance.Value = new decimal(new int[] { 30, 0, 0, 0 });

            buttonReplaceColor.Location = new Point(805, 45);
            buttonReplaceColor.Name = "buttonReplaceColor";
            buttonReplaceColor.Size = new Size(370, 42);
            buttonReplaceColor.TabIndex = 20;
            buttonReplaceColor.Text = "Зберегти заміну кольору";
            buttonReplaceColor.UseVisualStyleBackColor = true;
            buttonReplaceColor.Click += buttonReplaceColor_Click;

            trkR.Location = new Point(10, 45);
            trkR.Maximum = 200;
            trkR.Name = "trkR";
            trkR.Size = new Size(220, 56);
            trkR.TabIndex = 1;
            trkR.Value = 100;

            trkG.Location = new Point(240, 45);
            trkG.Maximum = 200;
            trkG.Name = "trkG";
            trkG.Size = new Size(220, 56);
            trkG.TabIndex = 3;
            trkG.Value = 100;

            trkB.Location = new Point(470, 45);
            trkB.Maximum = 200;
            trkB.Name = "trkB";
            trkB.Size = new Size(220, 56);
            trkB.TabIndex = 5;
            trkB.Value = 100;

            trkBrightness.Location = new Point(700, 45);
            trkBrightness.Maximum = 200;
            trkBrightness.Name = "trkBrightness";
            trkBrightness.Size = new Size(220, 56);
            trkBrightness.TabIndex = 7;
            trkBrightness.Value = 100;

            buttonAdjustColor.Location = new Point(945, 43);
            buttonAdjustColor.Name = "buttonAdjustColor";
            buttonAdjustColor.Size = new Size(230, 43);
            buttonAdjustColor.TabIndex = 8;
            buttonAdjustColor.Text = "Зберегти баланс";
            buttonAdjustColor.UseVisualStyleBackColor = true;
            buttonAdjustColor.Click += buttonAdjustColor_Click;

            trkAlpha.Location = new Point(15, 43);
            trkAlpha.Maximum = 100;
            trkAlpha.Name = "trkAlpha";
            trkAlpha.Size = new Size(315, 56);
            trkAlpha.TabIndex = 1;
            trkAlpha.Value = 100;

            buttonApplyTransparency.Location = new Point(350, 38);
            buttonApplyTransparency.Name = "buttonApplyTransparency";
            buttonApplyTransparency.Size = new Size(225, 43);
            buttonApplyTransparency.TabIndex = 2;
            buttonApplyTransparency.Text = "Зберегти з прозорістю";
            buttonApplyTransparency.UseVisualStyleBackColor = true;
            buttonApplyTransparency.Click += buttonApplyTransparency_Click;

            trkContrast.Location = new Point(15, 43);
            trkContrast.Maximum = 200;
            trkContrast.Name = "trkContrast";
            trkContrast.Size = new Size(315, 56);
            trkContrast.TabIndex = 1;
            trkContrast.Value = 100;

            buttonApplyContrast.Location = new Point(350, 38);
            buttonApplyContrast.Name = "buttonApplyContrast";
            buttonApplyContrast.Size = new Size(225, 43);
            buttonApplyContrast.TabIndex = 2;
            buttonApplyContrast.Text = "Зберегти з контрастом";
            buttonApplyContrast.UseVisualStyleBackColor = true;
            buttonApplyContrast.Click += buttonApplyContrast_Click;

            numSplitRows.Location = new Point(220, 23);
            numSplitRows.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numSplitRows.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSplitRows.Name = "numSplitRows";
            numSplitRows.Size = new Size(60, 27);
            numSplitRows.TabIndex = 2;
            numSplitRows.Value = new decimal(new int[] { 2, 0, 0, 0 });

            numSplitCols.Location = new Point(370, 23);
            numSplitCols.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numSplitCols.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSplitCols.Name = "numSplitCols";
            numSplitCols.Size = new Size(60, 27);
            numSplitCols.TabIndex = 4;
            numSplitCols.Value = new decimal(new int[] { 2, 0, 0, 0 });

            buttonSplitParts.Location = new Point(468, 14);
            buttonSplitParts.Name = "buttonSplitParts";
            buttonSplitParts.Size = new Size(220, 36);
            buttonSplitParts.TabIndex = 5;
            buttonSplitParts.Text = "Розбити на частини";
            buttonSplitParts.UseVisualStyleBackColor = true;
            buttonSplitParts.Click += buttonSplitParts_Click;

            buttonCropKeepSelection.Location = new Point(450, 58);
            buttonCropKeepSelection.Name = "buttonCropKeepSelection";
            buttonCropKeepSelection.Size = new Size(330, 36);
            buttonCropKeepSelection.TabIndex = 7;
            buttonCropKeepSelection.Text = "Залишити тільки виділену область";
            buttonCropKeepSelection.UseVisualStyleBackColor = true;
            buttonCropKeepSelection.Click += buttonCropKeepSelection_Click;

            buttonRemoveSelection.Location = new Point(790, 58);
            buttonRemoveSelection.Name = "buttonRemoveSelection";
            buttonRemoveSelection.Size = new Size(330, 36);
            buttonRemoveSelection.TabIndex = 8;
            buttonRemoveSelection.Text = "Видалити виділену область";
            buttonRemoveSelection.UseVisualStyleBackColor = true;
            buttonRemoveSelection.Click += buttonRemoveSelection_Click;

            pnlTargetPreview.BackColor = Color.Black;
            pnlTargetPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlTargetPreview.Location = new Point(15, 51);
            pnlTargetPreview.Name = "pnlTargetPreview";
            pnlTargetPreview.Size = new Size(28, 28);
            pnlTargetPreview.TabIndex = 1;

            pnlNewPreview.BackColor = Color.Black;
            pnlNewPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlNewPreview.Location = new Point(360, 51);
            pnlNewPreview.Name = "pnlNewPreview";
            pnlNewPreview.Size = new Size(28, 28);
            pnlNewPreview.TabIndex = 10;

            grpConvert.Controls.Add(lblFormat);
            grpConvert.Controls.Add(comboFormat);
            grpConvert.Controls.Add(buttonConvertFormat);
            grpConvert.Location = new Point(20, 485);
            grpConvert.Name = "grpConvert";
            grpConvert.Size = new Size(590, 82);
            grpConvert.TabIndex = 9;
            grpConvert.TabStop = false;
            grpConvert.Text = "Конвертація формату";

            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(15, 31);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(66, 20);
            lblFormat.TabIndex = 0;
            lblFormat.Text = "Формат:";

            grpResize.Controls.Add(lblWidth);
            grpResize.Controls.Add(numberWidth);
            grpResize.Controls.Add(lblHeight);
            grpResize.Controls.Add(numberHeight);
            grpResize.Controls.Add(checkKeepAspect);
            grpResize.Controls.Add(buttonResize);
            grpResize.Location = new Point(20, 485);
            grpResize.Name = "grpResize";
            grpResize.Size = new Size(590, 82);
            grpResize.TabIndex = 10;
            grpResize.TabStop = false;
            grpResize.Text = "Зміна розмірів";

            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(15, 31);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(70, 20);
            lblWidth.TabIndex = 0;
            lblWidth.Text = "Ширина:";

            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(165, 31);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(60, 20);
            lblHeight.TabIndex = 2;
            lblHeight.Text = "Висота:";

            grpReplaceColor.Controls.Add(lblTargetColor);
            grpReplaceColor.Controls.Add(pnlTargetPreview);
            grpReplaceColor.Controls.Add(btnPickTarget);
            grpReplaceColor.Controls.Add(lblTR);
            grpReplaceColor.Controls.Add(numTR);
            grpReplaceColor.Controls.Add(lblTG);
            grpReplaceColor.Controls.Add(numTG);
            grpReplaceColor.Controls.Add(lblTB);
            grpReplaceColor.Controls.Add(numTB);
            grpReplaceColor.Controls.Add(lblNewColor);
            grpReplaceColor.Controls.Add(pnlNewPreview);
            grpReplaceColor.Controls.Add(btnPickNew);
            grpReplaceColor.Controls.Add(lblNR);
            grpReplaceColor.Controls.Add(numNR);
            grpReplaceColor.Controls.Add(lblNG);
            grpReplaceColor.Controls.Add(numNG);
            grpReplaceColor.Controls.Add(lblNB);
            grpReplaceColor.Controls.Add(numNB);
            grpReplaceColor.Controls.Add(lblTolerance);
            grpReplaceColor.Controls.Add(numTolerance);
            grpReplaceColor.Controls.Add(buttonReplaceColor);
            grpReplaceColor.Location = new Point(20, 485);
            grpReplaceColor.Name = "grpReplaceColor";
            grpReplaceColor.Size = new Size(1200, 105);
            grpReplaceColor.TabIndex = 11;
            grpReplaceColor.TabStop = false;
            grpReplaceColor.Text = "Заміна кольору";

            lblTargetColor.AutoSize = true;
            lblTargetColor.Location = new Point(15, 25);
            lblTargetColor.Name = "lblTargetColor";
            lblTargetColor.Size = new Size(63, 20);
            lblTargetColor.TabIndex = 0;
            lblTargetColor.Text = "Шукати:";

            btnPickTarget.Location = new Point(48, 50);
            btnPickTarget.Name = "btnPickTarget";
            btnPickTarget.Size = new Size(30, 29);
            btnPickTarget.TabIndex = 2;
            btnPickTarget.Text = "...";
            btnPickTarget.Click += btnPickTarget_Click;

            lblTR.Location = new Point(88, 55);
            lblTR.Name = "lblTR";
            lblTR.Size = new Size(18, 20);
            lblTR.TabIndex = 3;
            lblTR.Text = "R:";

            lblTG.Location = new Point(170, 55);
            lblTG.Name = "lblTG";
            lblTG.Size = new Size(18, 20);
            lblTG.TabIndex = 5;
            lblTG.Text = "G:";

            lblTB.Location = new Point(252, 55);
            lblTB.Name = "lblTB";
            lblTB.Size = new Size(18, 20);
            lblTB.TabIndex = 7;
            lblTB.Text = "B:";

            lblNewColor.AutoSize = true;
            lblNewColor.Location = new Point(360, 25);
            lblNewColor.Name = "lblNewColor";
            lblNewColor.Size = new Size(97, 20);
            lblNewColor.TabIndex = 9;
            lblNewColor.Text = "Замінити на:";

            btnPickNew.Location = new Point(393, 50);
            btnPickNew.Name = "btnPickNew";
            btnPickNew.Size = new Size(30, 29);
            btnPickNew.TabIndex = 11;
            btnPickNew.Text = "...";
            btnPickNew.Click += btnPickNew_Click;

            lblNR.Location = new Point(433, 55);
            lblNR.Name = "lblNR";
            lblNR.Size = new Size(18, 20);
            lblNR.TabIndex = 12;
            lblNR.Text = "R:";

            lblNG.Location = new Point(515, 55);
            lblNG.Name = "lblNG";
            lblNG.Size = new Size(18, 20);
            lblNG.TabIndex = 14;
            lblNG.Text = "G:";

            lblNB.Location = new Point(597, 55);
            lblNB.Name = "lblNB";
            lblNB.Size = new Size(18, 20);
            lblNB.TabIndex = 16;
            lblNB.Text = "B:";

            lblTolerance.AutoSize = true;
            lblTolerance.Location = new Point(700, 25);
            lblTolerance.Name = "lblTolerance";
            lblTolerance.Size = new Size(61, 20);
            lblTolerance.TabIndex = 18;
            lblTolerance.Text = "Допуск:";

            grpColorBalance.Controls.Add(lblR);
            grpColorBalance.Controls.Add(trkR);
            grpColorBalance.Controls.Add(lblG);
            grpColorBalance.Controls.Add(trkG);
            grpColorBalance.Controls.Add(lblB);
            grpColorBalance.Controls.Add(trkB);
            grpColorBalance.Controls.Add(lblBrightness);
            grpColorBalance.Controls.Add(trkBrightness);
            grpColorBalance.Controls.Add(buttonAdjustColor);
            grpColorBalance.Location = new Point(20, 485);
            grpColorBalance.Name = "grpColorBalance";
            grpColorBalance.Size = new Size(1200, 105);
            grpColorBalance.TabIndex = 12;
            grpColorBalance.TabStop = false;
            grpColorBalance.Text = "Колірний баланс та яскравість";

            lblR.AutoSize = true;
            lblR.Location = new Point(15, 25);
            lblR.Name = "lblR";
            lblR.Size = new Size(103, 20);
            lblR.TabIndex = 0;
            lblR.Text = "Червоний (R)";

            lblG.AutoSize = true;
            lblG.Location = new Point(245, 25);
            lblG.Name = "lblG";
            lblG.Size = new Size(92, 20);
            lblG.TabIndex = 2;
            lblG.Text = "Зелений (G)";

            lblB.AutoSize = true;
            lblB.Location = new Point(475, 25);
            lblB.Name = "lblB";
            lblB.Size = new Size(72, 20);
            lblB.TabIndex = 4;
            lblB.Text = "Синій (B)";

            lblBrightness.AutoSize = true;
            lblBrightness.Location = new Point(705, 25);
            lblBrightness.Name = "lblBrightness";
            lblBrightness.Size = new Size(82, 20);
            lblBrightness.TabIndex = 6;
            lblBrightness.Text = "Яскравість";

            grpTransparency.Controls.Add(lblAlpha);
            grpTransparency.Controls.Add(trkAlpha);
            grpTransparency.Controls.Add(buttonApplyTransparency);
            grpTransparency.Location = new Point(20, 485);
            grpTransparency.Name = "grpTransparency";
            grpTransparency.Size = new Size(590, 92);
            grpTransparency.TabIndex = 13;
            grpTransparency.TabStop = false;
            grpTransparency.Text = "Прозорість зображення";

            lblAlpha.AutoSize = true;
            lblAlpha.Location = new Point(15, 25);
            lblAlpha.Name = "lblAlpha";
            lblAlpha.Size = new Size(183, 20);
            lblAlpha.TabIndex = 0;
            lblAlpha.Text = "Непрозорість (альфа), %:";

            grpContrast.Controls.Add(lblContrast);
            grpContrast.Controls.Add(trkContrast);
            grpContrast.Controls.Add(buttonApplyContrast);
            grpContrast.Location = new Point(20, 485);
            grpContrast.Name = "grpContrast";
            grpContrast.Size = new Size(590, 92);
            grpContrast.TabIndex = 14;
            grpContrast.TabStop = false;
            grpContrast.Text = "Контрастність зображення";

            lblContrast.AutoSize = true;
            lblContrast.Location = new Point(15, 25);
            lblContrast.Name = "lblContrast";
            lblContrast.Size = new Size(94, 20);
            lblContrast.TabIndex = 0;
            lblContrast.Text = "Контраст, %:";

            grpCrop.Controls.Add(lblSplitHint);
            grpCrop.Controls.Add(lblSplitRows);
            grpCrop.Controls.Add(numSplitRows);
            grpCrop.Controls.Add(lblSplitCols);
            grpCrop.Controls.Add(numSplitCols);
            grpCrop.Controls.Add(buttonSplitParts);
            grpCrop.Controls.Add(lblCropSelectionHint);
            grpCrop.Controls.Add(buttonCropKeepSelection);
            grpCrop.Controls.Add(buttonRemoveSelection);
            grpCrop.Location = new Point(20, 485);
            grpCrop.Name = "grpCrop";
            grpCrop.Size = new Size(1200, 90);
            grpCrop.TabIndex = 15;
            grpCrop.TabStop = false;
            grpCrop.Text = "Кадрування зображення";

            lblSplitHint.AutoSize = true;
            lblSplitHint.Location = new Point(15, 27);
            lblSplitHint.Name = "lblSplitHint";
            lblSplitHint.Size = new Size(150, 20);
            lblSplitHint.TabIndex = 0;
            lblSplitHint.Text = "Розбити на частини:";

            lblSplitRows.AutoSize = true;
            lblSplitRows.Location = new Point(165, 27);
            lblSplitRows.Name = "lblSplitRows";
            lblSplitRows.Size = new Size(55, 20);
            lblSplitRows.TabIndex = 1;
            lblSplitRows.Text = "Рядків:";

            lblSplitCols.AutoSize = true;
            lblSplitCols.Location = new Point(295, 27);
            lblSplitCols.Name = "lblSplitCols";
            lblSplitCols.Size = new Size(74, 20);
            lblSplitCols.TabIndex = 3;
            lblSplitCols.Text = "Стовпців:";

            lblCropSelectionHint.AutoSize = true;
            lblCropSelectionHint.ForeColor = Color.DimGray;
            lblCropSelectionHint.Location = new Point(15, 62);
            lblCropSelectionHint.Name = "lblCropSelectionHint";
            lblCropSelectionHint.Size = new Size(335, 20);
            lblCropSelectionHint.TabIndex = 6;
            lblCropSelectionHint.Text = "Виділіть область на зображенні, потім оберіть дію:";

            grpMerge.Controls.Add(lblMergeFirst);
            grpMerge.Controls.Add(comboMergeFirst);
            grpMerge.Controls.Add(lblMergeSecond);
            grpMerge.Controls.Add(comboMergeSecond);
            grpMerge.Controls.Add(radioMergeHorizontal);
            grpMerge.Controls.Add(radioMergeVertical);
            grpMerge.Controls.Add(buttonMergeImages);
            grpMerge.Location = new Point(20, 485);
            grpMerge.Name = "grpMerge";
            grpMerge.Size = new Size(1200, 95);
            grpMerge.TabIndex = 16;
            grpMerge.TabStop = false;
            grpMerge.Text = "Об'єднання зображень";

            lblMergeFirst.AutoSize = true;
            lblMergeFirst.Location = new Point(15, 27);
            lblMergeFirst.Name = "lblMergeFirst";
            lblMergeFirst.Size = new Size(110, 20);
            lblMergeFirst.TabIndex = 0;
            lblMergeFirst.Text = "Зображення 1:";

            comboMergeFirst.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMergeFirst.FormattingEnabled = true;
            comboMergeFirst.Location = new Point(125, 24);
            comboMergeFirst.Name = "comboMergeFirst";
            comboMergeFirst.Size = new Size(230, 28);
            comboMergeFirst.TabIndex = 1;

            lblMergeSecond.AutoSize = true;
            lblMergeSecond.Location = new Point(390, 28);
            lblMergeSecond.Name = "lblMergeSecond";
            lblMergeSecond.Size = new Size(110, 20);
            lblMergeSecond.TabIndex = 2;
            lblMergeSecond.Text = "Зображення 2:";

            comboMergeSecond.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMergeSecond.FormattingEnabled = true;
            comboMergeSecond.Location = new Point(500, 24);
            comboMergeSecond.Name = "comboMergeSecond";
            comboMergeSecond.Size = new Size(230, 28);
            comboMergeSecond.TabIndex = 3;

            radioMergeHorizontal.AutoSize = true;
            radioMergeHorizontal.Checked = true;
            radioMergeHorizontal.Location = new Point(760, 27);
            radioMergeHorizontal.Name = "radioMergeHorizontal";
            radioMergeHorizontal.Size = new Size(116, 24);
            radioMergeHorizontal.TabIndex = 4;
            radioMergeHorizontal.TabStop = true;
            radioMergeHorizontal.Text = "Горизонтально";
            radioMergeHorizontal.UseVisualStyleBackColor = true;

            radioMergeVertical.AutoSize = true;
            radioMergeVertical.Location = new Point(900, 27);
            radioMergeVertical.Name = "radioMergeVertical";
            radioMergeVertical.Size = new Size(98, 24);
            radioMergeVertical.TabIndex = 5;
            radioMergeVertical.Text = "Вертикально";
            radioMergeVertical.UseVisualStyleBackColor = true;

            buttonMergeImages.Location = new Point(1040, 22);
            buttonMergeImages.Name = "buttonMergeImages";
            buttonMergeImages.Size = new Size(145, 38);
            buttonMergeImages.TabIndex = 6;
            buttonMergeImages.Text = "Об'єднати та зберегти";
            buttonMergeImages.UseVisualStyleBackColor = true;
            buttonMergeImages.Click += buttonMergeImages_Click;

            grpWatermark.Controls.Add(lblWatermarkText);
            grpWatermark.Controls.Add(txtWatermarkText);
            grpWatermark.Controls.Add(lblWatermarkPosition);
            grpWatermark.Controls.Add(comboWatermarkPosition);
            grpWatermark.Controls.Add(lblWatermarkSize);
            grpWatermark.Controls.Add(numWatermarkSize);
            grpWatermark.Controls.Add(lblWatermarkOpacity);
            grpWatermark.Controls.Add(trkWatermarkOpacity);
            grpWatermark.Controls.Add(btnWatermarkColor);
            grpWatermark.Controls.Add(pnlWatermarkColor);
            grpWatermark.Controls.Add(chkWatermarkBold);
            grpWatermark.Controls.Add(chkWatermarkItalic);
            grpWatermark.Controls.Add(buttonApplyWatermark);
            grpWatermark.Controls.Add(buttonResetOriginal);
            grpWatermark.Controls.Add(chkWatermarkCoordinates);
            grpWatermark.Controls.Add(lblWatermarkX);
            grpWatermark.Controls.Add(numWatermarkX);
            grpWatermark.Controls.Add(lblWatermarkY);
            grpWatermark.Controls.Add(numWatermarkY);
            grpWatermark.Location = new Point(20, 485);
            grpWatermark.Name = "grpWatermark";
            grpWatermark.Size = new Size(1200, 125);
            grpWatermark.TabIndex = 17;
            grpWatermark.TabStop = false;
            grpWatermark.Text = "Водяний знак";

            lblWatermarkText.AutoSize = true;
            lblWatermarkText.Location = new Point(12, 27);
            lblWatermarkText.Name = "lblWatermarkText";
            lblWatermarkText.Size = new Size(42, 20);
            lblWatermarkText.TabIndex = 0;
            lblWatermarkText.Text = "Текст:";

            txtWatermarkText.Location = new Point(85, 24);
            txtWatermarkText.Name = "txtWatermarkText";
            txtWatermarkText.Size = new Size(260, 27);
            txtWatermarkText.TabIndex = 1;
            txtWatermarkText.Text = "Водяний знак";

            lblWatermarkPosition.AutoSize = true;
            lblWatermarkPosition.Location = new Point(370, 28);
            lblWatermarkPosition.Name = "lblWatermarkPosition";
            lblWatermarkPosition.Size = new Size(65, 20);
            lblWatermarkPosition.TabIndex = 2;
            lblWatermarkPosition.Text = "Позиція:";

            comboWatermarkPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboWatermarkPosition.FormattingEnabled = true;
            comboWatermarkPosition.Items.AddRange(new object[] {
            "Верх ліворуч", "Верх по центру", "Верх праворуч",
            "По центру ліворуч", "По центру", "По центру праворуч",
            "Внизу ліворуч", "Внизу по центру", "Внизу праворуч"});
            comboWatermarkPosition.Location = new Point(445, 24);
            comboWatermarkPosition.Name = "comboWatermarkPosition";
            comboWatermarkPosition.Size = new Size(115, 28);
            comboWatermarkPosition.TabIndex = 3;

            lblWatermarkSize.AutoSize = true;
            lblWatermarkSize.Location = new Point(610, 28);
            lblWatermarkSize.Name = "lblWatermarkSize";
            lblWatermarkSize.Size = new Size(54, 20);
            lblWatermarkSize.TabIndex = 4;
            lblWatermarkSize.Text = "Розмір:";

            numWatermarkSize.Location = new Point(680, 24);
            numWatermarkSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numWatermarkSize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            numWatermarkSize.Name = "numWatermarkSize";
            numWatermarkSize.Size = new Size(70, 27);
            numWatermarkSize.TabIndex = 5;
            numWatermarkSize.Value = new decimal(new int[] { 32, 0, 0, 0 });

            lblWatermarkOpacity.AutoSize = true;
            lblWatermarkOpacity.Location = new Point(15, 68);
            lblWatermarkOpacity.Name = "lblWatermarkOpacity";
            lblWatermarkOpacity.Size = new Size(70, 20);
            lblWatermarkOpacity.TabIndex = 6;
            lblWatermarkOpacity.Text = "Непроз.:";

            trkWatermarkOpacity.Location = new Point(85, 62);
            trkWatermarkOpacity.Maximum = 100;
            trkWatermarkOpacity.Minimum = 0;
            trkWatermarkOpacity.Name = "trkWatermarkOpacity";
            trkWatermarkOpacity.Size = new Size(120, 35);
            trkWatermarkOpacity.TabIndex = 7;
            trkWatermarkOpacity.Value = 50;

            btnWatermarkColor.Location = new Point(225, 65);
            btnWatermarkColor.Name = "btnWatermarkColor";
            btnWatermarkColor.Size = new Size(95, 30);
            btnWatermarkColor.TabIndex = 8;
            btnWatermarkColor.Text = "Колір";
            btnWatermarkColor.UseVisualStyleBackColor = true;
            btnWatermarkColor.Click += btnWatermarkColor_Click;

            pnlWatermarkColor.BackColor = Color.Red;
            pnlWatermarkColor.BorderStyle = BorderStyle.FixedSingle;
            pnlWatermarkColor.Location = new Point(325, 65);
            pnlWatermarkColor.Name = "pnlWatermarkColor";
            pnlWatermarkColor.Size = new Size(30, 30);
            pnlWatermarkColor.TabIndex = 9;

            chkWatermarkBold.AutoSize = true;
            chkWatermarkBold.Location = new Point(375, 68);
            chkWatermarkBold.Name = "chkWatermarkBold";
            chkWatermarkBold.Size = new Size(81, 24);
            chkWatermarkBold.TabIndex = 10;
            chkWatermarkBold.Text = "Жирний";
            chkWatermarkBold.UseVisualStyleBackColor = true;

            chkWatermarkItalic.AutoSize = true;
            chkWatermarkItalic.Location = new Point(480, 68);
            chkWatermarkItalic.Name = "chkWatermarkItalic";
            chkWatermarkItalic.Size = new Size(86, 24);
            chkWatermarkItalic.TabIndex = 11;
            chkWatermarkItalic.Text = "Курсив";
            chkWatermarkItalic.UseVisualStyleBackColor = true;

            chkWatermarkCoordinates.AutoSize = true;
            chkWatermarkCoordinates.Location = new Point(770, 26);
            chkWatermarkCoordinates.Name = "chkWatermarkCoordinates";
            chkWatermarkCoordinates.Size = new Size(145, 24);
            chkWatermarkCoordinates.TabIndex = 12;
            chkWatermarkCoordinates.Text = "За координатами";
            chkWatermarkCoordinates.UseVisualStyleBackColor = true;

            lblWatermarkX.AutoSize = true;
            lblWatermarkX.Location = new Point(15, 102);
            lblWatermarkX.Name = "lblWatermarkX";
            lblWatermarkX.Size = new Size(22, 20);
            lblWatermarkX.TabIndex = 13;
            lblWatermarkX.Text = "X:";

            numWatermarkX.Location = new Point(42, 99);
            numWatermarkX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numWatermarkX.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numWatermarkX.Name = "numWatermarkX";
            numWatermarkX.Size = new Size(75, 27);
            numWatermarkX.TabIndex = 14;

            lblWatermarkY.AutoSize = true;
            lblWatermarkY.Location = new Point(130, 102);
            lblWatermarkY.Name = "lblWatermarkY";
            lblWatermarkY.Size = new Size(22, 20);
            lblWatermarkY.TabIndex = 15;
            lblWatermarkY.Text = "Y:";

            numWatermarkY.Location = new Point(157, 99);
            numWatermarkY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numWatermarkY.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numWatermarkY.Name = "numWatermarkY";
            numWatermarkY.Size = new Size(75, 27);
            numWatermarkY.TabIndex = 16;

            buttonApplyWatermark.Location = new Point(880, 88);
            buttonApplyWatermark.Name = "buttonApplyWatermark";
            buttonApplyWatermark.Size = new Size(145, 34);
            buttonApplyWatermark.TabIndex = 17;
            buttonApplyWatermark.Text = "Додати та зберегти";
            buttonApplyWatermark.UseVisualStyleBackColor = true;
            buttonApplyWatermark.Click += buttonApplyWatermark_Click;

            buttonResetOriginal.Location = new Point(1030, 88);
            buttonResetOriginal.Name = "buttonResetOriginal";
            buttonResetOriginal.Size = new Size(145, 34);
            buttonResetOriginal.TabIndex = 18;
            buttonResetOriginal.Text = "Скинути оригінал";
            buttonResetOriginal.UseVisualStyleBackColor = true;
            buttonResetOriginal.Click += buttonResetOriginal_Click;

            grpSlideshow.Controls.Add(lblSlideshowDelay);
            grpSlideshow.Controls.Add(numSlideshowDelay);
            grpSlideshow.Controls.Add(buttonSlideshowStart);
            grpSlideshow.Controls.Add(buttonSlideshowStop);
            grpSlideshow.Controls.Add(lblSlideshowHint);
            grpSlideshow.Location = new Point(20, 485);
            grpSlideshow.Name = "grpSlideshow";
            grpSlideshow.Size = new Size(1200, 90);
            grpSlideshow.TabIndex = 18;
            grpSlideshow.TabStop = false;
            grpSlideshow.Text = "Слайд-шоу";

            lblSlideshowDelay.AutoSize = true;
            lblSlideshowDelay.Location = new Point(15, 30);
            lblSlideshowDelay.Name = "lblSlideshowDelay";
            lblSlideshowDelay.Size = new Size(102, 20);
            lblSlideshowDelay.TabIndex = 0;
            lblSlideshowDelay.Text = "Затримка, мс:";

            numSlideshowDelay.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numSlideshowDelay.Location = new Point(125, 27);
            numSlideshowDelay.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            numSlideshowDelay.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            numSlideshowDelay.Name = "numSlideshowDelay";
            numSlideshowDelay.Size = new Size(80, 27);
            numSlideshowDelay.TabIndex = 1;
            numSlideshowDelay.Value = new decimal(new int[] { 1500, 0, 0, 0 });

            buttonSlideshowStart.Location = new Point(250, 24);
            buttonSlideshowStart.Name = "buttonSlideshowStart";
            buttonSlideshowStart.Size = new Size(100, 34);
            buttonSlideshowStart.TabIndex = 2;
            buttonSlideshowStart.Text = "▶ Запустити";
            buttonSlideshowStart.UseVisualStyleBackColor = true;
            buttonSlideshowStart.Click += buttonSlideshowStart_Click;

            buttonSlideshowStop.Location = new Point(360, 24);
            buttonSlideshowStop.Name = "buttonSlideshowStop";
            buttonSlideshowStop.Size = new Size(100, 34);
            buttonSlideshowStop.TabIndex = 3;
            buttonSlideshowStop.Text = "■ Зупинити";
            buttonSlideshowStop.UseVisualStyleBackColor = true;
            buttonSlideshowStop.Click += buttonSlideshowStop_Click;

            lblSlideshowHint.AutoSize = false;
            lblSlideshowHint.ForeColor = Color.DimGray;
            lblSlideshowHint.Location = new Point(500, 30);
            lblSlideshowHint.Name = "lblSlideshowHint";
            lblSlideshowHint.Size = new Size(500, 20);
            lblSlideshowHint.TabIndex = 4;
            lblSlideshowHint.Text = "Перегляд іде у вікні праворуч.";

            grpOriginalColor.Location = new Point(20, 485);
            grpOriginalColor.Name = "grpOriginalColor";
            grpOriginalColor.Size = new Size(1200, 80);
            grpOriginalColor.TabIndex = 19;
            grpOriginalColor.TabStop = false;
            grpOriginalColor.Text = "Оригінал";

            lblOriginalColorHint.AutoSize = false;
            lblOriginalColorHint.Location = new Point(15, 25);
            lblOriginalColorHint.Name = "lblOriginalColorHint";
            lblOriginalColorHint.Size = new Size(1160, 35);
            lblOriginalColorHint.TabIndex = 0;
            lblOriginalColorHint.Text = "Показ поточного кольорового зображення у вікні попереднього перегляду.";
            grpOriginalColor.Controls.Add(lblOriginalColorHint);

            grpBrightnessMatrix.Location = new Point(20, 485);
            grpBrightnessMatrix.Name = "grpBrightnessMatrix";
            grpBrightnessMatrix.Size = new Size(1200, 220);
            grpBrightnessMatrix.TabIndex = 20;
            grpBrightnessMatrix.TabStop = false;
            grpBrightnessMatrix.Text = "Матриця значень яскравості";

            txtBrightnessMatrix.Font = new Font("Consolas", 8.5F);
            txtBrightnessMatrix.Location = new Point(15, 25);
            txtBrightnessMatrix.Multiline = true;
            txtBrightnessMatrix.Name = "txtBrightnessMatrix";
            txtBrightnessMatrix.ReadOnly = true;
            txtBrightnessMatrix.ScrollBars = ScrollBars.Both;
            txtBrightnessMatrix.Size = new Size(1165, 175);
            txtBrightnessMatrix.TabIndex = 0;
            txtBrightnessMatrix.WordWrap = false;
            grpBrightnessMatrix.Controls.Add(txtBrightnessMatrix);

            grpBrightnessHistogram.Location = new Point(20, 485);
            grpBrightnessHistogram.Name = "grpBrightnessHistogram";
            grpBrightnessHistogram.Size = new Size(1200, 220);
            grpBrightnessHistogram.TabIndex = 21;
            grpBrightnessHistogram.TabStop = false;
            grpBrightnessHistogram.Text = "Гістограма яскравості кольорового зображення";

            pnlBrightnessHistogram.BorderStyle = BorderStyle.FixedSingle;
            pnlBrightnessHistogram.Location = new Point(15, 25);
            pnlBrightnessHistogram.Name = "pnlBrightnessHistogram";
            pnlBrightnessHistogram.Size = new Size(1165, 155);
            pnlBrightnessHistogram.TabIndex = 0;
            pnlBrightnessHistogram.BackColor = Color.White;
            pnlBrightnessHistogram.Paint += HistogramPanel_Paint;

            lblBrightnessStats.AutoSize = false;
            lblBrightnessStats.Location = new Point(15, 190);
            lblBrightnessStats.Name = "lblBrightnessStats";
            lblBrightnessStats.Size = new Size(1160, 22);
            lblBrightnessStats.TabIndex = 1;
            grpBrightnessHistogram.Controls.Add(pnlBrightnessHistogram);
            grpBrightnessHistogram.Controls.Add(lblBrightnessStats);

            grpColorTransforms.Location = new Point(20, 485);
            grpColorTransforms.Name = "grpColorTransforms";
            grpColorTransforms.Size = new Size(1200, 270);
            grpColorTransforms.TabIndex = 22;
            grpColorTransforms.TabStop = false;
            grpColorTransforms.Text = "Кольорові ефекти";

            buttonBinary.Location = new Point(15, 25);
            buttonBinary.Name = "buttonBinary";
            buttonBinary.Size = new Size(120, 34);
            buttonBinary.TabIndex = 0;
            buttonBinary.Text = "Бінаризація";
            buttonBinary.UseVisualStyleBackColor = true;
            buttonBinary.Click += buttonBinary_Click;

            buttonGrayscale.Location = new Point(145, 25);
            buttonGrayscale.Name = "buttonGrayscale";
            buttonGrayscale.Size = new Size(130, 34);
            buttonGrayscale.TabIndex = 1;
            buttonGrayscale.Text = "Відтінки сірого";
            buttonGrayscale.UseVisualStyleBackColor = true;
            buttonGrayscale.Click += buttonGrayscale_Click;

            buttonNegative.Location = new Point(290, 25);
            buttonNegative.Name = "buttonNegative";
            buttonNegative.Size = new Size(120, 34);
            buttonNegative.TabIndex = 2;
            buttonNegative.Text = "Негатив";
            buttonNegative.UseVisualStyleBackColor = true;
            buttonNegative.Click += buttonNegative_Click;

            buttonSaveColorTransform.Location = new Point(425, 25);
            buttonSaveColorTransform.Name = "buttonSaveColorTransform";
            buttonSaveColorTransform.Size = new Size(155, 34);
            buttonSaveColorTransform.TabIndex = 3;
            buttonSaveColorTransform.Text = "Зберегти результат";
            buttonSaveColorTransform.UseVisualStyleBackColor = true;
            buttonSaveColorTransform.Click += buttonSaveColorTransform_Click;

            pnlGrayHistogram.BorderStyle = BorderStyle.FixedSingle;
            pnlGrayHistogram.Location = new Point(15, 70);
            pnlGrayHistogram.Name = "pnlGrayHistogram";
            pnlGrayHistogram.Size = new Size(1165, 155);
            pnlGrayHistogram.TabIndex = 4;
            pnlGrayHistogram.BackColor = Color.White;
            pnlGrayHistogram.Paint += HistogramPanel_Paint;

            lblGrayStats.AutoSize = false;
            lblGrayStats.Location = new Point(15, 235);
            lblGrayStats.Name = "lblGrayStats";
            lblGrayStats.Size = new Size(1160, 22);
            lblGrayStats.TabIndex = 5;
            grpColorTransforms.Controls.Add(buttonBinary);
            grpColorTransforms.Controls.Add(buttonGrayscale);
            grpColorTransforms.Controls.Add(buttonNegative);
            grpColorTransforms.Controls.Add(buttonSaveColorTransform);
            grpColorTransforms.Controls.Add(pnlGrayHistogram);
            grpColorTransforms.Controls.Add(lblGrayStats);

            //
            // grpNoiseFiltering
            //
            grpNoiseFiltering.Location = new Point(20, 485);
            grpNoiseFiltering.Name = "grpNoiseFiltering";
            grpNoiseFiltering.Size = new Size(1200, 120);
            grpNoiseFiltering.TabIndex = 24;
            grpNoiseFiltering.TabStop = false;
            grpNoiseFiltering.Text = "Зменшення шуму";

            //
            // lblNoiseFilter
            //
            lblNoiseFilter.AutoSize = true;
            lblNoiseFilter.Location = new Point(15, 31);
            lblNoiseFilter.Text = "Фільтр:";

            //
            // comboNoiseFilter
            //
            comboNoiseFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboNoiseFilter.Location = new Point(80, 27);
            comboNoiseFilter.Size = new Size(260, 28);

            comboNoiseFilter.Items.AddRange(new object[]
            {
    "Усереднювальний 3×3",
    "Гауссівський 3×3",
    "Гауссівський 5×5",
    "Медіанний 3×3",
    "Медіанний 5×5",
    "Мінімальний 3×3",
    "Максимальний 3×3",
    "Серединний (Midpoint) 3×3",
    "Alpha-trimmed mean 3×3",
    "Білатеральний"
            });

            comboNoiseFilter.SelectedIndex = 0;
            comboNoiseFilter.SelectedIndexChanged += comboNoiseFilter_SelectedIndexChanged;

            //
            // buttonApplyNoiseFilter
            //
            buttonApplyNoiseFilter.Location = new Point(360, 25);
            buttonApplyNoiseFilter.Size = new Size(170, 34);
            buttonApplyNoiseFilter.Text = "Застосувати";
            buttonApplyNoiseFilter.UseVisualStyleBackColor = true;
            buttonApplyNoiseFilter.Click += buttonApplyNoiseFilter_Click;

            //
            // buttonRunAllFilters
            //
            buttonRunAllFilters.Location = new Point(545, 25);
            buttonRunAllFilters.Size = new Size(210, 34);
            buttonRunAllFilters.Text = "Перевірити всі фільтри";
            buttonRunAllFilters.UseVisualStyleBackColor = true;
            buttonRunAllFilters.Click += buttonRunAllFilters_Click;

            //
            // lblNoiseMse
            //
            lblNoiseMse.AutoSize = true;
            lblNoiseMse.Location = new Point(775, 31);
            lblNoiseMse.Text = "ε²:";

            //
            // textNoiseMse
            //
            textNoiseMse.Location = new Point(810, 27);
            textNoiseMse.Size = new Size(150, 27);
            textNoiseMse.ReadOnly = true;

            grpNoiseFiltering.Controls.Add(lblNoiseFilter);
            grpNoiseFiltering.Controls.Add(comboNoiseFilter);
            grpNoiseFiltering.Controls.Add(buttonApplyNoiseFilter);
            grpNoiseFiltering.Controls.Add(buttonRunAllFilters);
            grpNoiseFiltering.Controls.Add(lblNoiseMse);
            grpNoiseFiltering.Controls.Add(textNoiseMse);

            Controls.Add(grpNoiseFiltering);

            Controls.Add(buttonSelectPicture);
            Controls.Add(lblFilesList);
            Controls.Add(listSelectFile);
            Controls.Add(comboAction);
            Controls.Add(lblAction);
            Controls.Add(lblPreview);
            Controls.Add(buttonResetToOriginal);
            Controls.Add(picturePreview);
            Controls.Add(lblPickHint);
            Controls.Add(lblLog);
            Controls.Add(textLog);
            Controls.Add(grpConvert);
            Controls.Add(grpResize);
            Controls.Add(grpReplaceColor);
            Controls.Add(grpColorBalance);
            Controls.Add(grpTransparency);
            Controls.Add(grpContrast);
            Controls.Add(grpCrop);
            Controls.Add(grpMerge);
            Controls.Add(grpWatermark);
            Controls.Add(grpSlideshow);
            Controls.Add(grpOriginalColor);
            Controls.Add(grpBrightnessMatrix);
            Controls.Add(grpBrightnessHistogram);
            grpSigmaFilter.Location = new Point(20, 485);
            grpSigmaFilter.Name = "grpSigmaFilter";
            grpSigmaFilter.Size = new Size(1200, 100);
            grpSigmaFilter.TabIndex = 23;
            grpSigmaFilter.TabStop = false;
            grpSigmaFilter.Text = "Фільтрація зображення — сигма-фільтр";

            lblSigma.AutoSize = true;
            lblSigma.Location = new Point(15, 30);
            lblSigma.Name = "lblSigma";
            lblSigma.Size = new Size(110, 20);
            lblSigma.TabIndex = 0;
            lblSigma.Text = "Значення σ:";

            numSigma.DecimalPlaces = 1;
            numSigma.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSigma.Location = new Point(125, 27);
            numSigma.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            numSigma.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSigma.Name = "numSigma";
            numSigma.Size = new Size(90, 27);
            numSigma.TabIndex = 1;
            numSigma.Value = new decimal(new int[] { 20, 0, 0, 0 });

            buttonSaveSigmaFilter.Location = new Point(235, 25);
            buttonSaveSigmaFilter.Name = "buttonSaveSigmaFilter";
            buttonSaveSigmaFilter.Size = new Size(155, 34);
            buttonSaveSigmaFilter.TabIndex = 2;
            buttonSaveSigmaFilter.Text = "Зберегти результат";
            buttonSaveSigmaFilter.UseVisualStyleBackColor = true;
            buttonSaveSigmaFilter.Click += buttonSaveSigmaFilter_Click;

            lblSigmaHint.AutoSize = true;
            lblSigmaHint.Location = new Point(410, 30);
            lblSigmaHint.Name = "lblSigmaHint";
            lblSigmaHint.Size = new Size(650, 20);
            lblSigmaHint.TabIndex = 3;
            lblSigmaHint.Text = "3×3: усереднюються тільки сусідні пікселі з близькою яскравістю.";

            grpSigmaFilter.Controls.Add(lblSigma);
            grpSigmaFilter.Controls.Add(numSigma);
            grpSigmaFilter.Controls.Add(buttonSaveSigmaFilter);
            grpSigmaFilter.Controls.Add(lblSigmaHint);

            Controls.Add(grpColorTransforms);
            Controls.Add(grpSigmaFilter);
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            Name = "EditorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Обробник фотографій";
            WindowState = FormWindowState.Maximized;
            Load += EditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTR).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTG).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNR).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNG).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTolerance).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkR).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkG).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkB).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSplitRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSplitCols).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkWatermarkOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSlideshowDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWatermarkY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSigma).EndInit();
            grpConvert.ResumeLayout(false);
            grpConvert.PerformLayout();
            grpResize.ResumeLayout(false);
            grpResize.PerformLayout();
            grpReplaceColor.ResumeLayout(false);
            grpReplaceColor.PerformLayout();
            grpColorBalance.ResumeLayout(false);
            grpColorBalance.PerformLayout();
            grpTransparency.ResumeLayout(false);
            grpTransparency.PerformLayout();
            grpContrast.ResumeLayout(false);
            grpContrast.PerformLayout();
            grpCrop.ResumeLayout(false);
            grpCrop.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSelectPicture;
        private Label lblAction;
        private ComboBox comboAction;
        private ListBox listSelectFile;
        private PictureBox picturePreview;
        private TextBox textLog;
        private ComboBox comboFormat;
        private Button buttonConvertFormat;
        private NumericUpDown numberWidth;
        private NumericUpDown numberHeight;
        private CheckBox checkKeepAspect;
        private Button buttonResize;
        private NumericUpDown numTR;
        private NumericUpDown numTG;
        private NumericUpDown numTB;
        private NumericUpDown numNR;
        private NumericUpDown numNG;
        private NumericUpDown numNB;
        private NumericUpDown numTolerance;
        private Button buttonReplaceColor;
        private TrackBar trkR;
        private TrackBar trkG;
        private TrackBar trkB;
        private TrackBar trkBrightness;
        private Button buttonAdjustColor;
        private TrackBar trkAlpha;
        private Button buttonApplyTransparency;
        private TrackBar trkContrast;
        private Button buttonApplyContrast;
        private NumericUpDown numSplitRows;
        private NumericUpDown numSplitCols;
        private Button buttonSplitParts;
        private Button buttonCropKeepSelection;
        private Button buttonRemoveSelection;
        private Panel pnlTargetPreview;
        private Panel pnlNewPreview;
        private GroupBox grpConvert;
        private Label lblFormat;
        private GroupBox grpResize;
        private Label lblWidth;
        private Label lblHeight;
        private GroupBox grpReplaceColor;
        private Label lblTargetColor;
        private Button btnPickTarget;
        private Label lblTR;
        private Label lblTG;
        private Label lblTB;
        private Label lblNewColor;
        private Button btnPickNew;
        private Label lblNR;
        private Label lblNG;
        private Label lblNB;
        private Label lblTolerance;
        private GroupBox grpColorBalance;
        private Label lblR;
        private Label lblG;
        private Label lblB;
        private Label lblBrightness;
        private GroupBox grpTransparency;
        private Label lblAlpha;
        private GroupBox grpContrast;
        private Label lblContrast;
        private GroupBox grpCrop;
        private Label lblSplitHint;
        private Label lblSplitRows;
        private Label lblSplitCols;
        private Label lblCropSelectionHint;

        private GroupBox grpMerge;
        private Label lblMergeFirst;
        private ComboBox comboMergeFirst;
        private Label lblMergeSecond;
        private ComboBox comboMergeSecond;
        private RadioButton radioMergeHorizontal;
        private RadioButton radioMergeVertical;
        private Button buttonMergeImages;
        private GroupBox grpWatermark;
        private Label lblWatermarkText;
        private TextBox txtWatermarkText;
        private Label lblWatermarkPosition;
        private ComboBox comboWatermarkPosition;
        private Label lblWatermarkSize;
        private NumericUpDown numWatermarkSize;
        private Label lblWatermarkOpacity;
        private TrackBar trkWatermarkOpacity;
        private Button btnWatermarkColor;
        private Panel pnlWatermarkColor;
        private CheckBox chkWatermarkBold;
        private CheckBox chkWatermarkItalic;
        private Button buttonApplyWatermark;
        private CheckBox chkWatermarkCoordinates;
        private Label lblWatermarkX;
        private NumericUpDown numWatermarkX;
        private Label lblWatermarkY;
        private NumericUpDown numWatermarkY;
        private Button buttonResetOriginal;
        private Button buttonResetToOriginal;
        private GroupBox grpSlideshow;
        private Label lblSlideshowDelay;
        private NumericUpDown numSlideshowDelay;
        private Button buttonSlideshowStart;
        private Button buttonSlideshowStop;
        private Label lblSlideshowHint;
        private GroupBox grpOriginalColor;
        private Label lblOriginalColorHint;
        private GroupBox grpBrightnessMatrix;
        private TextBox txtBrightnessMatrix;
        private GroupBox grpBrightnessHistogram;
        private Panel pnlBrightnessHistogram;
        private Label lblBrightnessStats;
        private GroupBox grpColorTransforms;
        private Button buttonBinary;
        private Button buttonGrayscale;
        private Button buttonNegative;
        private Button buttonSaveColorTransform;
        private Panel pnlGrayHistogram;
        private Label lblGrayStats;
        private GroupBox grpSigmaFilter;
        private Label lblSigma;
        private NumericUpDown numSigma;
        private Button buttonSaveSigmaFilter;
        private Label lblSigmaHint;
        private Label lblFilesList;
        private Label lblPreview;
        private Label lblPickHint;
        private Label lblLog;
        private GroupBox grpNoiseFiltering;
        private ComboBox comboNoiseFilter;
        private Label lblNoiseFilter;
        private Button buttonApplyNoiseFilter;
        private Button buttonRunAllFilters;
        private Label lblNoiseMse;
        private TextBox textNoiseMse;
    }
}
