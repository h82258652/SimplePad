using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Tabs;

public sealed class OpenFileBehaviorComboBox : ComboBox
{
    private readonly ITabsSettings _tabsSettings;

    public OpenFileBehaviorComboBox()
    {
        _tabsSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();        
    }
}
