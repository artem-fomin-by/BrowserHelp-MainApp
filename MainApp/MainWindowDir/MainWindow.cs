using WinFormsLogic;

namespace MainApp.MainWindowDir{
    public partial class MainWindow : Form{
        #region STD_Indent_Constants

        private const int STD_BordersX_Indent = 50;
        private const int STD_BordersY_Indent = 50;
        private const int STD_BFromB_Indent = 30;

        #endregion

        #region STD_Messages

        private const string STD_ErrorMessage = 
            "";
        private const string STD_NoBrowsersMessage =
            "";

        #endregion

        private readonly BrowserButton[] Buttons;

        public MainWindow(BrowserButton[] buttons, string name, string link){
            Buttons = buttons;
            Name = name;

            InitializeComponent(Buttons != null && Buttons.Length > 0);

            if(Buttons != null){
                foreach(var button in Buttons){
                    button.Click += (sender, args) => {
                        button.BrowserToLaunch.Launch(link);
                        Application.Exit();
                    };
                }
            }
        }

        public void InitializeComponent(bool AreThereAnyButtons){
            if(AreThereAnyButtons){
                InitComponent1();
            }

            InitComponent2();
        }

        public MainWindow(string name, Exception exception){
            Name = name;
        }
    }
}
