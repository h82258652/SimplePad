using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Editor;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class IsWordWrapToggleMenuFlyoutItem : ToggleMenuFlyoutItem
{
    private readonly CoreDispatcher _coreDispatcher;
    private readonly IEditorSettings _editorSettings;

    public IsWordWrapToggleMenuFlyoutItem()
    {
        _coreDispatcher = Dispatcher;
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsChecked();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = IsChecked;
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await _coreDispatcher.SafeRunAsync(UpdateIsChecked);
    }

    private void UpdateIsChecked()
    {
        IsChecked = _editorSettings.IsWordWrap;
    }
}