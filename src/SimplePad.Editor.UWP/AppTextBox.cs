using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Fonts;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace SimplePad.Editor;

public sealed partial class AppTextBox : TextBox, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1), OnCursorPositionChanged)
    );

    private static readonly DependencyProperty ZoomedFontSizeProperty = DependencyProperty.Register(
        nameof(ZoomedFontSize),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(14d)
    );

    private readonly CoreDispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];
    private bool _internalCanUndo;

    public AppTextBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        DefaultStyleKey = typeof(AppTextBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Editor.UWP/AppTextBox.xaml"
        );
        InitializeKeyboardAccelerators();

        _internalCanUndo = CanUndo;
        UpdateFontFamily();
        UpdateFontStyle();
        UpdateFontSize();
        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();
        UpdateZoomedFontSize();

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
        TextChanged += OnTextChanged;
        SelectionChanged += OnSelectionChanged;
        RegisterPropertyChangedCallback(FontSizeProperty, OnFontSizeChanged);
    }

    public event EventHandler<bool>? CanUndoChanged;

    public event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? IAppTextBox.SelectionChanged
    {
        add => _selectionChagnedHandler.Add(value);
        remove => _selectionChagnedHandler.Remove(value);
    }

    event EventHandler<string>? IAppTextBox.TextChanged
    {
        add => _textChangedHandler.Add(value);
        remove => _textChangedHandler.Remove(value);
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

    public void Focus()
    {
        Focus(FocusState.Programmatic);
    }

    public async Task GoToLineAsync()
    {
        string text = Text;
        int totalLines = text.Split('\r').Length;

        GoToLineDialog goToLineDialog = new(CursorPosition.Row, totalLines);
        ContentDialogResult dialogResult = await goToLineDialog.ShowAsync();
        if (dialogResult == ContentDialogResult.Primary)
        {
            int selectionStart = 0;
            int row = 1;

            for (int i = 0; i < text.Length && row < goToLineDialog.LineNumber; i++)
            {
                char c = text[i];
                selectionStart++;

                if (c == '\r')
                {
                    row++;
                }
            }

            SelectionStart = selectionStart;
        }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer contentElement = (ScrollViewer)GetTemplateChild("ContentElement");
        contentElement.PointerWheelChanged -= OnContentElementPointerWheelChanged;
        contentElement.PointerWheelChanged += OnContentElementPointerWheelChanged;
    }

    private static void OnCursorPositionChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        AppTextBox self = (AppTextBox)d;
        var cursorPosition = (CursorPosition)e.NewValue;
        self.CursorPositionChanged?.Invoke(self, cursorPosition);
    }

    private void InitializeKeyboardAccelerators()
    {
        KeyboardAccelerator zoomInKeyboardAccelerator = new()
        {
            Key = VirtualKey.Add,
            Modifiers = VirtualKeyModifiers.Control
        };
        zoomInKeyboardAccelerator.Invoked += OnZoomInKeyboardAcceleratorInvoked;
        KeyboardAccelerators.Add(zoomInKeyboardAccelerator);

        KeyboardAccelerator zoomOutKeyboardAccelerator = new()
        {
            Key = VirtualKey.Subtract,
            Modifiers = VirtualKeyModifiers.Control
        };
        zoomOutKeyboardAccelerator.Invoked += OnZoomOutKeyboardAcceleratorInvoked;
        KeyboardAccelerators.Add(zoomOutKeyboardAccelerator);

        KeyboardAccelerator restoreDefaultZoomKeyboardAccelerator = new()
        {
            Key = VirtualKey.Number0,
            Modifiers = VirtualKeyModifiers.Control
        };
        restoreDefaultZoomKeyboardAccelerator.Invoked += OnRestoreDefaultZoomKeyboardAcceleratorInvoked;
        KeyboardAccelerators.Add(restoreDefaultZoomKeyboardAccelerator);
        restoreDefaultZoomKeyboardAccelerator = new()
        {
            Key = VirtualKey.NumberPad0,
            Modifiers = VirtualKeyModifiers.Control
        };
        restoreDefaultZoomKeyboardAccelerator.Invoked += OnRestoreDefaultZoomKeyboardAcceleratorInvoked;
        KeyboardAccelerators.Add(restoreDefaultZoomKeyboardAccelerator);
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
        await _dispatcher.SafeRunAsync(UpdateIsSpellCheckEnabled);
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateTextWrapping);
    }

    private async void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        await _dispatcher.SafeRunAsync(UpdateZoomedFontSize);
    }

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontFamily);
    }

    private async void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontSize);
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, Fonts.AppFontStyle e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontStyle);
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        UpdateZoomedFontSize();
    }

    private void OnRestoreDefaultZoomKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        _editorZoomState.ResetZoomFactor();
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateCursorPosition();

        foreach (EventHandler? handler in _selectionChagnedHandler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateInternalCanUndo();

        foreach (EventHandler<string>? handler in _textChangedHandler)
        {
            handler?.Invoke(this, Text);
        }
    }

    private void OnZoomInKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        _editorZoomState.ZoomIn();
    }

    private void OnZoomOutKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        _editorZoomState.ZoomOut();
    }

    private void UpdateCursorPosition()
    {
        int endMarker = SelectionStart + SelectionLength;

        if (endMarker == 0)
        {
            CursorPosition = new CursorPosition(1, 1);
            return;
        }

        int i = 0;
        int col = 1;
        int row = 1;

        foreach (char c in Text)
        {
            i++;
            col++;

            if (c == '\r')
            {
                row++;
                col = 1;
            }

            if (i == endMarker)
            {
                CursorPosition = new CursorPosition(row, col);
                return;
            }
        }

        CursorPosition = new CursorPosition(row, col);
    }

    private void UpdateFontFamily()
    {
        FontFamily = new FontFamily(_fontSettings.FontFamily);
    }

    private void UpdateFontSize()
    {
        FontSize = _fontSettings.FontSize;
    }

    private void UpdateFontStyle()
    {
        FontStyle = _fontSettings.FontStyle.GetUWPFontStyle();
        FontWeight = _fontSettings.FontStyle.GetUWPFontWeight();
    }

    private void UpdateInternalCanUndo()
    {
        bool canUndo = CanUndo;
        if (_internalCanUndo != canUndo)
        {
            _internalCanUndo = canUndo;
            CanUndoChanged?.Invoke(this, canUndo);
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