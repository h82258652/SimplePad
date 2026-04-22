using System;
using System.Numerics;
using CommunityToolkit.WinUI.Animations.Expressions;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace SimplePad.Search;

/// <summary>
/// A content control that its background size can be animated when the control size is changed.
/// </summary>
[TemplatePart(Name = BackplateHostTemplateName, Type = typeof(Border))]
internal sealed partial class AnimatedBackgroundContentControl : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="BackgroundBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register(
        nameof(BackgroundBrush),
        typeof(ICompositionBrushProvider),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(null, OnBackgroundBrushChanged));

    /// <summary>
    /// Identifies the <see cref="BackgroundGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundGeometryProperty = DependencyProperty.Register(
        nameof(BackgroundGeometry),
        typeof(ICompositionGeometryProvider),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(null, OnBackgroundGeometryChanged));

    /// <summary>
    /// Identifies the <see cref="ResizeAnimationDuration"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ResizeAnimationDurationProperty = DependencyProperty.Register(
        nameof(ResizeAnimationDuration),
        typeof(TimeSpan),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(TimeSpan.FromSeconds(1), OnResizeAnimationDurationChanged));

    /// <summary>
    /// Identifies the <see cref="ShadowProvider"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShadowProviderProperty = DependencyProperty.Register(
        nameof(ShadowProvider),
        typeof(ICompositionShadowProvider),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(null, OnShadowProviderChanged));

    /// <summary>
    /// Identifies the <see cref="StrokeBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrokeBrushProperty = DependencyProperty.Register(
        nameof(StrokeBrush),
        typeof(ICompositionBrushProvider),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(null, OnStrokeBrushChanged));

    /// <summary>
    /// Identifies the <see cref="StrokeThickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
        nameof(StrokeThickness),
        typeof(double),
        typeof(AnimatedBackgroundContentControl),
        new PropertyMetadata(0d, OnStrokeThicknessChanged));

    private const string BackplateHostTemplateName = "PART_BackplateHost";

    private readonly SpriteVisual _backgroundVisual;
    private readonly SpriteVisual _backplateVisual;
    private readonly CompositionGeometricClip _clip;
    private readonly Compositor _compositor;
    private readonly SpriteVisual _containerVisual;
    private readonly Vector2KeyFrameAnimation _resizeAnimation;
    private readonly CompositionSpriteShape _shape;
    private readonly ShapeVisual _shapeVisual;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatedBackgroundContentControl"/> class.
    /// </summary>
    public AnimatedBackgroundContentControl()
    {
        _compositor = Window.Current.Compositor;
        _clip = _compositor.CreateGeometricClip();
        _backgroundVisual = CreateBackgroundVisual(_clip);
        _shape = CreateShape();
        _shapeVisual = CreateShapeVisual(_shape);
        _containerVisual = CreateContainerVisual(_backgroundVisual, _shapeVisual);
        _backplateVisual = CreateBackplateVisual(_containerVisual);

        _resizeAnimation = SetupImplicitSizeAnimation(_backplateVisual);
        UpdateShadow();

        DefaultStyleKey = typeof(AnimatedBackgroundContentControl);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/AnimatedBackgroundContentControl.xaml");

        SizeChanged += OnSizeChanged;
    }

    /// <summary>
    /// Gets or sets the background brush.
    /// </summary>
    public ICompositionBrushProvider? BackgroundBrush
    {
        get => (ICompositionBrushProvider?)GetValue(BackgroundBrushProperty);
        set => SetValue(BackgroundBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the geometry uses for the control's background.
    /// </summary>
    public ICompositionGeometryProvider? BackgroundGeometry
    {
        get => (ICompositionGeometryProvider?)GetValue(BackgroundGeometryProperty);
        set => SetValue(BackgroundGeometryProperty, value);
    }

    /// <summary>
    /// Gets or sets the duration of the resize animation.
    /// </summary>
    public TimeSpan ResizeAnimationDuration
    {
        get => (TimeSpan)GetValue(ResizeAnimationDurationProperty);
        set => SetValue(ResizeAnimationDurationProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow provider.
    /// </summary>
    public ICompositionShadowProvider? ShadowProvider
    {
        get => (ICompositionShadowProvider?)GetValue(ShadowProviderProperty);
        set => SetValue(ShadowProviderProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke brush.
    /// </summary>
    public ICompositionBrushProvider? StrokeBrush
    {
        get => (ICompositionBrushProvider?)GetValue(StrokeBrushProperty);
        set => SetValue(StrokeBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke thickness.
    /// </summary>
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        Border backplateHost = (Border)GetTemplateChild(BackplateHostTemplateName);
        ElementCompositionPreview.SetElementChildVisual(backplateHost, _backplateVisual);
    }

    private static void OnBackgroundBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;

        ICompositionBrushProvider? oldBackgroundBrush = (ICompositionBrushProvider?)e.OldValue;
        oldBackgroundBrush?.OnDisconnected(self);

        ICompositionBrushProvider? newBackgroundBrush = (ICompositionBrushProvider?)e.NewValue;
        newBackgroundBrush?.OnConnected(self);
        self._backgroundVisual.Brush = newBackgroundBrush?.GetBrush();
    }

    private static void OnBackgroundGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;

        ICompositionGeometryProvider? oldBackgroundGeometry = (ICompositionGeometryProvider?)e.OldValue;
        oldBackgroundGeometry?.GeometryInfoContext = null;

        ICompositionGeometryProvider? newBackgroundGeometry = (ICompositionGeometryProvider?)e.NewValue;
        if (newBackgroundGeometry is not null)
        {
            newBackgroundGeometry.GeometryInfoContext = new AnimatedBackgroundContentControlGeometryInfoContext(self);

            CompositionGeometry geometry = newBackgroundGeometry.GetGeometry();
            newBackgroundGeometry.UpdateGeometry();
            self._clip.Geometry = geometry;
            self._shape.Geometry = geometry;
        }
        else
        {
            self._clip.Geometry = null;
            self._shape.Geometry = null;
        }
    }

    private static void OnResizeAnimationDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;
        TimeSpan resizeAnimationDuration = (TimeSpan)e.NewValue;
        self._resizeAnimation.Duration = resizeAnimationDuration;
    }

    private static void OnShadowProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;
        self.UpdateShadow();
    }

    private static void OnStrokeBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;

        ICompositionBrushProvider? oldStrokeBrush = (ICompositionBrushProvider?)e.OldValue;
        oldStrokeBrush?.OnDisconnected(self);

        ICompositionBrushProvider? newStrokeBrush = (ICompositionBrushProvider?)e.NewValue;
        newStrokeBrush?.OnConnected(self);
        self._shape.StrokeBrush = newStrokeBrush?.GetBrush();
    }

    private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedBackgroundContentControl self = (AnimatedBackgroundContentControl)d;
        double strokeThickness = (double)e.NewValue;
        self._shape.StrokeThickness = (float)strokeThickness;
        self.BackgroundGeometry?.UpdateGeometry();
    }

    private SpriteVisual CreateBackgroundVisual(CompositionClip clip)
    {
        SpriteVisual backgroundVisual = _compositor.CreateSpriteVisual();
        backgroundVisual.RelativeSizeAdjustment = Vector2.One;
        backgroundVisual.Brush = BackgroundBrush?.GetBrush();
        backgroundVisual.Clip = clip;
        return backgroundVisual;
    }

    private SpriteVisual CreateBackplateVisual(Visual containerVisual)
    {
        SpriteVisual backplateVisual = _compositor.CreateSpriteVisual();
        backplateVisual.Size = new Vector2((float)ActualWidth, (float)ActualHeight);
        backplateVisual.Children.InsertAtTop(containerVisual);
        return backplateVisual;
    }

    private SpriteVisual CreateContainerVisual(SpriteVisual backgroundVisual, ShapeVisual borderVisual)
    {
        var containerVisual = _compositor.CreateSpriteVisual();
        containerVisual.RelativeSizeAdjustment = Vector2.One;
        containerVisual.Children.InsertAtTop(backgroundVisual);
        containerVisual.Children.InsertAtTop(borderVisual);
        return containerVisual;
    }

    private CompositionSpriteShape CreateShape()
    {
        CompositionSpriteShape shape = _compositor.CreateSpriteShape();
        shape.StrokeThickness = (float)StrokeThickness;
        shape.StrokeBrush = StrokeBrush?.GetBrush();
        return shape;
    }

    private ShapeVisual CreateShapeVisual(CompositionShape shape)
    {
        ShapeVisual shapeVisual = _compositor.CreateShapeVisual();
        shapeVisual.RelativeSizeAdjustment = Vector2.One;
        shapeVisual.Shapes.Add(shape);
        return shapeVisual;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        _backplateVisual.Size = e.NewSize.ToVector2();
        BackgroundGeometry?.UpdateGeometry();
    }

    private Vector2KeyFrameAnimation SetupImplicitSizeAnimation(Visual visual)
    {
        Vector2KeyFrameAnimation resizeAnimation = _compositor.CreateVector2KeyFrameAnimation();
        resizeAnimation.InsertExpressionKeyFrame(0f, "this.StartingValue.X > 0 && this.StartingValue.Y > 0 ? this.StartingValue : this.FinalValue");
        resizeAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
        resizeAnimation.Target = nameof(visual.Size);
        resizeAnimation.Duration = ResizeAnimationDuration;

        ImplicitAnimationCollection implicitAnimations = _compositor.CreateImplicitAnimationCollection();
        implicitAnimations[nameof(visual.Size)] = resizeAnimation;

        visual.ImplicitAnimations = implicitAnimations;

        return resizeAnimation;
    }

    private void UpdateShadow()
    {
        if (ShadowProvider is not { } shadowProvider)
        {
            _backplateVisual.Shadow = null;
            return;
        }

        CompositionVisualSurface visualSurface = _compositor.CreateVisualSurface();
        visualSurface.SourceVisual = _containerVisual;
        visualSurface.StartAnimation(nameof(visualSurface.SourceSize), _backplateVisual.GetReference().Size);

        shadowProvider.SetMask(_compositor.CreateSurfaceBrush(visualSurface));

        _backplateVisual.Shadow = shadowProvider.GetShadow();
    }
}
