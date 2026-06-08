using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class AvaloniaAppWindowManager : IAppWindowManager
{
    public IAppWindow? CurrentWindow => throw new NotImplementedException();

    public IReadOnlyList<IAppWindow> Instances => throw new NotImplementedException();

    public Task<bool> CloseAsync(IAppWindow window)
    {
        throw new NotImplementedException();
    }

    public IAppWindow CreateAppWindow()
    {
        throw new NotImplementedException();
    }

    public Task<IAppWindow> ShowNewWindowAsync()
    {
        throw new NotImplementedException();
    }
}
