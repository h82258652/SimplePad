using CommunityToolkit.WinUI.Animations.Expressions;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace SimplePad.Search;

internal sealed partial class SearchControlBackground : FrameworkElement
{
    public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
        nameof(BorderColor),
        typeof(Color),
        typeof(SearchControlBackground),
        new PropertyMetadata(Colors.Transparent, OnBorderColorChanged));

    public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
        nameof(BorderThickness),
        typeof(double),
        typeof(SearchControlBackground),
        new PropertyMetadata(0d, OnBorderThicknessChanged));

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(Color),
        typeof(SearchControlBackground),
        new PropertyMetadata(Colors.Transparent, OnColorChanged));

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(double),
        typeof(SearchControlBackground),
        new PropertyMetadata(0d, OnCornerRadiusChanged));

    public static readonly DependencyProperty ResizeAnimationDurationProperty = DependencyProperty.Register(
        nameof(ResizeAnimationDuration),
        typeof(TimeSpan),
        typeof(SearchControlBackground),
        new PropertyMetadata(TimeSpan.FromSeconds(0.3), OnResizeAnimationDurationChanged));

    public static readonly DependencyProperty ShadowBlurRadiusProperty = DependencyProperty.Register(
        nameof(ShadowBlurRadius),
        typeof(double),
        typeof(SearchControlBackground),
        new PropertyMetadata(9d, OnShadowBlurRadiusChanged));

    public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
        nameof(ShadowColor),
        typeof(Color),
        typeof(SearchControlBackground),
        new PropertyMetadata(Colors.Black, OnShadowColorChanged));

    public static readonly DependencyProperty ShadowOffsetProperty = DependencyProperty.Register(
        nameof(ShadowOffset),
        typeof(Point),
        typeof(SearchControlBackground),
        new PropertyMetadata(new Point(0, 0), OnShadowOffsetChanged));

    public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
        nameof(ShadowOpacity),
        typeof(double),
        typeof(SearchControlBackground),
        new PropertyMetadata(1d, OnShadowOpacityChanged));

    private readonly RectangleClip _clip;
    private readonly Compositor _compositor = Window.Current.Compositor;
    private readonly ContainerVisual _containerVisual;
    private readonly ShapeVisual _contentVisual;
    private readonly Vector2KeyFrameAnimation _resizeAnimation;
    private readonly CompositionSpriteShape _roundedRectangle;
    private readonly CompositionRoundedRectangleGeometry _roundedRectangleGeometry;
    private readonly DropShadow _shadow;

    public SearchControlBackground()
    {
        _shadow = _compositor.CreateDropShadow();
        UpdateShadowColor();
        UpdateShadowBlurRadius();
        UpdateShadowOffset();
        UpdateShadowOpacity();

        SpriteVisual shadowVisual = _compositor.CreateSpriteVisual();
        shadowVisual.RelativeSizeAdjustment = Vector2.One;
        shadowVisual.Shadow = _shadow;

        _roundedRectangleGeometry = _compositor.CreateRoundedRectangleGeometry();
        UpdateRoundedRectangleGeometryRadius();

        _roundedRectangle = _compositor.CreateSpriteShape(_roundedRectangleGeometry);
        UpdateRoundedRectangleFillColor();
        UpdateRoundedRectangleStrokeThickness();
        UpdateRoundedRectangleStrokeColor();

        _contentVisual = _compositor.CreateShapeVisual();
        _contentVisual.Shapes.Add(_roundedRectangle);
        _contentVisual.RelativeSizeAdjustment = Vector2.One;

        _containerVisual = _compositor.CreateContainerVisual();
        _containerVisual.Children.InsertAtBottom(shadowVisual);
        _containerVisual.Children.InsertAtTop(_contentVisual);

        CompositionVisualSurface contentVisualSurface = _compositor.CreateVisualSurface();
        contentVisualSurface.SourceVisual = _contentVisual;
        contentVisualSurface.StartAnimation(nameof(contentVisualSurface.SourceSize), _containerVisual.GetReference().Size);
        CompositionSurfaceBrush contentVisualSurfaceBrush = _compositor.CreateSurfaceBrush(contentVisualSurface);
        _shadow.Mask = contentVisualSurfaceBrush;

        _roundedRectangleGeometry.StartAnimation(nameof(_roundedRectangleGeometry.Size), _containerVisual.GetReference().Size);

        _clip = _compositor.CreateRectangleClip();
        _clip.StartAnimation(nameof(_clip.Right), _containerVisual.GetReference().Size.X);
        _clip.StartAnimation(nameof(_clip.Bottom), _containerVisual.GetReference().Size.Y);
        UpdateClipRadius();
        _contentVisual.Clip = _clip;

        ImplicitAnimationCollection implicitAnimationCollection = _compositor.CreateImplicitAnimationCollection();
        _resizeAnimation = _compositor.CreateVector2KeyFrameAnimation();
        _resizeAnimation.InsertExpressionKeyFrame(0f, "this.StartingValue.Y > 0 ? Vector2(this.FinalValue.X, this.StartingValue.Y) : this.FinalValue");
        _resizeAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
        _resizeAnimation.Target = nameof(_containerVisual.Size);
        UpdateResizeAnimationDuration();
        implicitAnimationCollection[nameof(_containerVisual.Size)] = _resizeAnimation;
        _containerVisual.ImplicitAnimations = implicitAnimationCollection;

        ElementCompositionPreview.SetElementChildVisual(this, _containerVisual);

        SizeChanged += OnSizeChanged;
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public double BorderThickness
    {
        get => (double)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public TimeSpan ResizeAnimationDuration
    {
        get => (TimeSpan)GetValue(ResizeAnimationDurationProperty);
        set => SetValue(ResizeAnimationDurationProperty, value);
    }

    public double ShadowBlurRadius
    {
        get => (double)GetValue(ShadowBlurRadiusProperty);
        set => SetValue(ShadowBlurRadiusProperty, value);
    }

    public Color ShadowColor
    {
        get => (Color)GetValue(ShadowColorProperty);
        set => SetValue(ShadowColorProperty, value);
    }

    public Point ShadowOffset
    {
        get => (Point)GetValue(ShadowOffsetProperty);
        set => SetValue(ShadowOffsetProperty, value);
    }

    public double ShadowOpacity
    {
        get => (double)GetValue(ShadowOpacityProperty);
        set => SetValue(ShadowOpacityProperty, value);
    }

    private static void OnBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateRoundedRectangleStrokeColor();
    }

    private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateRoundedRectangleStrokeThickness();
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateRoundedRectangleFillColor();
    }

    private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateRoundedRectangleGeometryRadius();
        self.UpdateClipRadius();
    }

    private static void OnResizeAnimationDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateResizeAnimationDuration();
    }

    private static void OnShadowBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateShadowBlurRadius();
    }

    private static void OnShadowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateShadowColor();
    }

    private static void OnShadowOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateShadowOffset();
    }

    private static void OnShadowOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SearchControlBackground self = (SearchControlBackground)d;
        self.UpdateShadowOpacity();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        _containerVisual.Size = e.NewSize.ToVector2();
    }

    private void UpdateClipRadius()
    {
        float cornerRadius = (float)CornerRadius;
        _clip.TopLeftRadius = new Vector2(cornerRadius);
        _clip.TopRightRadius = new Vector2(cornerRadius);
        _clip.BottomRightRadius = new Vector2(cornerRadius);
        _clip.BottomLeftRadius = new Vector2(cornerRadius);
    }

    private void UpdateResizeAnimationDuration()
    {
        _resizeAnimation.Duration = ResizeAnimationDuration;
    }

    private void UpdateRoundedRectangleFillColor()
    {
        _roundedRectangle.FillBrush = _compositor.CreateColorBrush(Color);
    }

    private void UpdateRoundedRectangleGeometryRadius()
    {
        float cornerRadius = (float)CornerRadius;
        _roundedRectangleGeometry.CornerRadius = new Vector2(cornerRadius);
    }

    private void UpdateRoundedRectangleStrokeColor()
    {
        _roundedRectangle.StrokeBrush = _compositor.CreateColorBrush(BorderColor);
    }

    private void UpdateRoundedRectangleStrokeThickness()
    {
        _roundedRectangle.StrokeThickness = (float)BorderThickness;
    }

    private void UpdateShadowBlurRadius()
    {
        _shadow.BlurRadius = (float)ShadowBlurRadius;
    }

    private void UpdateShadowColor()
    {
        _shadow.Color = ShadowColor;
    }

    private void UpdateShadowOffset()
    {
        _shadow.Offset = new Vector3(ShadowOffset.ToVector2(), 0);
    }

    private void UpdateShadowOpacity()
    {
        _shadow.Opacity = (float)ShadowOpacity;
    }
}