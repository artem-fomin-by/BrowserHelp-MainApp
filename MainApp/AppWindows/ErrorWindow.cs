namespace MainApp.AppWindows{
    public partial class ErrorWindow : Form{
        public ErrorWindow(string name, Exception exception){
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = MousePosition;

            richTextBox2.Text = exception.Message;

            button1.Click += (sender, args) => {
                Application.Exit();
            };

            Name = name;
            Text = name;
        }
    }
}
