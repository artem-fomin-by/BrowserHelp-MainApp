﻿namespace MainApp.AppWindows{
    public partial class ErrorWindow : Form{
        public ErrorWindow(string name, Exception exception){
            InitializeComponent();

            richTextBox2.Text = exception.Message;

            button1.Click += (sender, args) => {
                Application.Exit();
            };

            Name = name;
            Text = name;
        }
    }
}