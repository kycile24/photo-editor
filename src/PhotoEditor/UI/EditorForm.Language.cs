namespace PhotoEditor.UI;

public partial class EditorForm
{
    private readonly Dictionary<Control, string> captions = new();

    private void ApplyLanguage(Control parent)
    {
        if (parent is Form or Label or ButtonBase or GroupBox)
        {
            if (!captions.TryGetValue(parent, out string? source))
                captions[parent] = source = parent.Text;
            parent.Text = UiLanguage.Text(source);
        }
        if (parent == comboAction || parent == comboWatermarkPosition || parent == comboNoiseFilter)
        {
            var combo = (ComboBox)parent;
            combo.Format -= FormatItem;
            combo.FormattingEnabled = true;
            combo.Format += FormatItem;
            combo.Refresh();
        }
        if (parent is DataGridView grid)
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Tag ??= column.HeaderText;
                column.HeaderText = UiLanguage.Text((string)column.Tag);
            }
        foreach (Control child in parent.Controls) ApplyLanguage(child);
    }

    private void FormatItem(object? sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is string source) e.Value = UiLanguage.Text(source);
    }
}
