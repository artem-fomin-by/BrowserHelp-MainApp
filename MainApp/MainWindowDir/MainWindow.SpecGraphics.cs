namespace MainApp.MainWindowDir{
    partial class MainWindow{
        #region Spec_Components_Properties

        private const int SpecMessageBoxSizeX1 = 0;
        private const int SpecMessageBoxSizeY1 = 0;
        private const int SpecMessageBoxSizeX2 = 0;
        private const int SpecMessageBoxSizeY2 = 0;

        private const int SpecErrorMessageBoxSizeX = 0;
        private const int SpecErrorMessageBoxSizeY = 0;

        private const int SpecExitButtonSizeX = 0;
        private const int SpecExitButtonSizeY = 0;

        #endregion

        private void InitComponent2(){
            SpecMessageBox = new TextBox();
            SpecExitButton = new Button();

            SuspendLayout();

            SpecMessageBox.Location = new Point(STD_BordersX_Indent, STD_BordersY_Indent);
            SpecMessageBox.Name = nameof(SpecMessageBox);
            SpecMessageBox.ReadOnly = true;
            SpecMessageBox.Size = new Size(SpecMessageBoxSizeX1, SpecMessageBoxSizeY1);
            SpecMessageBox.TabIndex = 0;
            SpecMessageBox.Text = STD_NoBrowsersMessage;

            InitWindow(0, 0);

            ResumeLayout(false);
        }

        private void InitComponent3(Exception exception){
            SpecMessageBox = new TextBox();
            SpecExitButton = new Button();

            SuspendLayout();

            SpecMessageBox.Location = new Point(STD_BordersX_Indent, STD_BordersY_Indent);
            SpecMessageBox.Name = nameof(SpecMessageBox);
            SpecMessageBox.ReadOnly = true;
            SpecMessageBox.Size = new Size(SpecMessageBoxSizeX2, SpecMessageBoxSizeY2);
            SpecMessageBox.TabIndex = 0;
            SpecMessageBox.Text = STD_ErrorMessage;

            InitWindow(0, 0);

            ResumeLayout(false);
        }

        private TextBox SpecMessageBox;
        private TextBox SpecErrorMessageBox;
        private Button SpecExitButton;
    }
}
