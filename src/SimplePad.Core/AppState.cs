using System;

namespace SimplePad.Core;

public sealed partial class AppState
{
    private const double DefaultZoomFactor = 1d;
    private const double ZoomChangeDelta = 0.1d;

    private double _zoomFactor = DefaultZoomFactor;

    public event EventHandler<bool>? CanZoomInChanged;

    public event EventHandler<bool>? CanZoomOutChanged;

    public event EventHandler<double>? ZoomFactorChanged;

    public bool CanZoomIn => ZoomFactor < MaxZoomFactor;

    public bool CanZoomOut => ZoomFactor > MinZoomFactor;

    public double MaxZoomFactor { get; } = 5d;

    public double MinZoomFactor { get; } = 0.1d;

    public double ZoomFactor
    {
        get => _zoomFactor;
        set
        {
            double clampedValue = Math.Clamp(value, MinZoomFactor, MaxZoomFactor);
            if (clampedValue != _zoomFactor)
            {
                _zoomFactor = clampedValue;
                ZoomFactorChanged?.Invoke(this, _zoomFactor);
                CanZoomInChanged?.Invoke(this, CanZoomIn);
                CanZoomOutChanged?.Invoke(this, CanZoomOut);
            }
        }
    }

    public void ResetZoomFactor()
    {
        ZoomFactor = DefaultZoomFactor;
    }

    public void ZoomIn()
    {
        if (CanZoomIn)
        {
            ZoomFactor = Math.Min(MaxZoomFactor, ZoomFactor + ZoomChangeDelta);
        }
    }

    public void ZoomOut()
    {
        if (CanZoomOut)
        {
            ZoomFactor = Math.Max(MinZoomFactor, ZoomFactor - ZoomChangeDelta);
        }
    }
}
