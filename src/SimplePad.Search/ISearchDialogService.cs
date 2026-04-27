using System.Threading.Tasks;

namespace SimplePad.Search;

public interface ISearchDialogService
{
    Task ShowSearchTextNotFoundDialogAsync(string searchText);
}
