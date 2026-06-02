using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.File;

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

    private readonly EditorZoomState _editorZoomState;
    private readonly IStatusBarSettings _statusBarSettings;

    public AppStatusBar()
    {
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
