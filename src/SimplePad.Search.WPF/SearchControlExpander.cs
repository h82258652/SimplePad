using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search
{
    public sealed class SearchControlExpander : Expander
    {
        private readonly SearchViewState _searchViewState;

        public SearchControlExpander()
        {
            _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

            Expanded += OnExpanded;
            Collapsed += OnCollapsed;
            _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;

        }

        private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
        {
            throw new NotImplementedException();
        }

        private void OnExpanded(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnCollapsed(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
