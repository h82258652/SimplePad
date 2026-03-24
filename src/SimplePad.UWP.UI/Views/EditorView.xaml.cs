using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.UWP.UI.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class EditorView : UserControl
{
    private readonly IAppSettings _appSettings;
    private readonly AppState _appState;

    public EditorView(ShellViewModel shellViewModel)
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();

        ShellViewModel = shellViewModel;
    }

    public ShellViewModel ShellViewModel { get; }

    private TextWrapping GetTextWrapping(bool isWordWrap)
    {
        return isWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }
}
