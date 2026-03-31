namespace SimplePad.MultiTab.UWP.Controls;

internal sealed class OpenFileBehaviorComboBoxItem
{
    internal OpenFileBehaviorComboBoxItem(OpenFileBehavior value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    internal string DisplayName { get; }

    internal OpenFileBehavior Value { get; }

    public override string ToString()
    {
        return DisplayName;
    }
}
