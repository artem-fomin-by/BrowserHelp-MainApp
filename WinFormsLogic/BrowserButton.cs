using Logic;

namespace WinFormsLogic;

public class BrowserButton : Button{
    public const int STD_SizeX = 80;
    public const int STD_SizeY = 48;

    public readonly Browser BrowserToLaunch;

    public BrowserButton(Browser browser){
        Name = browser.Name;
        Text = browser.Name;
        BrowserToLaunch = browser;
    }

    public void Initialize(int x, int y, int index, Form window){
        Location = new Point(x, y);
        Size = new Size(STD_SizeX, STD_SizeY);
        TabIndex = index;
        UseVisualStyleBackColor = true;
        window.Controls.Add(this);
    }
}
