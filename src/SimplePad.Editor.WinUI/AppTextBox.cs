using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Fonts;
using System;
using System.Threading.Tasks;

namespace SimplePad.Editor;

public sealed partial class AppTextBox : TextBox, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1), OnCursorPositionChanged));

    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;
    private bool _internalCanUndo;

    public AppTextBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        _internalCanUndo = CanUndo;
        UpdateFontFamily();
        UpdateFontStyle();
        UpdateFontSize();
        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();
        UpdateZoomedFontSize();
    }

    public event EventHandler<bool>? CanUndoChanged;

    public event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? IAppTextBox.SelectionChanged
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    event EventHandler<string>? IAppTextBox.TextChanged
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    public void Focus()
    {
        Focus(FocusState.Programmatic);
    }

    public Task GoToLineAsync()
    {
        throw new NotImplementedException();
    }

    private static void OnCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void UpdateFontFamily()
    {
        throw new NotImplementedException();
    }

    private void UpdateFontSize()
    {
        throw new NotImplementedException();
    }

    private void UpdateFontStyle()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}