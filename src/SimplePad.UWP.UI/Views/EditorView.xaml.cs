using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.UWP.UI.Controls;
using SimplePad.ViewModels;
using Windows.UI.Core;
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
        null
    );

    private readonly IAppSettings _appSettings;
    private readonly AppState _appState;

    public EditorView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();

        _ = UpdateTextBoxZoomFactor();

        _appSettings.PropertyChanged += OnAppSettingsPropertyChanged;
        _appState.PropertyChanged += OnAppStatePropertyChanged;

        TextBox.RegisterPropertyChangedCallback(
            AppTextBox.ZoomFactorProperty,
            OnTextBoxZoomFactorChanged
        );

        _ = UpdateStatusBar();

        _ = UpdateTextBoxFontStyle();
        _ = UpdateTextBoxFontSize();
        _ = UpdateTextBoxTextWrapping();
        _ = UpdateTextBoxIsSpellCheck();
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

    private async void OnAppSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_appSettings.FontSize))
        {
            await UpdateTextBoxFontSize();
        }
        else if (e.PropertyName == nameof(_appSettings.FontStyle))
        {
            await UpdateTextBoxFontStyle();
        }
        else if (e.PropertyName == nameof(_appSettings.IsWordWrap))
        {
            await UpdateTextBoxTextWrapping();
        }
        else if (e.PropertyName == nameof(_appSettings.IsStatusBarVisible))
        {
            await UpdateStatusBar();
        }
        else if (e.PropertyName == nameof(_appSettings.IsSpellCheckEnabled))
        {
            await UpdateTextBoxIsSpellCheck();
        }
    }

    private async void OnAppStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_appState.ZoomFactor))
        {
            await UpdateTextBoxZoomFactor();
        }
    }

    private void OnTextBoxZoomFactorChanged(DependencyObject sender, DependencyProperty dp)
    {
        _appState.ZoomFactor = TextBox.ZoomFactor;
    }

    private async Task UpdateStatusBar()
    {
        if (Dispatcher.HasThreadAccess)
        {
            StatusBar.Visibility = _appSettings.IsStatusBarVisible
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    StatusBar.Visibility = _appSettings.IsStatusBarVisible
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }
            );
        }
    }

    private async Task UpdateTextBoxFontSize()
    {
        if (Dispatcher.HasThreadAccess)
        {
            TextBox.FontSize = _appSettings.FontSize;
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TextBox.FontSize = _appSettings.FontSize;
                }
            );
        }
    }

    private async Task UpdateTextBoxFontStyle()
    {
        if (Dispatcher.HasThreadAccess)
        {
            TextBox.FontStyle = GetFontStyle(_appSettings.FontStyle);
            TextBox.FontWeight = GetFontWeight(_appSettings.FontStyle);
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TextBox.FontStyle = GetFontStyle(_appSettings.FontStyle);
                    TextBox.FontWeight = GetFontWeight(_appSettings.FontStyle);
                }
            );
        }
    }

    private async Task UpdateTextBoxIsSpellCheck()
    {
        if (Dispatcher.HasThreadAccess)
        {
            TextBox.IsSpellCheckEnabled = _appSettings.IsSpellCheckEnabled;
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TextBox.IsSpellCheckEnabled = _appSettings.IsSpellCheckEnabled;
                }
            );
        }
    }

    private async Task UpdateTextBoxTextWrapping()
    {
        if (Dispatcher.HasThreadAccess)
        {
            TextBox.TextWrapping = GetTextWrapping(_appSettings.IsWordWrap);
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TextBox.TextWrapping = GetTextWrapping(_appSettings.IsWordWrap);
                }
            );
        }
    }

    private async Task UpdateTextBoxZoomFactor()
    {
        if (Dispatcher.HasThreadAccess)
        {
            TextBox.ZoomFactor = _appState.ZoomFactor;
        }
        else
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    TextBox.ZoomFactor = _appState.ZoomFactor;
                }
            );
        }
    }
}
