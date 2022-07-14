using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Logic;
using Pr = Setup.Program;

namespace MainApp;

public class App : Application
{
    public static readonly string AppName = Pr.AppName;

    private static readonly string ConfigurationFolderPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);

    private static readonly string ConfigurationFilePath = 
        Path.Combine(ConfigurationFolderPath, "Configuration.json");

    public static Process? ParentProcess { get; private set; }
    private static Config _configuration;

    [STAThread]
    public static void Main(string[] args)
    {
        _configuration = GetConfiguration(ConfigurationFilePath, ConfigurationFolderPath);
        ParentProcess = ParentProcessUtilities.GetParentProcess();
        var link = args.Length > 0 ? args[0] : null;

        var browser = ChooseBrowser(link, ParentProcess, _configuration);
        if (browser != null)
        {
            browser.Launch(args.FirstOrDefault() ?? "");
        }
        else
        {
            var app = new App();

            if (_configuration.Browsers.Length != 0)
            {
                app.Run(new MainWindow(app, AppName, _configuration.Browsers, link));
            }
            else
            {
                app.Run(new NoBrowsersWindow(AppName));
            }
        }
    }

    private static Browser? ChooseBrowser(string? link, Process? parentProcess, Config config)
    {
        return config.FindBrowser(link ?? "", parentProcess?.ProcessName);
    }

    private static Config GetConfiguration(string configurationFilePath, string configurationFolderPath)
    {
        EnsureConfiguration(configurationFilePath, configurationFolderPath);

        var root = new ConfigurationManager()
            .AddJsonFile(configurationFilePath)
            .Build();

        var instance = new Config();
        root.Bind(instance);
        return instance;
    }

    private static void EnsureConfiguration(string configurationFilePath, string configurationFolderPath)
    {
        if (!Directory.Exists(configurationFolderPath))
        {
            Directory.CreateDirectory(configurationFolderPath);
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
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public void End(Browser selectedBrowser)
    {
        if(ParentProcess == null)
        {
            Shutdown();
            return;
        }

        selectedBrowser.Applications ??= new List<string>();
        selectedBrowser.Applications.Add(ParentProcess.ProcessName);
        _configuration.Deserialize(ConfigurationFilePath);
        Shutdown();
    }

    private App() : base(){ }
}
