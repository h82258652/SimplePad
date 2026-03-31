using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SimplePad.Editor.UWP.Controls;

public sealed partial class AppTextBox : TextBox, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1), OnCursorPositionChanged));

    private readonly IEditorSettings _editorSettings;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];

    public AppTextBox()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        DefaultStyleKey = typeof(AppTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx///SimplePad.Editor.UWP/Controls/AppTextBox.xaml");

        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
        TextChanged += OnTextChanged;
        SelectionChanged += OnSelectionChanged;
    }

    public event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? IAppTextBox.SelectionChanged
    {
        add
        {
            _selectionChagnedHandler.Add(value);
        }
        remove
        {
            _selectionChagnedHandler.Remove(value);
        }
    }

    event EventHandler<string>? IAppTextBox.TextChanged
    {
        add
        {
            _textChangedHandler.Add(value);
        }
        remove
        {
            _textChangedHandler.Remove(value);
        }
    }

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer contentElement = (ScrollViewer)GetTemplateChild("ContentElement");
        contentElement.PointerWheelChanged -= OnContentElementPointerWheelChanged;
        contentElement.PointerWheelChanged += OnContentElementPointerWheelChanged;
    }

    private static void OnCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        var cursorPosition = (CursorPosition)e.NewValue;
        self.CursorPositionChanged?.Invoke(self, cursorPosition);
    }

    private void OnContentElementPointerWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateIsSpellCheckEnabled);
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateTextWrapping);
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        foreach (EventHandler? handler in _selectionChagnedHandler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        foreach (EventHandler<string>? handler in _textChangedHandler)
        {
            handler?.Invoke(this, Text);
        }
    }

    private void UpdateIsSpellCheckEnabled()
    {
        IsSpellCheckEnabled = _editorSettings.IsSpellCheckEnabled;
    }

    private void UpdateTextWrapping()
    {
        TextWrapping = _editorSettings.IsWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }
}