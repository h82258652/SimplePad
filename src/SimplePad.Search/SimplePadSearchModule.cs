using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;

namespace SimplePad.Search;

public sealed class SimplePadSearchModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddScoped(_ => new SearchViewState());
    }
}
