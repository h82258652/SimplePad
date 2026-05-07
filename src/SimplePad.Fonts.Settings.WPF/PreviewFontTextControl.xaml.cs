using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimplePad.Fonts;

public partial class PreviewFontTextControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;

    public PreviewFontTextControl()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();

        InitializeComponent();

        UpdateFontFamily();
        UpdateFontStyle();
        UpdateFontSize();
    }

    private void UpdateFontFamily()
    {
        throw new NotImplementedException();
    }

    private void UpdateFontSize()
    {
        PreviewText.FontSize = _fontSettings.FontSize;
    }

    private void UpdateFontStyle()
    {
        throw new NotImplementedException();
    }
}