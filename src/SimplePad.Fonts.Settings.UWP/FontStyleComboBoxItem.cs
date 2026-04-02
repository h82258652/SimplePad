namespace SimplePad.Fonts;

internal sealed class FontStyleComboBoxItem
{
    internal FontStyleComboBoxItem(AppFontStyle value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    internal string DisplayName { get; }

    internal AppFontStyle Value { get; }

    public override string ToString()
    {
        return DisplayName;
    }
}
