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

            foreach (var startKey in StartKeys){
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
    }
}
