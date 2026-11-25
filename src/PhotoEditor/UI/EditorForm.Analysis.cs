using System.Drawing.Imaging;
using System.Text;
using PhotoEditor.Imaging;

namespace PhotoEditor.UI;

public partial class EditorForm
{
    private void InitializeNoiseResultsUi()
    {
        grpNoiseFiltering.Size = new System.Drawing.Size(1200, 390);

        buttonSaveNoiseResult = new Button
        {
            Location = new Point(15, 70),
            Size = new System.Drawing.Size(190, 34),
            Text = "Зберегти результат BMP",
            UseVisualStyleBackColor = true
        };
        buttonSaveNoiseResult.Click += buttonSaveNoiseResult_Click;

        buttonProcessAllNoiseImages = new Button
        {
            Location = new Point(215, 70),
            Size = new System.Drawing.Size(190, 34),
            Text = "Обробити вибрані фото",
            UseVisualStyleBackColor = true
        };
        buttonProcessAllNoiseImages.Click += buttonProcessAllNoiseImages_Click;

        buttonSaveNoiseCsv = new Button
        {
            Location = new Point(415, 70),
            Size = new System.Drawing.Size(190, 34),
            Text = "Зберегти таблицю CSV",
            UseVisualStyleBackColor = true
        };
        buttonSaveNoiseCsv.Click += buttonSaveNoiseCsv_Click;

        dgvNoiseResults = new DataGridView
        {
            Location = new Point(15, 115),
            Size = new System.Drawing.Size(1170, 255),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false
        };

        dgvNoiseResults.Columns.Add("Image", "Зображення");
        dgvNoiseResults.Columns.Add("Filter", "Фільтр");
        dgvNoiseResults.Columns.Add("Type", "Тип фільтра");
        dgvNoiseResults.Columns.Add("MSE", "ε²");
        dgvNoiseResults.Columns.Add("File", "Збережений файл");

        grpNoiseFiltering.Controls.Add(buttonSaveNoiseResult);
        grpNoiseFiltering.Controls.Add(buttonProcessAllNoiseImages);
        grpNoiseFiltering.Controls.Add(buttonSaveNoiseCsv);
        grpNoiseFiltering.Controls.Add(dgvNoiseResults);
    }

    // Виведення первинного кольорового зображення на екран
    private void UpdateOriginalColorPreview()
    {
        // Перевірка, чи активована відповідна група елементів інтерфейсу
        if (!RequireAction(grpOriginalColor)) return;
        // Перевірка наявності завантаженого оригінального зображення
        if (originalBitmapForPreview == null) return;

        // Конвертація зображення у формат 32bpp та його встановлення у вікно попереднього перегляду
        SetPreviewImage(BitmapOperations.Ensure32bpp(originalBitmapForPreview));
    }

