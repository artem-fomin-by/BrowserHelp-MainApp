using Microsoft.Win32;

using Logic.Exceptions;

namespace Logic{
    public static class Search{
        private static readonly RegistryKey[] StartKeys ={
            Registry.CurrentUser,
            Registry.ClassesRoot,
            Registry.CurrentConfig,
            Registry.LocalMachine,
            Registry.PerformanceData,
            Registry.Users
        };

        public static IEnumerable<RegistryKey> FromBottomToTopDFS(){
            NotSupportedOSException.CheckOS(NotSupportedOSException.Windows);

            foreach(var startKey in StartKeys){
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
            NotSupportedOSException.CheckOS(NotSupportedOSException.Windows);

            RegistryKey cur;
            var keysNames = link.Split(@"\");

            switch(keysNames[0]){
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
                default:
                    throw new ArgumentException("Cannot find the RegistryKey",
                        new RegKeyNotFoundException(keysNames[0], link));
            }

            for(int i = 1; i < keysNames.Length; i++){
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
    }
}
