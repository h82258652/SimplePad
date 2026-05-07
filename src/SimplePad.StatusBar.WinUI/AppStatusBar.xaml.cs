using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;
using SimplePad.File;
using System;

namespace SimplePad.StatusBar;

public sealed partial class AppStatusBar : UserControl
{
    public static readonly DependencyProperty LineEndingsProperty = DependencyProperty.Register(
        nameof(LineEndings),
        typeof(LineEndings),
        typeof(AppStatusBar),
        new PropertyMetadata(LineEndings.CRLF));

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    public AppStatusBar()
    {
        InitializeComponent();
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
