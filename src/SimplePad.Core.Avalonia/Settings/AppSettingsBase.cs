using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimplePad.Core.Settings;

public abstract class AppSettingsBase : IAppSettings
{
    private static readonly JsonSerializerOptions SerializeOptions = new()
    {
        WriteIndented = true
    };

    private readonly string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimplePad", "settings.json");

    public async Task LoadAsync()
    {
        Dictionary<string, object?>? settings = null;

        try
        {
            if (File.Exists(SettingsPath))
            {
                string json = await File.ReadAllTextAsync(SettingsPath);
                settings = JsonSerializer.Deserialize<Dictionary<string, object?>>(json);
            }
        }
        catch
        {
        }

        settings ??= [];
        SetSettings(settings);
    }

    public async Task SaveAsync()
    {
        Dictionary<string, object?> settings = GetSettings();

        try
        {
            string directory = Path.GetDirectoryName(SettingsPath)!;
            Directory.CreateDirectory(directory);

            string json = JsonSerializer.Serialize(settings, SerializeOptions);
            await File.WriteAllTextAsync(SettingsPath, json);
        }
        catch
        {
        }
    }

    protected abstract Dictionary<string, object?> GetSettings();

    protected abstract void SetSettings(Dictionary<string, object?> settings);
}