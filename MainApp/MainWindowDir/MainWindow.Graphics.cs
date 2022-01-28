using WinFormsLogic;

namespace MainApp.MainWindowDir{
    partial class MainWindow{
        private void InitWindow(int sizeX, int sizeY){
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(sizeX, sizeY);
            Text = Name;
        }

        private void InitComponent1(){
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

        private readonly BrowserButton[] Buttons;
    }
}
