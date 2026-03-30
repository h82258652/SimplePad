using System.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Themes.UWP.Controls;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class ShellView : ThemeContainer
{
    public ShellView()
    {
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
