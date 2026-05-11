using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Tabs
{
    /// <summary>
    /// Interaction logic for AppTabView.xaml
    /// </summary>
    public partial class AppTabView : TabControl
    {
        public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
            nameof(TabRoot),
            typeof(TabRoot),
            typeof(AppTabView),
            new PropertyMetadata(null, OnTabRootChanged));

        private static void OnTabRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private readonly TabManager _tabManager;

        public TabRoot? TabRoot
        {
            get => (TabRoot?)GetValue(TabRootProperty);
            set => SetValue(TabRootProperty, value);
        }

        public AppTabView()
        {
            _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();

            InitializeComponent();
        }
    }
}
