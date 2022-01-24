using WinFormsLogic;

namespace MainApp{
    partial class Form1{
        private readonly BrowserButton[] Buttons;

        //private System.ComponentModel.IContainer components = null;

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing){
            if(disposing && (components != null)){
                components.Dispose();
            }

            base.Dispose(disposing);
        }*/

        protected override void Dispose(bool disposing){
            base.Dispose(disposing);
        }

        private void InitializeComponent(){
            var x = 0;
            var y = 0;

            foreach(var button in Buttons){
                button.Initialise(x, y);
               
                x = x + 0;
                y = y + 0;
            }

            var sizeX = 800;
            var sizeY = 450;

            //this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(sizeX, sizeY);
            this.Text = "BrowserHelper";
        }

    }
}
