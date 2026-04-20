using System.Threading.Tasks;

namespace SimplePad.Tabs;

public interface IConfirmCloseService
{
    Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab);
}
