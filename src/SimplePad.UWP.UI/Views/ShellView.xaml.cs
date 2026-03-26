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

        Window.Current.SetTitleBar(TitleBar);
    }

    public ShellViewModel ViewModel { get; } = new ShellViewModel();

    private ElementTheme GetRequestedTheme(AppTheme appTheme)
    {
        switch (appTheme)
        {
            case AppTheme.UseSystemSettings:
                return ElementTheme.Default;

            case AppTheme.Light:
                return ElementTheme.Light;

            case AppTheme.Dark:
                return ElementTheme.Dark;

            default:
                throw new System.ArgumentOutOfRangeException(nameof(appTheme));
        }
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
