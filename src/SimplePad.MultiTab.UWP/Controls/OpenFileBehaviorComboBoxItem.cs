namespace SimplePad.MultiTab.UWP.Controls;

internal sealed class OpenFileBehaviorComboBoxItem
{
    internal string DisplayName { get; }

    internal OpenFileBehavior Value { get; }

    internal OpenFileBehaviorComboBoxItem(OpenFileBehavior value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public override string ToString()
    {
        return DisplayName;
    }
}
