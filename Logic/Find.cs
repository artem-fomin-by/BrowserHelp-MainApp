﻿using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Logic{
    public static class Find{
        public static RegistryKey GetKey(string link){
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new NotSupportedException("The GetKey function does not support any OS but Microsoft Windows");

            RegistryKey cur;
            var keysNames = link.Split(@"\");

            switch (keysNames[0])
            {
                case "HKEY_CURRENT_USER":
                    cur = Registry.CurrentUser;
                    break;
                case "HKEY_LOCAL_MACHINE":
                    cur = Registry.LocalMachine;
                    break;
                case "HKEY_USERS":
                    cur = Registry.Users;
                    break;
                case "HKEY_CLASSES_ROOT":
                    cur = Registry.ClassesRoot;
                    break;
                case "HKEY_CURRENT_CONFIG":
                    cur = Registry.CurrentConfig;
                    break;
                case "HKEY_PERFORMANCE_DATA":
                    cur = Registry.PerformanceData;
                    break;
                default: throw new ArgumentException(
                    "Cannot find the RegistryKey: " + keysNames[0] + "\nFull key: " + link);
            }

            for (int i = 1; i < keysNames.Length; i++)
            {
                cur = cur.OpenSubKey(keysNames[i]);
                if (cur == null) 
                    throw new ArgumentException(new KeyNotFoundException(keyName));
            }

            return cur;
        }
    }
}
