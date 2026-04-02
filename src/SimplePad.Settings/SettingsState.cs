namespace SimplePad.Settings;

public sealed class SettingsState
{
    private bool _isFontSettingsExpanded;
    private bool _isVisible;

    public event EventHandler<bool>? IsFontSettingsExpandedChanged;

    public event EventHandler<bool>? IsVisibleChanged;

    public bool IsFontSettingsExpanded
    {
        get => _isFontSettingsExpanded;
        set
        {
            if (_isFontSettingsExpanded != value)
            {
                _isFontSettingsExpanded = value;
                IsFontSettingsExpandedChanged?.Invoke(this, value);
            }
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                IsVisibleChanged?.Invoke(this, value);
            }
        }
    }
}