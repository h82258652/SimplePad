using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Editor.Settings;
using SimplePad.StatusBar.Settings;
using System;
using System.Collections.Generic;

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
                        new MenuItem("Zoom in", serviceProvider =>
                        {
                            serviceProvider.GetRequiredService<EditorZoomState>().ZoomIn();
                        }),
                        new MenuItem("Zoom out", serviceProvider =>
                        {
                            serviceProvider.GetRequiredService<EditorZoomState>().ZoomOut();
                        }),
                        new MenuItem("Restore default zoom", serviceProvider =>{
                            serviceProvider.GetRequiredService<EditorZoomState>().ResetZoomFactor();
                        })
                        ]
                    },
                    new ToggleMenuItem("Status bar", (serviceProvider) =>
                    {
                        return serviceProvider.GetRequiredService<IStatusBarSettings>().IsStatusBarVisible;
                    }, (serviceProvider, isChecked) =>
                    {
                        serviceProvider.GetRequiredService<IStatusBarSettings>().IsStatusBarVisible = isChecked;
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