    // Формування та відображення матриці значень яскравості
    private void UpdateBrightnessMatrix()
    {
        // Перевірка необхідності виконання дії для даної групи елементів
        if (!RequireAction(grpBrightnessMatrix)) return;
        if (originalBitmapForPreview == null) return;

        // Вибір джерела: базове оброблене зображення або оригінальне
        Bitmap source = previewBaseBitmap != null
            ? BitmapOperations.Ensure32bpp(previewBaseBitmap)
            : BitmapOperations.Ensure32bpp(originalBitmapForPreview);

        // Оновлення перегляду для поточного джерела
        SetPreviewImage(BitmapOperations.Ensure32bpp(source));

        // Максимальна кількість колонок і рядків для виведення в матрицю
        int maxCols = 45;
        int maxRows = 28;

        // Обчислення кроку сканування пікселів по осі X та Y
        int stepX = Math.Max(1, (int)Math.Ceiling(source.Width / (double)maxCols));
        int stepY = Math.Max(1, (int)Math.Ceiling(source.Height / (double)maxRows));

        // Кінцева кількість колонок і рядків у сформованій матриці
        int cols = (int)Math.Ceiling(source.Width / (double)stepX);
        int rows = (int)Math.Ceiling(source.Height / (double)stepY);

        // Створення буфера для текстового представлення матриці
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine((UiLanguage.English ? $"Source size: {source.Width} × {source.Height}. Matrix preview: {rows} × {cols}." : $"Розмір джерела: {source.Width} × {source.Height}. Матриця preview: {rows} × {cols}."));
        sb.AppendLine((UiLanguage.English ? "Brightness = 0.299R + 0.587G + 0.114B; values 0..255." : "Яскравість = 0.299R + 0.587G + 0.114B; значення 0..255."));

        // Ітерація по зображенню з заданим кроком
        for (int y = 0; y < source.Height; y += stepY)
        {
            for (int x = 0; x < source.Width; x += stepX)
            {
                // Отримання яскравості пікселя та форматування значення в 3 цифри (наприклад, 005, 042, 255)
                sb.Append(BitmapOperations.GetBrightness(source.GetPixel(x, y)).ToString("D3"));
                sb.Append(' ');
            }
            sb.AppendLine(); // Перехід на новий рядок матриці
        }

        // Виведення сформованого тексту у текстове поле
        txtBrightnessMatrix.Text = sb.ToString();

        // Звільнення ресурсів об'єкта Bitmap
        source.Dispose();
    }

    // Побудова гістограми яскравості кольорового зображення
    private void UpdateBrightnessHistogram()
    {
        // Перевірка активності елемента керування та наявності зображення
        if (!RequireAction(grpBrightnessHistogram)) return;
        if (originalBitmapForPreview == null) return;

        // Вибір джерела зображення
        Bitmap source = previewBaseBitmap != null
            ? BitmapOperations.Ensure32bpp(previewBaseBitmap)
            : BitmapOperations.Ensure32bpp(originalBitmapForPreview);

        // Відображення джерела
        SetPreviewImage(BitmapOperations.Ensure32bpp(source));

        // Розрахунок даних гістограми та оновлення статистики
        BuildHistogram(source, pnlBrightnessHistogram, lblBrightnessStats);

        // Очищення виділених ресурсів
        source.Dispose();
    }

    // Оновлення попереднього перегляду після зміни кольоровості
    private void UpdateColorTransformPreview()
    {
        // Перевірка прав на виконання обробки
        if (!RequireAction(grpColorTransforms)) return;
        if (originalBitmapForPreview == null) return;

        // Визначення джерела зображення
        Bitmap source = previewBaseBitmap != null
            ? BitmapOperations.Ensure32bpp(previewBaseBitmap)
            : BitmapOperations.Ensure32bpp(originalBitmapForPreview);

        // Застосування обраного режиму перетворення (градації сірого, негатив, бінаризація)
        Bitmap result = BitmapOperations.ApplyColorTransform(source, activeColorTransform);

        // Виведення результату перетворення на екран
        SetPreviewImage(result);

        // Побудова гістограми для вже обробленого зображення
        BuildHistogram(result, pnlGrayHistogram, lblGrayStats);

        // Звільнення пам'яті джерела
        source.Dispose();
    }

