using Logic.BrowserLogic;

namespace WinFormsLogic{
    public class BrowserButton : Button{
        public const int STD_SizeX = 100;
        public const int STD_SizeY = 30;

        private readonly Browser BrowserToLaunch;
        private readonly string Link;

        private void LaunchBrowser(object? sender, EventArgs args){
            BrowserToLaunch.Launch(Link);
            Application.Exit();
        }

        public BrowserButton(Browser browser, string link){ 
            Name = browser.Name;
            Text = browser.Name;
            BrowserToLaunch = browser;
            Link = link;
            Click += LaunchBrowser;
        }

        public void Initialize(int x, int y, int index){
            Location = new Point(x, y);
            Size = new Size();
            TabIndex = index;
            UseVisualStyleBackColor = true;
        }
    }
}
