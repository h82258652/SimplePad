using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class WinUISearchNotificationService : ISearchNotificationService
{
    private CancellationTokenSource? _cts;
    private Action? _hideAction;
    private Action<string>? _setContentAction;
    private Action? _showAction;

    public void ShowFindNextFromTopNotification()
    {
        ShowNotification("Found next from the top");
    }

    public void ShowFindPreviousFromBottomNotification()
    {
        ShowNotification("Found next from the bottom");
    }

    internal void Configure(Action showAction, Action hideAction, Action<string> setContentAction)
    {
        _showAction = showAction;
        _hideAction = hideAction;
        _setContentAction = setContentAction;
    }

    private async void ShowNotification(string notificationContent)
    {
        if (_showAction is not { } showAction
            || _hideAction is not { } hideAction
            || _setContentAction is not { } setContentAction)
        {
            return;
        }

        _cts?.Cancel();
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;
        _cts = cts;

        setContentAction(notificationContent);
        showAction();

        await Task.Delay(TimeSpan.FromSeconds(2));

        if (!cancellationToken.IsCancellationRequested)
        {
            hideAction();
        }
    }
}