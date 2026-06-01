using System.Windows;
using Dragablz;

namespace SimplePad.Tabs;

internal static class DragablzItemHelper
{
    internal static readonly DependencyProperty IsModifiedProperty = DependencyProperty.RegisterAttached(
        "IsModified",
        typeof(bool),
        typeof(DragablzItemHelper),
        new PropertyMetadata(false));

    internal static bool GetIsModified(DragablzItem obj)
    {
        return (bool)obj.GetValue(IsModifiedProperty);
    }

    internal static void SetIsModified(DragablzItem obj, bool value)
    {
        obj.SetValue(IsModifiedProperty, value);
    }
}