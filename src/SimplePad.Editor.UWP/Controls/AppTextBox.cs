using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor.Settings;
using Windows.System;
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

    private static readonly DependencyProperty ZoomedFontSizeProperty = DependencyProperty.Register(
        nameof(ZoomedFontSize),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(14d));

    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];

    public AppTextBox()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        DefaultStyleKey = typeof(AppTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx///SimplePad.Editor.UWP/Controls/AppTextBox.xaml");

        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();
        UpdateZoomedFontSize();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
        TextChanged += OnTextChanged;
        SelectionChanged += OnSelectionChanged;
        RegisterPropertyChangedCallback(FontSizeProperty, OnFontSizeChanged);
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

    private double ZoomedFontSize
    {
        get => (double)GetValue(ZoomedFontSizeProperty);
        set => SetValue(ZoomedFontSizeProperty, value);
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
        if (e.KeyModifiers.HasFlag(VirtualKeyModifiers.Control))
        {
            e.Handled = true;

            int mouseWheelDelta = e.GetCurrentPoint(this).Properties.MouseWheelDelta;
            if (mouseWheelDelta > 0)
            {
                _editorZoomState.ZoomIn();
            }
            else
            {
                _editorZoomState.ZoomOut();
            }
        }
    }

    private async void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateIsSpellCheckEnabled);
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateTextWrapping);
    }

    private async void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        await Dispatcher.SafeRunAsync(UpdateZoomedFontSize);
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        UpdateZoomedFontSize();
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

    private void UpdateZoomedFontSize()
    {
        ZoomedFontSize = FontSize * _editorZoomState.ZoomFactor;
    }
}