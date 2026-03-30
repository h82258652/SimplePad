using System;
using System.Threading.Tasks;
using SimplePad.MultiTab.Settings;

namespace SimplePad.MultiTab.UWP.Settings;

public sealed class UWPMultiTabSettings : IMultiTabSettings
{
    public UWPMultiTabSettings()
    {
    }

    public OpenFileBehavior OpenFileBehavior { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
