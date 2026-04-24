using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace SimplePad.Search
{

    public sealed partial class AnimatedElement : FrameworkElement
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color),
            typeof(Color),
            typeof(AnimatedElement),
            new PropertyMetadata(Colors.Transparent, OnColorChanged));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius),
            typeof(double),
            typeof(AnimatedElement),
            new PropertyMetadata(0d, OnRadiusChanged));

        public static readonly DependencyProperty ResizeAnimationDurationProperty = DependencyProperty.Register(
            nameof(ResizeAnimationDuration),
            typeof(TimeSpan),
            typeof(AnimatedElement),
            new PropertyMetadata(TimeSpan.FromMilliseconds(300), OnResizeAnimationDurationChanged));

        public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
            nameof(ShadowColor),
            typeof(Color),
            typeof(AnimatedElement),
            new PropertyMetadata(Colors.Black, OnShadowColorChanged));

        public static readonly DependencyProperty ShadowOffsetProperty = DependencyProperty.Register(
            nameof(ShadowOffset),
            typeof(Point),
            typeof(AnimatedElement),
            new PropertyMetadata(new Point(0, 4), OnShadowOffsetChanged));

        public static readonly DependencyProperty ShadowBlurRadiusProperty = DependencyProperty.Register(
            nameof(ShadowBlurRadius),
            typeof(double),
            typeof(AnimatedElement),
            new PropertyMetadata(16d, OnShadowBlurRadiusChanged));

        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
            nameof(ShadowOpacity),
            typeof(double),
            typeof(AnimatedElement),
            new PropertyMetadata(0.3d, OnShadowOpacityChanged));

        private readonly Compositor _compositor = Window.Current.Compositor;
        private readonly ContainerVisual _containerVisual;
        private readonly SpriteVisual _shadowVisual;
        private readonly SpriteVisual _contentVisual;
        private readonly DropShadow _shadow;
        private readonly RectangleClip _clip;
        private readonly CompositionRoundedRectangleGeometry _shadowGeometry;
        private readonly ShapeVisual _shadowMaskVisual;
        private readonly CompositionVisualSurface _shadowMaskSurface;
        private readonly ImplicitAnimationCollection _visualImplicitAnimations;
        private readonly ImplicitAnimationCollection _clipImplicitAnimations;


        public AnimatedElement()
        {
            _containerVisual = _compositor.CreateContainerVisual();

            // Bottom child: shadow visual (unclipped so the shadow is visible)
            _shadowVisual = _compositor.CreateSpriteVisual();
            _shadowVisual.Brush = _compositor.CreateColorBrush(Colors.Transparent);

            _shadow = _compositor.CreateDropShadow();
            _shadow.BlurRadius = (float)ShadowBlurRadius;
            _shadow.Offset = new Vector3((float)ShadowOffset.X, (float)ShadowOffset.Y, 0);
            _shadow.Color = ShadowColor;
            _shadow.Opacity = (float)ShadowOpacity;

            _shadowGeometry = _compositor.CreateRoundedRectangleGeometry();
            var shape = _compositor.CreateSpriteShape(_shadowGeometry);
            shape.FillBrush = _compositor.CreateColorBrush(Colors.Black);

            _shadowMaskVisual = _compositor.CreateShapeVisual();
            _shadowMaskVisual.Shapes.Add(shape);

            _shadowMaskSurface = _compositor.CreateVisualSurface();
            _shadowMaskSurface.SourceVisual = _shadowMaskVisual;

            _shadow.Mask = _compositor.CreateSurfaceBrush(_shadowMaskSurface);
            _shadowVisual.Shadow = _shadow;

            // Top child: content visual (clipped with rounded corners)
            _contentVisual = _compositor.CreateSpriteVisual();
            _contentVisual.Brush = _compositor.CreateColorBrush(Colors.Transparent);

            _clip = _compositor.CreateRectangleClip();
            _contentVisual.Clip = _clip;

            _containerVisual.Children.InsertAtBottom(_shadowVisual);
            _containerVisual.Children.InsertAtTop(_contentVisual);

            // Implicit animations
            _visualImplicitAnimations = _compositor.CreateImplicitAnimationCollection();
            _clipImplicitAnimations = _compositor.CreateImplicitAnimationCollection();

            ConfigureResizeAnimations();

            _shadowVisual.ImplicitAnimations = _visualImplicitAnimations;
            _contentVisual.ImplicitAnimations = _visualImplicitAnimations;
            _clip.ImplicitAnimations = _clipImplicitAnimations;

            ElementCompositionPreview.SetElementChildVisual(this, _containerVisual);

            SizeChanged += OnSizeChanged;
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public TimeSpan ResizeAnimationDuration
        {
            get => (TimeSpan)GetValue(ResizeAnimationDurationProperty);
            set => SetValue(ResizeAnimationDurationProperty, value);
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

        public double ShadowBlurRadius
        {
            get => (double)GetValue(ShadowBlurRadiusProperty);
            set => SetValue(ShadowBlurRadiusProperty, value);
        }

        public double ShadowOpacity
        {
            get => (double)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        private void ConfigureResizeAnimations()
        {
            // Animate visual size
            var sizeAnimation = _compositor.CreateVector2KeyFrameAnimation();
            sizeAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
            sizeAnimation.Duration = ResizeAnimationDuration;
            sizeAnimation.Target = "Size";
            _visualImplicitAnimations["Size"] = sizeAnimation;

            // Animate clip edges to stay in sync with visual size
            var clipRightAnimation = _compositor.CreateScalarKeyFrameAnimation();
            clipRightAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
            clipRightAnimation.Duration = ResizeAnimationDuration;
            clipRightAnimation.Target = "Right";
            _clipImplicitAnimations["Right"] = clipRightAnimation;

            var clipBottomAnimation = _compositor.CreateScalarKeyFrameAnimation();
            clipBottomAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
            clipBottomAnimation.Duration = ResizeAnimationDuration;
            clipBottomAnimation.Target = "Bottom";
            _clipImplicitAnimations["Bottom"] = clipBottomAnimation;
        }

        private static void OnResizeAnimationDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedElement)d).ConfigureResizeAnimations();
        }

        private static void OnShadowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedElement)d)._shadow.Color = (Color)e.NewValue;
        }

        private static void OnShadowOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var point = (Point)e.NewValue;
            ((AnimatedElement)d)._shadow.Offset = new Vector3((float)point.X, (float)point.Y, 0);
        }

        private static void OnShadowBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedElement)d)._shadow.BlurRadius = (float)(double)e.NewValue;
        }

        private static void OnShadowOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedElement)d)._shadow.Opacity = (float)(double)e.NewValue;
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (AnimatedElement)d;
            element._contentVisual.Brush = element._compositor.CreateColorBrush((Color)e.NewValue);
        }

        private static void OnRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (AnimatedElement)d;
            var radius = new Vector2((float)(double)e.NewValue);
            element._clip.TopLeftRadius = radius;
            element._clip.TopRightRadius = radius;
            element._clip.BottomLeftRadius = radius;
            element._clip.BottomRightRadius = radius;
            element._shadowGeometry.CornerRadius = radius;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newSize = e.NewSize;
            var size = new Vector2((float)newSize.Width, (float)newSize.Height);
            _containerVisual.Size = size;
            _shadowVisual.Size = size;
            _contentVisual.Size = size;
            _clip.Right = size.X;
            _clip.Bottom = size.Y;
            _shadowGeometry.Size = size;
            _shadowMaskVisual.Size = size;
            _shadowMaskSurface.SourceSize = size;
        }
    }
}
