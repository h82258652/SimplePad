using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Tabs;

public sealed partial class AppTabHeaderControl : UserControl
{
    public AppTabHeaderControl()
    {
        InitializeComponent();

        UpdateHeaderText();
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is Tab oldTab)
        {
            oldTab.TitleChanged -= OnTabTitleChanged;
        }

        if (e.NewValue is Tab newTab)
        {
            newTab.TitleChanged += OnTabTitleChanged;
        }

        UpdateHeaderText();
    }

    private void OnTabTitleChanged(object? sender, string e)
    {
        UpdateHeaderText();
    }

    private void UpdateHeaderText()
    {
        HeaderText.Text = (DataContext as Tab)?.Title ?? string.Empty;
    }
}