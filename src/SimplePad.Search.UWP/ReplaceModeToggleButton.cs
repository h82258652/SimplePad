using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace SimplePad.Search;

internal sealed partial class ReplaceModeToggleButton : ToggleButton
{
    private readonly SearchViewState _searchViewState;

    public ReplaceModeToggleButton()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(ReplaceModeToggleButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/ReplaceModeToggleButton.xaml");

        UpdateIsChecked();
        UpdateTooltip();

        Checked += OnChecked;
        Unchecked += OnUnchecked;

        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
    }

    private void OnChecked(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsReplaceMode = true;
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateIsChecked();
        UpdateTooltip();
    }

    private void OnUnchecked(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsReplaceMode = false;
    }

    private void UpdateIsChecked()
    {
        IsChecked = _searchViewState.IsReplaceMode;
    }

    private void UpdateTooltip()
    {
        if (_searchViewState.IsReplaceMode)
        {
            ToolTipService.SetToolTip(this, "Close replace options");
        }
        else
        {
            ToolTipService.SetToolTip(this, "Open replace options");
        }
    }
}