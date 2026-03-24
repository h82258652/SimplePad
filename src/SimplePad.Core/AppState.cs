using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.Core;

public sealed partial class AppState : ObservableObject
{
    private const double DefaultZoomFactor = 1d;
    private const double ZoomChangeDelta = 0.1d;

    private double _zoomFactor = DefaultZoomFactor;

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
                SetProperty(ref _zoomFactor, clampedValue);
                OnPropertyChanged(nameof(CanZoomIn));
                OnPropertyChanged(nameof(CanZoomOut));
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
