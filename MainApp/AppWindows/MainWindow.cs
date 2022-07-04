using Logic;

namespace MainApp.AppWindows
{
    public partial class MainWindow : Form
    {
        public MainWindow(Browser[] browsers, string? link, string? processName)
        {
            InitializeComponent();


            InitButtons(browsers, link);

            var bounds = Screen.FromPoint(MousePosition).WorkingArea;

            var mouseX = MousePosition.X;
            int mouseY = MousePosition.Y;
            
            int sizeX = Width;
            var x = mouseX - sizeX / 2;

            x = Limit(x, bounds.X, bounds.Right - sizeX);

            int sizeY = Height;
            var y = mouseY - sizeY / 2;
            y = Limit(y, bounds.Y, bounds.Bottom - sizeY);

            Location = new Point(x, y);
            label1.Text = processName;
        }

        private static int Limit(int x, int min, int max)
        {
            if (x < min)
            {
                return min;
            }
            return x > max ? max : x;
        }

        private void InitButtons(Browser[] browsers, string? link)
        {
            flowLayoutPanel1.Controls.RemoveAt(0);

            foreach (var browser in browsers)
            {
                var button = new Button
                {
                    Text = browser.Name,
                };

                button.Click += (_, _) =>
                {
                    browser.Launch(link!);
                    Application.Exit();
                };

                flowLayoutPanel1.Controls.Add(button);
            }
        }
    }
}
