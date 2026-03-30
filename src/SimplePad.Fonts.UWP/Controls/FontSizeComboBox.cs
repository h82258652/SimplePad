using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Fonts.Settings;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts.UWP.Controls;

public sealed partial class FontSizeComboBox : ComboBox
{
    private readonly IFontSettings _fontSettings;

    public FontSizeComboBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
    }
}
