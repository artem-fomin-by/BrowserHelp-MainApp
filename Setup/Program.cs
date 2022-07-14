using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using WixSharp;
using File = System.IO.File;
using WixSharpFile = WixSharp.File;

namespace Setup
{
    public static class Program
    {
        public const string AppName = "BrowserHelper";
        public const string MainDirectoryName = "Browser Helper";
        public const string MainAppFilesDirectory = @"BrowserHelper\bin\Debug\net6.0-windows";

        public static readonly string InstallDirPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
            MainDirectoryName
        );

        public static void Main()
        {
            CreateInstaller();
        }

        private static void CreateInstaller()
        {
            var (mainAppDirPath, mainDirPath) = GetMainDirs();
            var installDir = new Dir(InstallDirPath);
            installDir.Files = GetFiles(mainDirPath, mainAppDirPath);

            var project = new Project
            {
                Name = MainDirectoryName,
                Version = new Version("1.0"),
                Language = "en-US",
                Dirs = new[] {installDir},
                GUID = new Guid("2601159C-2189-433D-9CB1-D3E8A672A9A0"),
                RegValues = GetRegValues(mainDirPath, InstallDirPath)
            };

            project.BuildMsi();
        }

        private static (string, string) GetMainDirs()
        {
            var currentDirPath = Environment.CurrentDirectory;
            var mainDirPath = currentDirPath.Substring(0,
                currentDirPath.IndexOf(MainDirectoryName, StringComparison.CurrentCulture)
                + MainDirectoryName.Length + 1);
            var mainAppDirPath = Path.Combine(mainDirPath, MainAppFilesDirectory);
            return (mainAppDirPath, mainDirPath);
        }

        private static WixSharpFile[] GetFiles(string mainDirPath, string mainAppDirPath)
        {
            var filePathsSourceFilePath = Path.Combine(mainDirPath, "FilePaths.json");

            using(var filePathsSourceFile =
                  File.Open(filePathsSourceFilePath, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(JsonFilePathsSet));
                var filePathsSet = (JsonFilePathsSet) serializer.ReadObject(filePathsSourceFile);
                return filePathsSet.FilePaths
                    .Select(x => new WixSharpFile(Path.Combine(mainAppDirPath, x)))
                    .ToArray();
            }
        }

        private static RegValue[] GetRegValues(string mainDirPath, string mainAppDirPath)
        {
            var mainExeFilePath = Path.Combine(mainAppDirPath, AppName + ".exe");

            RegValue[] res;
            var registrySetupPath = Path.Combine(mainDirPath, "RegistrySetup.json");
            using(var registrySetupFile = File.Open(registrySetupPath, FileMode.Open, FileAccess.Read))
            {
                var serializer = new DataContractJsonSerializer(typeof(JsonRegKeySet));
                var regKeySet = (JsonRegKeySet) serializer.ReadObject(registrySetupFile);
                res = regKeySet.RegistryKey_ValuesSets
                    .Select(x => x.Values.Select(y => (x.MainKey, x.Path, y.Name, y.Value)))
                    .SelectMany(x => x)
                    .Select(x => (x.MainKey, string.Format(x.Path, AppName), x.Name, x.Value))
                    .Select(x =>
                    {
                        var regPath = x.MainKey + '\\' + x.Item2;
                        string value;
                        if(PathKeys.ContainsKey(regPath) && PathKeys[regPath].Equals(x.Name))
                        {
                            value = string.Format(x.Value, string.Concat("\"", mainExeFilePath, "\""));
                        }
                        else
                        {
                            value = string.Format(x.Value, AppName);
                        }

                        return (x.MainKey, x.Item2, x.Name, value);
                    })
                    .Select(x => (StartKeys[x.MainKey], x.Item2, x.Name, x.Item4))
                    .Select(x => 
                        (x.Item1, x.Item2, string.Format(x.Name, AppName), x.Item4))
                    
                    .Select(x => new RegValue(x.Item1, x.Item2, x.Item3, x.Item4))
                    .ToArray();
            }

            return res;
        }

        private static readonly Dictionary<string, string> PathKeys = new Dictionary<string, string>
        {
            {
                $"HKEY_LOCAL_MACHINE\\SOFTWARE\\{AppName}\\Capabilities",
                "ApplicationIcon"
            },
            {
                $"HKEY_LOCAL_MACHINE\\SOFTWARE\\Clients\\StartMenuInternet\\{AppName}\\Capabilities",
                "ApplicationIcon"
            },
            {
                $"HKEY_LOCAL_MACHINE\\SOFTWARE\\Clients\\StartMenuInternet\\{AppName}\\shell\\open\\command",
                ""
            },
            {
                $"HKEY_LOCAL_MACHINE\\Software\\Classes\\{AppName}URL\\shell\\open\\command",
                ""
            }
        };

        private static readonly Dictionary<string, RegistryHive> StartKeys = new Dictionary<string, RegistryHive>
        {
            {"HKEY_CURRENT_USER", RegistryHive.CurrentUser},
            {"HKEY_CLASSES_ROOT", RegistryHive.ClassesRoot},
            {"HKEY_LOCAL_MACHINE", RegistryHive.LocalMachine},
            {"HKEY_USERS", RegistryHive.Users},
        };
    }
}
