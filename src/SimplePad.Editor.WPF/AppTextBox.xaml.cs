using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Fonts;

namespace SimplePad.Editor;

public partial class AppTextBox : UserControl, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        new PropertyMetadata(null, OnCursorPositionChanged));

    public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register(
        nameof(SelectedText),
        typeof(string),
        typeof(AppTextBox),
        new PropertyMetadata(string.Empty, OnSelectedTextChanged));

    public static readonly DependencyProperty SelectionLengthProperty = DependencyProperty.Register(
        nameof(SelectionLength),
        typeof(int),
        typeof(AppTextBox),
        new PropertyMetadata(0, OnSelectionLengthChanged));

    public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.Register(
        nameof(SelectionStart),
        typeof(int),
        typeof(AppTextBox),
        new PropertyMetadata(0, OnSelectionStartChanged));

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(AppTextBox),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, OnTextChanged, CoerceText, isAnimationProhibited: true, UpdateSourceTrigger.LostFocus));

    private readonly Dispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;

    private bool _internalCanUndo;

    public AppTextBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        CursorPosition = new CursorPosition(1, 1);

        InitializeComponent();

        _internalCanUndo = CanUndo;
        UpdateFontSize();
        UpdateTextBoxInternalFontSize();

        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
    }

    public event EventHandler<bool>? CanUndoChanged;

    public event EventHandler<CursorPosition>? CursorPositionChanged;

    public event EventHandler? SelectionChanged;

    public event EventHandler<string>? TextChanged;

    public bool CanUndo => TextBoxInternal.CanUndo;

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    public string SelectedText
    {
        get => (string)GetValue(SelectedTextProperty);
        set => SetValue(SelectedTextProperty, value);
    }

    public int SelectionLength
    {
        get => (int)GetValue(SelectionLengthProperty);
        set => SetValue(SelectionLengthProperty, value);
    }

    public int SelectionStart
    {
        get => (int)GetValue(SelectionStartProperty);
        set => SetValue(SelectionStartProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public void CopySelectionToClipboard()
    {
        TextBoxInternal.Copy();
    }

    public void CutSelectionToClipboard()
    {
        TextBoxInternal.Cut();
    }

    void IAppTextBox.Focus()
    {
        _ = TextBoxInternal.Focus();
    }

    public Task GoToLineAsync()
    {
        string text = Text;
        int totalLines = text.Split('\r').Length;

        GoToLineDialog goToLineDialog = new(CursorPosition.Row, totalLines);
        if (goToLineDialog.ShowDialog() is true)
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

        return Task.CompletedTask;
    }

    public void PasteFromClipboard()
    {
        TextBoxInternal.Paste();
    }

    public void SelectAll()
    {
        TextBoxInternal.SelectAll();
    }

    public void Undo()
    {
        TextBoxInternal.Undo();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == FontSizeProperty)
        {
            UpdateTextBoxInternalFontSize();
        }
    }

    private static object? CoerceText(DependencyObject d, object? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return value;
    }

    private static void OnCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        CursorPosition cursorPosition = (CursorPosition)e.NewValue;
        self.CursorPositionChanged?.Invoke(self, cursorPosition);
    }

    private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        string selectedText = (string)e.NewValue;
        self.TextBoxInternal.SelectedText = selectedText;
    }

    private static void OnSelectionLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        int selectionLength = (int)e.NewValue;
        self.TextBoxInternal.SelectionLength = selectionLength;
    }

    private static void OnSelectionStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        int selectionStart = (int)e.NewValue;
        self.TextBoxInternal.SelectionStart = selectionStart;
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        string text = (string)e.NewValue;
        self.TextBoxInternal.Text = text;
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        _dispatcher.Invoke(UpdateTextBoxInternalFontSize);
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        _dispatcher.Invoke(UpdateFontSize);
    }

    private void OnTextBoxInternalCursorPositionChanged(object sender, CursorPosition e)
    {
        CursorPosition = e;
    }

    private void OnTextBoxInternalSelectionChanged(object sender, RoutedEventArgs e)
    {
        SelectionStart = TextBoxInternal.SelectionStart;
        SelectionLength = TextBoxInternal.SelectionLength;
        SelectedText = TextBoxInternal.SelectedText;

        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnTextBoxInternalTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateInternalCanUndo();

        string text = TextBoxInternal.Text;
        Text = text;
        TextChanged?.Invoke(this, text);
    }

    private void UpdateFontSize()
    {
        FontSize = _fontSettings.FontSize;
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

    private void UpdateTextBoxInternalFontSize()
    {
        if (TextBoxInternal is { } textBoxInternal)
        {
            textBoxInternal.FontSize = _fontSettings.FontSize * _editorZoomState.ZoomFactor;
        }
    }
}