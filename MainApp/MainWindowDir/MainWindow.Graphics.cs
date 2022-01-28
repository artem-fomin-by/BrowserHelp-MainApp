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

                y = y + STD_BordersX_Indent;
            }

            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(STD_BordersX_Indent * 2 + BrowserButton.STD_SizeX,
                2 * STD_BordersY_Indent + i * BrowserButton.STD_SizeY + (i - 1) * STD_BFromB_Indent);
            Text = this.Name;

            ResumeLayout(false);
        }

        private void InitComponent2(){

        }

        private void InitComponent3(){
        }
    }
}
