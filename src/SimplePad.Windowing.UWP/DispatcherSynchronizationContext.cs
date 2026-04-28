using System;
using System.Threading;
using Windows.UI.Core;

namespace SimplePad.Windowing;

internal sealed class DispatcherSynchronizationContext : SynchronizationContext
{
    private readonly CoreDispatcher _dispatcher;

    public DispatcherSynchronizationContext(CoreDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public override SynchronizationContext CreateCopy()
    {
        return new DispatcherSynchronizationContext(_dispatcher);
    }

    public override async void Post(SendOrPostCallback d, object? state)
    {
        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => d(state));
    }

    public override void Send(SendOrPostCallback d, object? state)
    {
        throw new NotSupportedException();
    }
}
