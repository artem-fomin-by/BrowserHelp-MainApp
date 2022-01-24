using Logic.BrowserLogic;

namespace WinFormsLogic{
    public class BrowserButton : Button{
        private readonly Browser BrowserToLaunch;
        private readonly string Link;

        private void LaunchBrowser(object? sender, EventArgs args){
            BrowserToLaunch.Launch(Link);
            Application.Exit();
        }

        public BrowserButton(Browser browser, string link){ 
            Name = browser.Name;
            BrowserToLaunch = browser;
            Link = link;
            Click += LaunchBrowser;
        }

        public void Initialise(int x, int y){

        }
    }
}
