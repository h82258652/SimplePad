using System;
using System.Threading.Tasks;
using SimplePad.Tabs.Properties;

namespace SimplePad.Tabs;

internal sealed class WPFTabsSettings : ITabsSettings
{
    private OpenFileBehavior _openFileBehavior = OpenFileBehavior.NewTab;

    public event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;

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

    public Task LoadAsync()
    {
        OpenFileBehavior = OpenFileBehavior.FromValue(Settings.Default.OpenFileBehavior);
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Settings.Default.OpenFileBehavior = OpenFileBehavior.Value;
        Settings.Default.Save();
        return Task.CompletedTask;
    }
}