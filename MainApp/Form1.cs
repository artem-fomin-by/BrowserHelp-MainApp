using WinFormsLogic;

namespace MainApp{
    public partial class Form1 : Form{
        #region STD_Indent_Constants

        private const int STD_BordersX_Indent = 50;
        private const int STD_BordersY_Indent = 50;
        private const int STD_BFromB_Indent = 30;

        #endregion

        private const string STD_NoBrowsersMessage =
            "";

        public Form1(BrowserButton[] buttons, string name, string link){
            Buttons = buttons;
            Name = name;
            
            InitializeComponent(buttons != null && Buttons.Length > 0);

            foreach(var button in Buttons){
                button.Click += (sender, args) => {
                    button.BrowserToLaunch.Launch(link);
                    Application.Exit();
                };
            }
        }

        public void InitializeComponent(bool AreThereAnyButtons){
            if(AreThereAnyButtons){
                InitComponent1();
            }

            InitComponent2();
        }
    }
}
