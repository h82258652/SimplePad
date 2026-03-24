namespace SimplePad.UWP.UI.Helpers;

public static class UIHelper
{
    public static bool IsStringNotNullOrEmpty(string? str)
    {
        return !string.IsNullOrEmpty(str);
    }
}
