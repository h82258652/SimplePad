using Ardalis.SmartEnum;

namespace SimplePad.Tabs;

public sealed class OpenFileBehavior : SmartEnum<OpenFileBehavior>
{
    public static readonly OpenFileBehavior NewTab = new("Open in a new tab", 0);
    public static readonly OpenFileBehavior NewWindow = new("Open in a new window", 1);

    private OpenFileBehavior(string name, int value) : base(name, value)
    {
    }
}
