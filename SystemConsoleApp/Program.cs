using Logic;

namespace SystemConsoleApp{
    public static class Program{
        private const string KeyPath =
            //@"HKEY_CURRENT_USER\SOFTWARE\Yandex\YandexBrowser";
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\Microsoft Edg\Capabilities";
        //@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\Microsoft Edge\shell\open\command";
        //@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\Shell\Associations\URLAssociations\http";

        public static void Main(string[] args){
            var key = Find.GetKey(KeyPath);

            Console.WriteLine("SubKeys {0}:\n", key.SubKeyCount);
            Console.WriteLine(string.Join('\n', key.GetSubKeyNames()));
            Console.WriteLine("\nValues {0}:\n", key.ValueCount);
            Console.WriteLine(string.Join('\n', key.GetValueNames()));
            Console.WriteLine(key.GetValue(""));

            Console.ReadKey();
        }
    }
}
