using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SimplePad.Editor.UWP.Controls;

public sealed partial class AppTextBox : TextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1))
    );

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    public AppTextBox()
    {
        DefaultStyleKey = typeof(AppTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx///SimplePad.Editor.UWP/Controls/AppTextBox.xaml");
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer contentElement = (ScrollViewer)GetTemplateChild("ContentElement");
        contentElement.PointerWheelChanged -= OnContentElementPointerWheelChanged;
        contentElement.PointerWheelChanged += OnContentElementPointerWheelChanged;
    }

    private void OnContentElementPointerWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
