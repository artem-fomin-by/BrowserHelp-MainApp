using Microsoft.Win32;

namespace SystemConsoleApp{
    public static class Program{
        private const string KeyPath =
        //@"HKEY_CURRENT_USER\SOFTWARE\Yandex\YandexBrowser";
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet";   
        //@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\Shell\Associations\URLAssociations\http";

        private static RegistryKey GetKey(string link){
            RegistryKey cur;
            var keysNames = link.Split(@"\");

            switch(keysNames[0]){
                case "HKEY_CURRENT_USER": cur = Registry.CurrentUser; break;
                case "HKEY_LOCAL_MACHINE": cur = Registry.LocalMachine; break;
                default: throw new ArgumentException("Cannot find the RegistryKey: " + keysNames[0]);
            }

            for(int i = 1; i < keysNames.Length; i++){
                cur = cur.OpenSubKey(keysNames[i]);
                if(cur == null) throw new ArgumentException("Cannot find the RegistryKey: " + keysNames[i]);
            }

            return cur;
        }

        public static void Main(string[] args){
            var key = GetKey(KeyPath);
            
            Console.WriteLine(string.Join('\n', key.GetSubKeyNames()));
            Console.WriteLine(string.Join('\n', key.GetValueNames()));

            Console.ReadKey();
        }
    }
}
