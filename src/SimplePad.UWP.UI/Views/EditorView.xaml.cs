using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.ViewModels;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class EditorView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(EditorViewModel),
        typeof(EditorView),
        null);

    private readonly IAppSettings _appSettings;
    private readonly AppState _appState;

    public EditorView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();
    }

    public EditorViewModel? ViewModel
    {
        get => (EditorViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    private FontWeight GetFontWeight(AppFontStyle fontStyle)
    {
        switch (fontStyle)
        {
            case AppFontStyle.Regular:
            case AppFontStyle.Italic:
                return FontWeights.Normal;
                
            case AppFontStyle.Bold:
            case AppFontStyle.BoldItalic:
                return FontWeights.Bold;

            default:
                throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }

    private TextWrapping GetTextWrapping(bool isWordWrap)
    {
        return isWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }

    private FontStyle GetFontStyle(AppFontStyle fontStyle)
    {
        switch (fontStyle)
        {
            case AppFontStyle.Regular:
            case AppFontStyle.Bold:
                return FontStyle.Normal;

            case AppFontStyle.Italic:
            case AppFontStyle.BoldItalic:
                return FontStyle.Italic;

            default:
                throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }
}
