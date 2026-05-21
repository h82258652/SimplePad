using System.Windows;
using SimplePad.Editor;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public sealed partial class AppMenuBar : System.Windows.Controls.Menu
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(AppMenuBar),
        new PropertyMetadata(null, OnTabChanged));

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppMenuBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    public AppMenuBar()
    {
        InitializeComponent();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppMenuBar self = (AppMenuBar)d;
        Tab? tab = (Tab?)e.NewValue;

        self.NewTabMenuItem.TabRoot = tab?.Root;
        self.SaveMenuItem.Tab = tab;
        self.SaveAsMenuItem.Tab = tab;
        self.CloseTabMenuItem.Tab = tab;
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppMenuBar self = (AppMenuBar)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;

        self.UndoMenuItem.TextBox = textBox;
        self.CutMenuItem.TextBox = textBox;
        self.CopyMenuItem.TextBox = textBox;
        self.PasteMenuItem.TextBox = textBox;
        self.DeleteMenuItem.TextBox = textBox;
        self.GoToLineMenuItem.TextBox = textBox;
        self.SelectAllMenuItem.TextBox = textBox;
        self.TimeDateMenuItem.TextBox = textBox;
    }
}