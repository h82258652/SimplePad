using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class AppTextBox : TextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1))
    );

    public static readonly DependencyProperty MaxZoomFactorProperty = DependencyProperty.Register(
        nameof(MaxZoomFactor),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(5d, OnMaxZoomFactorChanged)
    );

    public static readonly DependencyProperty MinZoomFactorProperty = DependencyProperty.Register(
        nameof(MinZoomFactor),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(0.1d, OnMinZoomFactorChanged)
    );

    public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(
        nameof(ZoomFactor),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(1d, OnZoomFactorChanged)
    );

    private static readonly DependencyProperty ComputedFontSizeProperty =
        DependencyProperty.Register(
            nameof(ComputedFontSize),
            typeof(double),
            typeof(AppTextBox),
            new PropertyMetadata(14d)
        );

    public AppTextBox()
    {
        DefaultStyleKey = typeof(AppTextBox);

        RegisterPropertyChangedCallback(FontSizeProperty, OnFontSizeChanged);

        SelectionChanged += OnSelectionChanged;

        ComputedFontSize = FontSize * ZoomFactor;
    }

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    public double MaxZoomFactor
    {
        get => (double)GetValue(MaxZoomFactorProperty);
        set => SetValue(MaxZoomFactorProperty, value);
    }

    public double MinZoomFactor
    {
        get => (double)GetValue(MinZoomFactorProperty);
        set => SetValue(MinZoomFactorProperty, value);
    }

    public double ZoomFactor
    {
        get => (double)GetValue(ZoomFactorProperty);
        set => SetValue(ZoomFactorProperty, value);
    }

    private double ComputedFontSize
    {
        get => (double)GetValue(ComputedFontSizeProperty);
        set => SetValue(ComputedFontSizeProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer scrollViewer = (ScrollViewer)GetTemplateChild("ContentElement");
        scrollViewer.PointerWheelChanged -= OnContentElementPointerWheelChanged;
        scrollViewer.PointerWheelChanged += OnContentElementPointerWheelChanged;
    }

    private static void OnMaxZoomFactorChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        AppTextBox self = (AppTextBox)d;
        double maxZoomFactor = (double)e.NewValue;
        self.ZoomFactor = Math.Min(self.ZoomFactor, maxZoomFactor);
    }

    private static void OnMinZoomFactorChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        AppTextBox self = (AppTextBox)d;
        double minZoomFactor = (double)e.NewValue;
        self.ZoomFactor = Math.Max(self.ZoomFactor, minZoomFactor);
    }

    private static void OnZoomFactorChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        AppTextBox self = (AppTextBox)d;
        self.ComputedFontSize = self.FontSize * self.ZoomFactor;
    }

    private CursorPosition GetCursorPosition()
    {
        int endMarker = SelectionStart + SelectionLength;

        if (endMarker == 0)
        {
            return new CursorPosition(1, 1);
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
                return new CursorPosition(row, col);
            }
        }

        return new CursorPosition(row, col);
    }

    private void OnContentElementPointerWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        if (e.KeyModifiers.HasFlag(VirtualKeyModifiers.Control))
        {
            e.Handled = true;

            int mouseWheelDelta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
            if (mouseWheelDelta > 0)
            {
                ZoomFactor = Math.Min(MaxZoomFactor, ZoomFactor + 0.1);
            }
            else if (mouseWheelDelta < 0)
            {
                ZoomFactor = Math.Max(MinZoomFactor, ZoomFactor - 0.1);
            }
        }
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        ComputedFontSize = FontSize * ZoomFactor;
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        CursorPosition = GetCursorPosition();
    }
}
