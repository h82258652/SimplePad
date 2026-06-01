using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Dragablz;

namespace SimplePad.Tabs;

public sealed partial class AppTabHeaderControl : UserControl
{
    private readonly Dispatcher _dispatcher;

    public AppTabHeaderControl()
    {
        _dispatcher = Dispatcher;

        InitializeComponent();
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is Tab oldTab)
        {
            oldTab.TitleChanged -= OnTabTitleChanged;
            oldTab.IsModifiedChanged -= OnTabIsModifiedChanged;
        }

        if (e.NewValue is Tab newTab)
        {
            newTab.TitleChanged += OnTabTitleChanged;
            newTab.IsModifiedChanged += OnTabIsModifiedChanged;
        }

        UpdateHeaderText();
        UpdateDragablzItemIsModified();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateDragablzItemIsModified();
    }

    private void OnTabIsModifiedChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateDragablzItemIsModified);
    }

    private void OnTabTitleChanged(object? sender, string e)
    {
        UpdateHeaderText();
    }

    private void UpdateDragablzItemIsModified()
    {
        DependencyObject parent = this;
        while (parent is not null)
        {
            parent = VisualTreeHelper.GetParent(parent);

            if (parent is DragablzItem dragablzItem)
            {
                DragablzItemHelper.SetIsModified(dragablzItem, (DataContext as Tab)?.IsModified ?? false);
                break;
            }
        }
    }

    private void UpdateHeaderText()
    {
        HeaderText.Text = (DataContext as Tab)?.Title ?? string.Empty;
    }
}