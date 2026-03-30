using System;
using System.Threading.Tasks;

namespace SimplePad.Fonts.Settings;

public interface IFontSettings
{
    event EventHandler<string>? FontFamilyChanged;

    event EventHandler<double>? FontSizeChanged;

    event EventHandler<AppFontStyle>? FontStyleChanged;

    string FontFamily { get; set; }

    double FontSize { get; set; }

    AppFontStyle FontStyle { get; set; }

    Task LoadAsync();

    Task SaveAsync();
}