    // Збір статистики та даних для побудови гістограми
    private void BuildHistogram(Bitmap source, Panel panel, Label stats)
    {
        int[] values = new int[256]; // Масив частот для кожного рівня яскравості (0..255)
        long sum = 0;                // Сума значень для розрахунку середньої яскравості
        int min = 255;               // Мінімальне значення яскравості
        int max = 0;                 // Максимальне значення яскравості

        // Прохід по всіх пікселях зображення
        for (int x = 0; x < source.Width; x++)
        {
            for (int y = 0; y < source.Height; y++)
            {
                int value = BitmapOperations.GetBrightness(source.GetPixel(x, y));
                values[value]++;
                sum += value;

                // Оновлення мінімуму та максимуму
                if (value < min) min = value;
                if (value > max) max = value;
            }
        }

        // Обчислення середньої яскравості зображення
        double average = source.Width * source.Height > 0
            ? (double)sum / (source.Width * source.Height)
            : 0;

        // Форматування та виведення статистичних даних у Label
        stats.Text = (UiLanguage.English ? $"Minimum: {min}   Maximum: {max}   Mean: {average:F1}   Pixels: {source.Width * source.Height:N0}" : $"Мінімум: {min}   Максимум: {max}   Середнє: {average:F1}   Пікселів: {source.Width * source.Height:N0}");

        // Збереження масиву частот у властивість Tag панелі для наступного малювання
        panel.Tag = values;

        // Примусовий виклик перемальовування панелі (подія Paint)
        panel.Invalidate();
    }

    // Відображення гістограми на панелі (графічне малювання)
    private void HistogramPanel_Paint(object sender, PaintEventArgs e)
    {
        Panel panel = sender as Panel;
        // Перевірка наявності даних у Tag
        if (panel == null || !(panel.Tag is int[] values)) return;

        // Пошук пикового значення (найбільшої кількості пікселів одного відтінку) для нормалізації
        int maxValue = 1;
        foreach (int value in values)
        {
            if (value > maxValue)
                maxValue = value;
        }

        // Розрахунок ширини одного стовпчика гістограми
        float barWidth = panel.ClientSize.Width / 256f;

        // Малювання стовпчиків гістограми
        using (Brush brush = new SolidBrush(Color.SteelBlue))
        {
            for (int i = 0; i < 256; i++)
            {
                // Масштабування висоти стовпчика відносно висоти панелі (з відступом 20px для підписів)
                float height = values[i] / (float)maxValue * (panel.ClientSize.Height - 20);
                e.Graphics.FillRectangle(
                    brush,
                    i * barWidth,
                    panel.ClientSize.Height - height,
                    Math.Max(1f, barWidth),
                    height);
            }
        }

        // Малювання нижньої осі гістограми
        using (Pen pen = new Pen(Color.Gray))
        {
            e.Graphics.DrawLine(
                pen,
                0,
                panel.ClientSize.Height - 1,
                panel.ClientSize.Width,
                panel.ClientSize.Height - 1);
        }

        // Нанесення текстових позначок значень яскравості (0, 128, 255)
        e.Graphics.DrawString("0", Font, Brushes.Black, 2, panel.ClientSize.Height - 18);
        e.Graphics.DrawString("128", Font, Brushes.Black, panel.ClientSize.Width / 2f - 12, panel.ClientSize.Height - 18);
        e.Graphics.DrawString("255", Font, Brushes.Black, panel.ClientSize.Width - 28, panel.ClientSize.Height - 18);
    }

