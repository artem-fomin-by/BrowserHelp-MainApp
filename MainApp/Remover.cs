using System.Diagnostics;

using Logic;
using Logic.BrowserLogic;

namespace MainApp{
    public static class Remover{
        public static void Remove(string name){
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath), name);

            //Process.Start();
        }
    }
}
