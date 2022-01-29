using Microsoft.Win32;

using Logic;
using Logic.BrowserLogic;

namespace MainApp{
    public static class Installer{
        public static void Install(string name){
            var browsersKey = WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath);

            browsersKey.CreateSubKey(name);
        }
    }
}
