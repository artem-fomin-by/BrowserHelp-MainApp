using WinFormsLogic;

namespace MainApp.MainWindowDir{
    partial class MainWindow{
        protected override void Dispose(bool disposing){
            foreach(var button in Buttons){
                button.Dispose();
            }

            base.Dispose(disposing);
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

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(STD_BordersX_Indent * 2 + BrowserButton.STD_SizeX, y + STD_BordersY_Indent);
            Text = Name;

            ResumeLayout(false);
        }

        private void InitComponent2(){

        }

        private void InitComponent3(){

        }
    }
}
