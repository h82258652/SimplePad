using System;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class UWPSearchDialogService : ISearchDialogService
{
    public async Task ShowSearchTextNotFoundDialogAsync(string searchText)
    {
        SearchTextNotFoundDialog dialog = new(searchText);
        await dialog.ShowAsync();
    }
}
