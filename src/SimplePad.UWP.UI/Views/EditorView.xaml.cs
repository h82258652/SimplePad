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

    private FontStyle GetFontStyle(AppFontStyle fontStyle)
    {
        return fontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Bold => FontStyle.Normal,
            AppFontStyle.Italic or AppFontStyle.BoldItalic => FontStyle.Italic,
            _ => throw new ArgumentOutOfRangeException(nameof(fontStyle)),
        };
    }

    private FontWeight GetFontWeight(AppFontStyle fontStyle)
    {
        return fontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Italic => FontWeights.Normal,
            AppFontStyle.Bold or AppFontStyle.BoldItalic => FontWeights.Bold,
            _ => throw new ArgumentOutOfRangeException(nameof(fontStyle)),
        };
    }

    private TextWrapping GetTextWrapping(bool isWordWrap)
    {
        return isWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }
}
