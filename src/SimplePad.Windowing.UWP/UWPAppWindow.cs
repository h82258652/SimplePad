using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public sealed class UWPAppWindow : IAppWindow
{
    public UWPAppWindow(IAppWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    private readonly IAppWindowManager _windowManager;

    public SettingsState SettingsState { get; } = new SettingsState();

    private readonly ObservableCollection<AppTabViewModel> _tabs = [];

    public IReadOnlyList<AppTabViewModel> Tabs => _tabs;

    public void Close()
    {
        throw new NotImplementedException();
    }
}
