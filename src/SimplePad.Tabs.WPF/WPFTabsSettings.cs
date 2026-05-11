using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

internal sealed class WPFTabsSettings : ITabsSettings
{
    private OpenFileBehavior _openFileBehavior = OpenFileBehavior.NewTab;

    public OpenFileBehavior OpenFileBehavior 
    {
        get => _openFileBehavior;
        set
        {
            if (_openFileBehavior != value)
            {
                _openFileBehavior = value;
                OpenFileBehaviorChanged?.Invoke(this, value);
            }
        }
    }

    public event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
