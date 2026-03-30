using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.UWP.UI.Controls;
using SimplePad.UWP.UI.Extensions;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace SimplePad.UWP.UI.Views;

public sealed partial class StatusBar : UserControl
{
    public static readonly DependencyProperty EditorViewModelProperty = DependencyProperty.Register(
        nameof(EditorViewModel),
        typeof(EditorViewModel),
        typeof(StatusBar),
        null
    );

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(AppTextBox),
        typeof(StatusBar),
        new PropertyMetadata(null, OnTextBoxChanged)
    );

    private readonly AppState _appState;

    public StatusBar()
    {
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();

        _ = UpdateZoomFactorIndicator();

        _appState.ZoomFactorChanged += OnAppStateZoomFactorChanged;
    }

    private async void OnAppStateZoomFactorChanged(object? sender, double e)
    {
        await UpdateZoomFactorIndicator();
    }
     

    private Task UpdateZoomFactorIndicator()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            ZoomFactorIndicator.Text = GetZoomFactorText(_appState.ZoomFactor);
        });
    }

    public EditorViewModel? EditorViewModel
    {
        get => (EditorViewModel?)GetValue(EditorViewModelProperty);
        set => SetValue(EditorViewModelProperty, value);
    }

    public AppTextBox? TextBox
    {
        get => (AppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        StatusBar self = (StatusBar)d;

        TextBox? oldTextBox = (TextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.TextChanged -= self.OnTextBoxTextChanged;
            oldTextBox.SelectionChanged -= self.OnSelectionChanged;
        }

        TextBox? newTextBox = (TextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.TextChanged += self.OnTextBoxTextChanged;
            newTextBox.SelectionChanged += self.OnSelectionChanged;
        }

        self.UpdateCharacterIndicator();
    }

    private string GetEncodingName(Encoding encoding)
    {
        return encoding.EncodingName;
    }

    private string GetZoomFactorText(double zoomFactor)
    {
        return zoomFactor.ToString("P0");
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateCharacterIndicator();
    }

    private string GetPositionText(CursorPosition cursorPosition)
    {
        return $"Ln {cursorPosition.Row}, Col {cursorPosition.Column}";
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateCharacterIndicator();
    }

    private void UpdateCharacterIndicator()
    {
        if (TextBox is null)
        {
            CharacterIndicator.Text = string.Empty;
            return;
        }

        StringBuilder characterIndicatorTextBuilder = new();
        if (TextBox.SelectionLength > 0)
        {
            characterIndicatorTextBuilder.Append(TextBox.SelectionLength.ToString("N0"));
            characterIndicatorTextBuilder.Append(" of ");
        }

        int textLength = TextBox.Text.Length;
        characterIndicatorTextBuilder.Append(textLength.ToString("N0"));
        if (textLength == 1)
        {
            characterIndicatorTextBuilder.Append(" character");
        }
        else
        {
            characterIndicatorTextBuilder.Append(" characters");
        }

        CharacterIndicator.Text = characterIndicatorTextBuilder.ToString();
    }
}
