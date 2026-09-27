using FISAssignment.Utils;
using Newtonsoft.Json;
using System;
using System.IO;

public static class ConfigManager
{
    public static AppSettingsSchema ReadConfig()
    {
        var configDetails = File.ReadAllText(
        Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        @"Utils\appSettings.json")
        .Replace(@"\bin\Debug\net8.0", "")
        );

        var appConfig = JsonConvert.DeserializeObject<AppSettingsSchema>(configDetails);

        if (appConfig == null)
        {
            throw new Exception("Unable to load application configuration.");
        }

        return appConfig;
    }
}