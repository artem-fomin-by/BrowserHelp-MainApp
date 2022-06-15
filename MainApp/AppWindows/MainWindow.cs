using WinFormsLogic;

namespace MainApp.AppWindows;

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

        foreach(var button in Buttons){
            button.Click += (sender, args) => {
                button.BrowserToLaunch.Launch(link);
                Application.Exit();
            };
        }
    }

    private void InitPosition(int sizeX, int sizeY, int mouseX, int mouseY){
        TopLevel = true;
        StartPosition = FormStartPosition.Manual;

        var bounds = Screen.GetBounds(Point.Empty);

        var x = mouseX - (sizeX / 2);
        if(x < 0) x = 0;
        else if(mouseX > bounds.Right - sizeX) x = bounds.Right - sizeX - 1;

        var y = mouseY - (sizeY / 2);
        if(y < 0) y = 0;
        else if(mouseY > bounds.Bottom - sizeY) y = bounds.Bottom - sizeY - 1;

        Location = new Point(x, y);
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
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Text = Name;
    }

    private void InitializeComponent(){
        SuspendLayout();

        var y = STD_BordersY_Indent;
        var i = 0;

        var sizeX = STD_BordersX_Indent * 2 + BrowserButton.STD_SizeX;
        var sizeY = STD_BordersY_Indent * 2 + BrowserButton.STD_SizeY +
                    (Buttons.Length - 1) * (BrowserButton.STD_SizeY + STD_BFromB_Indent);

        InitPosition(sizeX, sizeY, MousePosition.X, MousePosition.Y);

        foreach (var button in Buttons){
            button.Initialize(STD_BordersX_Indent, y, i++, this);

            y = y + BrowserButton.STD_SizeY + STD_BFromB_Indent;
        }

        InitWindow(sizeX, sizeY);

        ResumeLayout(false);

        TopLevel = true;
    }
}
