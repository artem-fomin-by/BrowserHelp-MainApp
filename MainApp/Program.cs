using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using Logic;
using MainApp.AppWindows;
using Microsoft.Extensions.Configuration;

namespace MainApp;

public static class Program
{
    public const string AppName = "MainApp";

    static string ConfigurationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName); 
    static string ConfigurationFilePath = Path.Combine(ConfigurationFolderPath, "Configuration.json");

    [STAThread]
    public static void Main(string[] args)
    {
        var config = GetConfiguration();

        ApplicationConfiguration.Initialize();

        // var parentProcess = ParentProcessUtilities.GetParentProcess();


        if (config.Browsers.Length != 0)
        {
            Application.Run(new MainWindow(config.Browsers, args.Length > 0 ? args[0] : ""));
        }
        else
        {
            Application.Run(new NoBrowsersWindow(AppName));
        }
    }

    private static Config GetConfiguration()
    {
        EnsureConfiguration(ConfigurationFilePath);

        var root = new ConfigurationManager()
            .AddJsonFile(ConfigurationFilePath)
            .Build();

        var instance = new Config();
        root.Bind(instance);
        return instance;
    }

    private static void EnsureConfiguration(string configurationFilePath)
    {
        if (!Directory.Exists(ConfigurationFolderPath))
        {
            Directory.CreateDirectory(ConfigurationFolderPath);
        }

        if (File.Exists(configurationFilePath))
        { 
            return;
        }   
        
        var browsers = Browser.FindBrowsers(AppName);
        if (browsers.Length != 0)
        {
            using var configurationFile = File.Create(configurationFilePath);
            JsonSerializer.Serialize(configurationFile, new Config
            {
                Browsers = browsers
            });
        }
    }
}

