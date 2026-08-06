using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed partial class IsWrapAroundToggleMenuItem : ToggleMenuFlyoutItem
{
    private readonly ISearchSettings _searchSettings;

    public IsWrapAroundToggleMenuItem()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();

        InitializeComponent();

        UpdateIsChecked();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsWrapAround = IsChecked;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsWrapAroundChanged += OnSearchSettingsIsWrapAroundChanged;

        UpdateIsChecked();
    }

    private void OnSearchSettingsIsWrapAroundChanged(object? sender, bool e)
    {
        UpdateIsChecked();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _searchSettings.IsWrapAroundChanged -= OnSearchSettingsIsWrapAroundChanged;
    }

    private void UpdateIsChecked()
    {
        IsChecked = _searchSettings.IsWrapAround;
    }
}