    // Бінаризація зображення: перехід до чорно-білого
    private void buttonBinary_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpColorTransforms)) return;

        activeColorTransform = "binary";
        UpdateColorTransformPreview();
    }

    // Перетворення зображення у градації сірого
    private void buttonGrayscale_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpColorTransforms)) return;

        activeColorTransform = "grayscale";
        UpdateColorTransformPreview();
    }

    // Перетворення зображення у негатив
    private void buttonNegative_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpColorTransforms)) return;

        activeColorTransform = "negative";
        UpdateColorTransformPreview();
    }

    // Збереження результату перетворення кольоровості
    private void buttonSaveColorTransform_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpColorTransforms)) return;
        if (picturePreview.Image == null || listSelectFile.SelectedIndex < 0) return;

        // Отримання папки для збереження
        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        string filePath = selectedFiles[listSelectFile.SelectedIndex];
        string suffix = "_grayscale"; // Суфікс за замовчуванням

        // Визначення суфікса імені файлу відповідно до режиму
        if (activeColorTransform == "binary")
            suffix = "_binary";
        else if (activeColorTransform == "negative")
            suffix = "_negative";

        // Формування повного шляху для збереження файлу
        string savePath = Path.Combine(
            outputDir,
            Path.GetFileNameWithoutExtension(filePath) + suffix + Path.GetExtension(filePath));

        try
        {
            // Перетворення у формат 32bpp та збереження з вихідним форматом файлу
            using (Bitmap result = BitmapOperations.Ensure32bpp(picturePreview.Image))
            {
                result.Save(savePath, GetOriginalFormat(filePath));
            }

            // Фіксація поточного стану як базового та логування
            CaptureCurrentPreviewAsBase();
            Log((UiLanguage.English ? $"Current result saved: {Path.GetFileName(savePath)}" : $"Збережено результат для поточного зображення: {Path.GetFileName(savePath)}"));
        }
        catch (Exception ex)
        {
            // Логування помилки у разі збою запису
            Log((UiLanguage.English ? $"Could not save result: {ex.Message}" : $"Помилка збереження результату: {ex.Message}"));
        }
    }

    // Оновлення preview сигма-фільтра
    private void UpdateSigmaFilterPreview()
    {
        if (!IsActionSelected(grpSigmaFilter))
            return;

        if (listSelectFile.SelectedIndex < 0)
            return;

        try
        {
            string filePath = selectedFiles[listSelectFile.SelectedIndex];

            using (Bitmap source = new Bitmap(filePath))
            {
                double sigma = (double)numSigma.Value;

                Bitmap result = NoiseFilters.ApplySigmaFilter(source, sigma);

                SetPreviewImage(result);

                Log((UiLanguage.English ? $"Sigma filter: Sigma = {sigma:0.0}" : $"Сигма-фільтр: Sigma = {sigma:0.0}"));
            }
        }
        catch (Exception ex)
        {
            Log((UiLanguage.English ? $"Sigma filter error: {ex.Message}" : $"Помилка сигма-фільтра: {ex.Message}"));
        }
    }

    // Налаштування параметрів Sigma
    private void ConfigureSigma()
    {
        numSigma.Minimum = 0;
        numSigma.Maximum = 100;
        numSigma.DecimalPlaces = 1;
        numSigma.Increment = 0.5M;
    }

    // Збереження результату сигма-фільтра
    private void buttonSaveSigmaFilter_Click(object sender, EventArgs e)
    {
        if (!RequireAction(grpSigmaFilter)) return;
        if (picturePreview.Image == null || listSelectFile.SelectedIndex < 0) return;

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        string filePath = selectedFiles[listSelectFile.SelectedIndex];

        // Отримуємо значення sigma з вашого контролу на формі grpSigmaFilter
        // Замініть numSigma на назву вашого елемента (наприклад, numSigma.Value, txtSigma.Text тощо)
        double sigma = (double)numSigma.Value;

        //  Формуємо шлях із використанням змінної sigma
        string savePath = Path.Combine(
            outputDir,
            $"{Path.GetFileNameWithoutExtension(filePath)}_sigma_{sigma}{Path.GetExtension(filePath)}"
        );

        try
        {
            using (Bitmap result = BitmapOperations.Ensure32bpp(picturePreview.Image))
            {
                result.Save(savePath, GetOriginalFormat(filePath));
            }

            CaptureCurrentPreviewAsBase();
            Log((UiLanguage.English ? $"Sigma filter result saved: {Path.GetFileName(savePath)}" : $"Збережено результат сигма-фільтра: {Path.GetFileName(savePath)}"));
        }
        catch (Exception ex)
        {
            Log((UiLanguage.English ? $"Could not save sigma filter result: {ex.Message}" : $"Помилка збереження сигма-фільтра: {ex.Message}"));
        }
    }

    private void UpdateNoiseFilterPreview()
    {
        if (originalBitmapForPreview == null)
            return;

        int filterIndex = comboNoiseFilter.SelectedIndex;

        using Bitmap source =
            new Bitmap(originalBitmapForPreview);

        Bitmap result =
            NoiseFilters.ApplyNoiseFilter(source, filterIndex);

        double mse = ImageMetrics.CalculateMSE(source, result);
        textNoiseMse.Text = mse.ToString("F4");

        SetPreviewImage(result);

        Log(
            (UiLanguage.English ? $"Noise filter: {comboNoiseFilter.SelectedItem}" : $"Фільтр шуму: {comboNoiseFilter.SelectedItem}"));
    }

    private void comboNoiseFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (activeActionGroup == grpNoiseFiltering)
            UpdateNoiseFilterPreview();
    }

    private void buttonApplyNoiseFilter_Click(object sender, EventArgs e)
    {
        if (originalBitmapForPreview == null)
        {
            MessageBox.Show(
                (UiLanguage.English ? "First select an image." : "Спочатку виберіть зображення."));
            return;
        }

        UpdateNoiseFilterPreview();

        Log(
            (UiLanguage.English ? $"Applied: {comboNoiseFilter.SelectedItem}" : $"Застосовано: {comboNoiseFilter.SelectedItem}"));
    }

    private string GetNoiseFilterType(int index)
    {
        return index <= 2 ? (UiLanguage.English ? "Linear" : "Лінійний") : (UiLanguage.English ? "Nonlinear" : "Нелінійний");
    }

    private string SafeFilePart(string value)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            value = value.Replace(c, '_');
        return value.Replace("×", "x").Replace(" ", "_");
    }

    private void AddNoiseResultRow(string imageName, int filterIndex, double mse, string savedFile = "")
    {
        string filterName = comboNoiseFilter.Items[filterIndex]?.ToString() ?? (UiLanguage.English ? $"Filter {filterIndex + 1}" : $"Фільтр {filterIndex + 1}");
        dgvNoiseResults.Rows.Add(imageName, filterName, GetNoiseFilterType(filterIndex), mse.ToString("F4"), savedFile);
    }

    private void buttonRunAllFilters_Click(object sender, EventArgs e)
    {
        if (originalBitmapForPreview == null)
        {
            MessageBox.Show((UiLanguage.English ? "First select an image." : "Спочатку виберіть зображення."));
            return;
        }

        dgvNoiseResults.Rows.Clear();
        string imageName = listSelectFile.SelectedItem?.ToString() ?? (UiLanguage.English ? "current image" : "поточне зображення");

        using Bitmap source = new Bitmap(originalBitmapForPreview);
        for (int i = 0; i < comboNoiseFilter.Items.Count; i++)
        {
            using Bitmap result = NoiseFilters.ApplyNoiseFilter(source, i);
            double mse = ImageMetrics.CalculateMSE(source, result);
            AddNoiseResultRow(imageName, i, mse);
        }

        Log((UiLanguage.English ? $"Checked 10 filters for {imageName}. Results are shown in the table." : $"Перевірено 10 фільтрів для {imageName}. Результати виведено в таблицю."));
    }

    private void buttonSaveNoiseResult_Click(object? sender, EventArgs e)
    {
        if (picturePreview.Image == null || listSelectFile.SelectedIndex < 0)
        {
            MessageBox.Show((UiLanguage.English ? "Select an image and a filter." : "Спочатку виберіть зображення та фільтр."));
            return;
        }

        using SaveFileDialog dialog = new SaveFileDialog();
        string sourcePath = selectedFiles[listSelectFile.SelectedIndex];
        string baseName = Path.GetFileNameWithoutExtension(sourcePath);
        string filterName = SafeFilePart(comboNoiseFilter.SelectedItem?.ToString() ?? "filter");
        dialog.Filter = "BMP (*.bmp)|*.bmp";
        dialog.FileName = $"{baseName}_{filterName}.bmp";

        if (dialog.ShowDialog() != DialogResult.OK) return;

        using Bitmap result = BitmapOperations.Ensure32bpp(picturePreview.Image);
        result.Save(dialog.FileName, ImageFormat.Bmp);
        Log((UiLanguage.English ? $"Saved: {Path.GetFileName(dialog.FileName)}" : $"Збережено: {Path.GetFileName(dialog.FileName)}"));
    }

    private void buttonProcessAllNoiseImages_Click(object? sender, EventArgs e)
    {
        if (selectedFiles.Count == 0)
        {
            MessageBox.Show((UiLanguage.English ? "Open photos first." : "Спочатку відкрийте фотографії."));
            return;
        }

        string? outputDir = GetOutputFolder();
        if (string.IsNullOrEmpty(outputDir)) return;

        dgvNoiseResults.Rows.Clear();
        int savedCount = 0;

        foreach (string filePath in selectedFiles)
        {
            try
            {
                using Bitmap source = new Bitmap(filePath);
                string baseName = Path.GetFileNameWithoutExtension(filePath);

                for (int i = 0; i < comboNoiseFilter.Items.Count; i++)
                {
                    using Bitmap result = NoiseFilters.ApplyNoiseFilter(source, i);
                    double mse = ImageMetrics.CalculateMSE(source, result);
                    string filterName = comboNoiseFilter.Items[i]?.ToString() ?? $"filter_{i + 1}";
                    string outputName = $"{baseName}_{SafeFilePart(filterName)}.bmp";
                    string outputPath = Path.Combine(outputDir, outputName);
                    result.Save(outputPath, ImageFormat.Bmp);
                    AddNoiseResultRow(Path.GetFileName(filePath), i, mse, outputName);
                    savedCount++;
                }
            }
            catch (Exception ex)
            {
                Log((UiLanguage.English ? $"Error for {Path.GetFileName(filePath)}: {ex.Message}" : $"Помилка для {Path.GetFileName(filePath)}: {ex.Message}"));
            }
        }

        string csvPath = Path.Combine(outputDir, "noise_filter_results.csv");
        SaveNoiseTableToCsv(csvPath);
        Log((UiLanguage.English ? $"Done: saved {savedCount} BMP-files and the table {Path.GetFileName(csvPath)}." : $"Готово: збережено {savedCount} BMP-файлів і таблицю {Path.GetFileName(csvPath)}."));
        MessageBox.Show((UiLanguage.English ? $"Processing complete.\nSaved BMP: {savedCount}\nTable: {csvPath}" : $"Обробку завершено.\nЗбережено BMP: {savedCount}\nТаблиця: {csvPath}"));
    }

    private void buttonSaveNoiseCsv_Click(object? sender, EventArgs e)
    {
        if (dgvNoiseResults.Rows.Count == 0)
        {
            MessageBox.Show((UiLanguage.English ? "The table is empty. Run the filter comparison first." : "Таблиця порожня. Спочатку виконайте перевірку фільтрів."));
            return;
        }

        using SaveFileDialog dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = "noise_filter_results.csv"
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;

        SaveNoiseTableToCsv(dialog.FileName);
        Log((UiLanguage.English ? $"Table saved: {Path.GetFileName(dialog.FileName)}" : $"Таблицю збережено: {Path.GetFileName(dialog.FileName)}"));
    }

    private void SaveNoiseTableToCsv(string path)
    {
        using StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(true));
        writer.WriteLine((UiLanguage.English ? "Image;Filter;Filter type;ε²;Saved file" : "Зображення;Фільтр;Тип фільтра;ε²;Збережений файл"));

        foreach (DataGridViewRow row in dgvNoiseResults.Rows)
        {
            string[] values = new string[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                string value = row.Cells[i].Value?.ToString() ?? "";
                values[i] = value.Replace(";", ",");
            }
            writer.WriteLine(string.Join(";", values));
        }
    }
}
