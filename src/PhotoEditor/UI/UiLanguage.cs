namespace PhotoEditor.UI;

internal static class UiLanguage
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PhotoEditor", "language.txt");
    public static bool English { get; private set; } = Load();
    private static bool Load()
    {
        try { return File.Exists(SettingsPath) && File.ReadAllText(SettingsPath).Trim() == "en"; }
        catch (IOException) { return false; }
        catch (UnauthorizedAccessException) { return false; }
    }

    public static void Select(bool english)
    {
        English = english;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
            File.WriteAllText(SettingsPath, english ? "en" : "uk");
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }

    public static string Text(string source) => !English ? source : source switch
    {
        "3×3: усереднюються тільки сусідні пікселі з близькою яскравістю." => "3×3: only neighbors with similar brightness are averaged.",
        "Інструмент" => "Tool",
        "Історія дій" => "Activity",
        "Білатеральний" => "Bilateral",
        "Бінаризація" => "Threshold",
        "Вертикально" => "Vertical",
        "Верх ліворуч" => "Top left",
        "Верх по центру" => "Top center",
        "Верх праворуч" => "Top right",
        "Видалити виділену область" => "Remove selected area",
        "Виділіть область на зображенні, потім оберіть дію:" => "Select an area on the image, then choose an action:",
        "Висота:" => "Height:",
        "Внизу ліворуч" => "Bottom left",
        "Внизу по центру" => "Bottom center",
        "Внизу праворуч" => "Bottom right",
        "Водяний знак" => "Watermark",
        "Відкрити…" => "Open…",
        "Відновити оригінал" => "Restore original",
        "Відтінки сірого" => "Grayscale",
        "Гауссівський 3×3" => "Gaussian 3×3",
        "Гауссівський 5×5" => "Gaussian 5×5",
        "Горизонтально" => "Horizontal",
        "Гістограма яскравості кольорового зображення" => "Brightness histogram",
        "Додати та зберегти" => "Apply and save",
        "Допуск:" => "Tolerance:",
        "Дія:" => "Action:",
        "Екран · F11" => "Fullscreen · F11",
        "Експорт PNG…" => "Export PNG…",
        "Жирний" => "Bold",
        "За координатами" => "Use coordinates",
        "Залишити тільки виділену область" => "Keep selected area",
        "Заміна кольору" => "Replace color",
        "Замінити на:" => "Replace with:",
        "Застосувати" => "Apply",
        "Затримка, мс:" => "Delay, ms:",
        "Зберегти" => "Save",
        "Зберегти баланс" => "Save balance",
        "Зберегти всі у вибраному форматі" => "Save all in selected format",
        "Зберегти з контрастом" => "Save contrast",
        "Зберегти з прозорістю" => "Save opacity",
        "Зберегти заміну кольору" => "Save replacement",
        "Зберегти результат" => "Save result",
        "Зберегти результат BMP" => "Save result BMP",
        "Зберегти таблицю CSV" => "Save table CSV",
        "Збережений файл" => "Saved file",
        "Зберігати пропорції" => "Keep aspect ratio",
        "Зелений (G)" => "Green (G)",
        "Зменшення шуму" => "Noise reduction",
        "Зміна розмірів" => "Resize",
        "Значення σ:" => "Value σ:",
        "Зображення" => "Image",
        "Зображення 1:" => "Image 1:",
        "Зображення 2:" => "Image 2:",
        "Кадрування / розбиття" => "Crop / split",
        "Кадрування зображення" => "Crop image",
        "Кольорові ефекти" => "Color effects",
        "Колір" => "Color",
        "Колірний баланс та яскравість" => "Color balance and brightness",
        "Конвертація формату" => "Convert format",
        "Контраст, %:" => "Contrast, %:",
        "Контрастність" => "Contrast",
        "Контрастність зображення" => "Image contrast",
        "Курсив" => "Italic",
        "Максимальний 3×3" => "Maximum 3×3",
        "Матриця значень яскравості" => "Brightness matrix",
        "Медіанний 3×3" => "Median 3×3",
        "Медіанний 5×5" => "Median 5×5",
        "Межа оригіналу й результату" => "Original and result divider",
        "Мінімальний 3×3" => "Minimum 3×3",
        "Негатив" => "Negative",
        "Непроз.:" => "Opacity.:",
        "Непрозорість (альфа), %:" => "Opacity (alpha), %:",
        "Об'єднання зображень" => "Merge images",
        "Об'єднати та зберегти" => "Merge and save",
        "Оберіть дію" => "Choose a tool",
        "Обрати зображення" => "Choose images",
        "Обробити вибрані фото" => "Process selected photos",
        "Обробник фотографій" => "Photo Editor",
        "Оригінал" => "Original",
        "Перевірити всі фільтри" => "Compare all filters",
        "Перегляд іде у вікні праворуч." => "Slideshow plays in the preview.",
        "По центру" => "Center",
        "По центру ліворуч" => "Middle left",
        "По центру праворуч" => "Middle right",
        "Повернути початкове фото" => "Restore original photo",
        "Позиція:" => "Position:",
        "Показ поточного кольорового зображення у вікні попереднього перегляду." => "Display the original color image in the preview.",
        "Порівняти з оригіналом" => "Compare with original",
        "Прозорість зображення" => "Image opacity",
        "Розбити на частини" => "Split into tiles",
        "Розбити на частини:" => "Split into tiles:",
        "Розмір:" => "Size:",
        "Рядків:" => "Rows:",
        "Серединний (Midpoint) 3×3" => "Midpoint (Midpoint) 3×3",
        "Синій (B)" => "Blue (B)",
        "Скинути оригінал" => "Reset original",
        "Слайд-шоу" => "Slideshow",
        "Стовпців:" => "Columns:",
        "Текст:" => "Text:",
        "Тип фільтра" => "Filter type",
        "Усереднювальний 3×3" => "Mean filter 3×3",
        "Формат:" => "Format:",
        "Фотографії" => "Photos",
        "Фільтр" => "Filter",
        "Фільтр:" => "Filter:",
        "Фільтрація зображення — сигма-фільтр" => "Image filtering — sigma filter",
        "Червоний (R)" => "Red (R)",
        "Ширина:" => "Width:",
        "Шукати:" => "Find:",
        "Яскравість" => "Brightness",
        "вертикально" => "vertical",
        "горизонтально" => "horizontal",
        "■ Зупинити" => "■ Stop",
        "▶ Запустити" => "▶ Start",
        _ => source
    };
}
