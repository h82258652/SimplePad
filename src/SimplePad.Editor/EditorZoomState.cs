using System;

namespace SimplePad.Editor;

public sealed class EditorZoomState
{
    private const double MaxZoomFactor = 5;

    private const double MinZoomFactor = 0.1;

    private const double ZoomChangeDelta = 0.1;

    private bool _canZoomIn = true;
    private bool _canZoomOut = true;
    private double _zoomFactor = 1;

    public event EventHandler<bool>? CanZoomInChanged;

    public event EventHandler<bool>? CanZoomOutChanged;

    public event EventHandler<double>? ZoomFactorChanged;

    public bool CanZoomIn
    {
        get => _canZoomIn;
        private set
        {
            if (_canZoomIn != value)
            {
                _canZoomIn = value;
                CanZoomInChanged?.Invoke(this, _canZoomIn);
            }
        }
    }

    public bool CanZoomOut
    {
        get => _canZoomOut;
        private set
        {
            if (_canZoomOut != value)
            {
                _canZoomOut = value;
                CanZoomOutChanged?.Invoke(this, _canZoomOut);
            }
        }
    }

    public double ZoomFactor
    {
        get { return _zoomFactor; }
        set
        {
            double clampedValue = Math.Clamp(value, MinZoomFactor, MaxZoomFactor);
            if (_zoomFactor != clampedValue)
            {
                _zoomFactor = clampedValue;
                ZoomFactorChanged?.Invoke(this, _zoomFactor);

                CanZoomIn = _zoomFactor < MaxZoomFactor;
                CanZoomOut = _zoomFactor > MinZoomFactor;
            }
        }
    }

    public void ResetZoomFactor()
    {
        ZoomFactor = 1;
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
