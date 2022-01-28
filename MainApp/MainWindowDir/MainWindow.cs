using WinFormsLogic;

namespace MainApp.MainWindowDir{
    public partial class MainWindow : Form{
        #region STD_Indent_Constants

        private const int STD_BordersX_Indent = 30;
        private const int STD_BordersY_Indent = 10;
        private const int STD_BFromB_Indent = 10;

        #endregion

        private const string STD_ErrorMessage =
            "";

        private const string STD_NoBrowsersMessage =
            "No browthers ar found";

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
            if(AreThereAnyButtons) InitComponent1();
            else InitComponent2();
        }

        public MainWindow(string name, Exception exception){
            Name = name;
        }

        protected override void Dispose(bool disposing){
            if(Buttons != null){
                foreach(var button in Buttons){
                    button.Dispose();
                }
            }

            if(SpecMessageBox != null){
                SpecMessageBox.Dispose();
            }

            if(SpecErrorMessageBox != null){
                SpecErrorMessageBox.Dispose();
            }

            if(SpecExitButton != null){
                SpecExitButton.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
