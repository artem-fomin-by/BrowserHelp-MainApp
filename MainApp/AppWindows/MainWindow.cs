using WinFormsLogic;

namespace MainApp.AppWindows{
    internal class MainWindow : Form{
        #region STD_Indent_Constants

        private const int STD_BordersX_Indent = 30;
        private const int STD_BordersY_Indent = 10;
        private const int STD_BFromB_Indent = 10;

        #endregion

        private readonly BrowserButton[] Buttons;

        public MainWindow(BrowserButton[] buttons, string name, string link){
            Buttons = buttons;
            Name = name;

            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = MousePosition;

            foreach(var button in Buttons){
                button.Click += (sender, args) => {
                    button.BrowserToLaunch.Launch(link);
                    Application.Exit();
                };
            }
        }

        protected override void Dispose(bool disposing){
            if(Buttons != null){
                foreach(var button in Buttons){
                    button.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitWindow(int sizeX, int sizeY){
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(sizeX, sizeY);
            Text = Name;
        }

        private void InitializeComponent(){
            SuspendLayout();

            var x = STD_BordersX_Indent;
            var y = STD_BordersY_Indent;
            var i = 0;

            foreach(var button in Buttons){
                button.Initialize(x, y, i++, this);

                y = y + BrowserButton.STD_SizeY + STD_BFromB_Indent;
            }

            InitWindow(STD_BordersX_Indent * 2 + BrowserButton.STD_SizeX, y + STD_BordersY_Indent);

            ResumeLayout(false);
        }
    }
}
