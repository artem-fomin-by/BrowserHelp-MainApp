using Microsoft.Win32;

namespace Logic{
    public static class WorkWithReg{
        #region StartRegistryKeys

        private static readonly Dictionary<string, RegistryKey> StartKeys = new Dictionary<string, RegistryKey>(
            new[]{
                new KeyValuePair<string, RegistryKey>("HKEY_CURRENT_USER", Registry.CurrentUser),
                new KeyValuePair<string, RegistryKey>("HKEY_CURRENT_CONFIG", Registry.CurrentConfig),
                new KeyValuePair<string, RegistryKey>("HKEY_CLASSES_ROOT", Registry.ClassesRoot),
                new KeyValuePair<string, RegistryKey>("HKEY_LOCAL_MACHINE", Registry.LocalMachine),
                new KeyValuePair<string, RegistryKey>("HKEY_PERFORMANCE_DATA", Registry.PerformanceData),
                new KeyValuePair<string, RegistryKey>("HKEY_USERS", Registry.Users)
            }
        );

        #endregion

        public const string ProgIDsPath = @"HKEY_CLASSES_ROOT";

        public static IEnumerable<RegistryKey> FromBottomToTopDFS(){
            foreach(var startKey in StartKeys.Values){
                foreach(var ret in FromBottomToTopDFS(startKey)){
                    yield return ret;
                }
            }
        }

        private static IEnumerable<RegistryKey> FromBottomToTopDFS(RegistryKey cur){
            var subKeyNames = cur.GetSubKeyNames();

            if(subKeyNames.Length != 0){
                foreach(var subKey in subKeyNames.Select(x => cur.OpenSubKey(x))){
                    foreach(var ret in FromBottomToTopDFS(subKey)){
                        yield return ret;
                    }
                }
            }

            yield return cur;
        }

        public static RegistryKey GetKey(string link){
            return GetKey(link.Split(@"\"), link);
        }

        private static RegistryKey GetKey(string[] keysNames, string link, int n_indexesToIgnore = 0){
            RegistryKey? cur;
            if(!StartKeys.TryGetValue(keysNames[0], out cur)){
                throw new ArgumentException("Cannot find the RegistryKey", nameof(keysNames),
                    new RegKeyNotFoundException(keysNames[0], link));
            }

            for(var i = 1; i < keysNames.Length - n_indexesToIgnore; i++){
                cur = cur.OpenSubKey(keysNames[i]);
                if(cur == null)
                    throw new ArgumentException("Cannot find the RegistryKey",
                        new RegKeyNotFoundException(keysNames[i], link));
            }

            return cur;
        }

        public static RegistryKey GetKey(string link, RegistryKey start){
            var cur = start;
            var keysNames = link.Split(@"\");

            for(int i = 1; i < keysNames.Length; i++){
                cur = cur.OpenSubKey(keysNames[i]);
                if(cur == null)
                    throw new ArgumentException("Cannot find the RegistryKey",
                        new RegKeyNotFoundException(keysNames[i], link));
            }

            return cur;
        }

        public static void DeleteKey(RegistryKey parentKey, string keyToDeleteName){
            parentKey.DeleteSubKeyTree(keyToDeleteName);
        }

        public static RegistryKey CreateRegistryKeysTree(RegistryKey parentKey, string link){
            var cur = parentKey;
            var keysNames = link.Split(@"\");

            for(var i = 1; i < keysNames.Length; i++){
                cur = cur.CreateSubKey(keysNames[i]);
            }

            return cur;
        }
    }
}
