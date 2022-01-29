namespace MainApp.AppWindows{
    public partial class NoBrowsersWindow : Form{
        public NoBrowsersWindow(string name){
            InitializeComponent();

            button1.Click += (sender, args) => {
                Application.Exit();
            };

            Name = name;
            Text = name;
        }
    }
}
