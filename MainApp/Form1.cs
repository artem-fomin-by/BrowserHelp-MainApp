using WinFormsLogic;

namespace MainApp{
    public partial class Form1 : Form{
        public Form1(BrowserButton[] buttons, string name){
            Buttons = buttons;
            Name = name;
            
            InitializeComponent();
        }
    }
}