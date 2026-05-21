using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Fonts;

namespace SimplePad.Editor;

internal sealed class AppTextBoxInternal : TextBox
{
    private readonly Dispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;

    static AppTextBoxInternal()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AppTextBoxInternal), new FrameworkPropertyMetadata(typeof(AppTextBoxInternal)));
    }

    public AppTextBoxInternal()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        CursorPosition = new CursorPosition(1, 1);

        UpdateFontFamily();
        UpdateFontStyle();
        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
        SelectionChanged += OnSelectionChanged;
    }

    internal event EventHandler<CursorPosition>? CursorPositionChanged;

    private CursorPosition CursorPosition
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                CursorPositionChanged?.Invoke(this, value);
            }
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer contentHost = (ScrollViewer)GetTemplateChild("PART_ContentHost");
        contentHost.MouseWheel -= OnContentHostMouseWheel;
        contentHost.MouseWheel += OnContentHostMouseWheel;
    }

    private void OnContentHostMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
        {
            e.Handled = true;

            int delta = e.Delta;
            if (delta > 0)
            {
                _editorZoomState.ZoomIn();
            }
            else
            {
                _editorZoomState.ZoomOut();
            }
        }
    }

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsSpellCheckEnabled);
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateTextWrapping);
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        _dispatcher.Invoke(UpdateFontFamily);
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        _dispatcher.Invoke(UpdateFontStyle);
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        CursorPosition = new CursorPosition(1, 1);// TODO
    }

    private void UpdateFontFamily()
    {
        FontFamily = new FontFamily(_fontSettings.FontFamily);
    }

    private void UpdateFontStyle()
    {
        FontStyle = _fontSettings.FontStyle.GetWPFFontStyle();
        FontWeight = _fontSettings.FontStyle.GetWPFFontWeight();
    }

    private void UpdateIsSpellCheckEnabled()
    {
        SpellCheck.IsEnabled = _editorSettings.IsSpellCheckEnabled;
    }

    private void UpdateTextWrapping()
    {
        TextWrapping = _editorSettings.IsWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }
}