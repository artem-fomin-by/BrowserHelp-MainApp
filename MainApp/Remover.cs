using System.Diagnostics;

using Logic.BrowserLogic;

namespace Logic{
    public static class Remover{
        public static void Remove(string name){
            WorkWithReg.DeleteKey(WorkWithReg.GetKey(BrowserServ.BrowsersKeyPath), name);

            //Process.Start();
        }
    }
}
