using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class ShellView : UserControl
{
    private readonly IAppSettings _appSettings;

    public ShellView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();

        InitializeComponent();

        ViewModel = new ShellViewModel();
        ViewModel.PropertyChanged += OnViewModelPropertyChanged;

        Window.Current.SetTitleBar(TitleBar);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.IsSettingsViewVisible))
        {
            if (ViewModel.IsSettingsViewVisible)
            {
                Window.Current.SetTitleBar(SettingsView.TitleBar);
            }
            else
            {
                Window.Current.SetTitleBar(TitleBar);
            }
        }
    }

    public ShellViewModel ViewModel { get; } 

    private ElementTheme GetRequestedTheme(AppTheme appTheme)
    {
        return appTheme switch
        {
            AppTheme.UseSystemSettings => ElementTheme.Default,
            AppTheme.Light => ElementTheme.Light,
            AppTheme.Dark => ElementTheme.Dark,
            _ => throw new ArgumentOutOfRangeException(nameof(appTheme)),
        };
    }

    private void OnAppTabViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TabView.CloseButtonOverlayMode = TabViewCloseButtonOverlayMode.OnPointerOver;
    }

    private async void OnTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        if (args.Item is EditorViewModel editorViewModel)
        {
            await ViewModel.CloseEditorAsync(editorViewModel);
        }
    }

    private void OnTabViewAddTabButtonClick(TabView sender, object args)
    {
        ViewModel.AddBlankEditor();
    } 
}
