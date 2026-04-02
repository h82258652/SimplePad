namespace SimplePad.Settings;

public sealed class SettingsState
{
    private bool _isVisible;

    public event EventHandler<bool>? IsVisibleChanged;

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
