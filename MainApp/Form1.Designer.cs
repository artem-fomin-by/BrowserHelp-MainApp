﻿using WinFormsLogic;

namespace MainApp{
    partial class Form1{
        private readonly BrowserButton[] Buttons;

        //private System.ComponentModel.IContainer components = null;
        
        /*
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing){
            if(disposing && (components != null)){
                components.Dispose();
            }

            base.Dispose(disposing);
        }*/

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing){
            if(disposing && (Buttons != null)){
                foreach(var button in Buttons){
                    button.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitComponent1(){
            this.SuspendLayout();
            var x = STD_BordersX_Indent;
            var y = STD_BordersY_Indent;
            var i = 0;

            foreach(var button in Buttons){
                button.Initialize(x, y, i, this);
               
                x = x + BrowserButton.STD_SizeX;
                y = y + BrowserButton.STD_SizeY + STD_BFromB_Indent;
                i++;
            }

            var sizeX = 2 * STD_BordersX_Indent + BrowserButton.STD_SizeX;
            var sizeY = 2 * STD_BordersY_Indent + BrowserButton.STD_SizeY * i + 
                        STD_BFromB_Indent * (i - 1);

            //this.components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(sizeX, sizeY);
            this.Text = Name;
            this.ResumeLayout(false);
        }

        private void InitComponent2(){

        }
    }
}