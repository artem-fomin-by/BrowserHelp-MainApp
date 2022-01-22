using Logic;
using Logic.BrowserLogic;

namespace SystemConsoleApp{
    public static class Program{
        private const string KeyPath =
            //@"HKEY_CURRENT_USER\SOFTWARE\Yandex\YandexBrowser";
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet";
            //@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\Google Chrome";
            //@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\Microsoft Edge";
            //@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\Shell\Associations\URLAssociations\http";

        public static void Main(){
            BrowserServ.FindBrowsers("")[0].Launch();
        }
    }
}
