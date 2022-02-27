namespace MainApp.AppWindows{
    internal partial class NoBrowsersWindow : Form{
        public NoBrowsersWindow(string name){
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = MousePosition;

            button1.Click += (sender, args) => {
                Application.Exit();
            };

            Name = name;
            Text = name;
        }
    }
}
