using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePad.Menu
{
    public sealed class SimplePadMenuModule : AppModuleBase
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            context.Services.Configure<MenuBarOptions>(options =>
            {
                options.FileItems.AddRange(
                    new MenuItem("New window", serviceProvider => { }) { },
                    new MenuItem("Open", serviceProvider => { }),
                    new MenuItem("Save", serviceProvider => { }),
                    new MenuItem("Save as", serviceProvider => { }),
                    new MenuItem("Close window", serviceProvider => { }),
                    new MenuItem("Exit", serviceProvider => { })
                    );

                options.EditItems.AddRange(
                    new MenuItem("Undo", serviceProvider => { }),
                    new MenuItemSeparator(),
                    new MenuItem("Cut", serviceProvider => { }),
                    new MenuItem("Copy", serviceProvider => { }),
                    new MenuItem("Paste", serviceProvider => { }));

                options.ViewItems.AddRange(
                    new MenuItemGroup("Zoom")
                    {
                        Children = [
                        new MenuItem("Zoom in", serviceProvider =>{}),
                        new MenuItem("Zoom out", serviceProvider =>{}),
                        new MenuItem("Restore default zoom", serviceProvider =>{})
                        ]
                    },
                    new ToggleMenuItem("Status bar", (serviceProvider) =>
                    {
                        throw new NotImplementedException();
                    }, (serviceProvider, isChecked) =>
                    {
                        // TODO
                    })
                    { },
                    new ToggleMenuItem("Word wrap", (serviceProvider) =>
                    {
                        return serviceProvider.GetRequiredService<IEditorSettings>().IsWordWrap;
                    }, (serviceProvider, isChecked) =>
                    {
                        serviceProvider.GetRequiredService<IEditorSettings>().IsWordWrap = isChecked;
                    }));
            });
        }
    }
}
