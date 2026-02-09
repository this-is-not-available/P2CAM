namespace P2CAM.UI.WinForms
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnOptionsSave = new Button();
            label1 = new Label();
            P2InstallDirectory = new TextBox();
            Btn_LocateP2InstallDir = new Button();
            label2 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnOptionsSave
            // 
            BtnOptionsSave.Location = new Point(695, 386);
            BtnOptionsSave.Name = "BtnOptionsSave";
            BtnOptionsSave.Size = new Size(75, 34);
            BtnOptionsSave.TabIndex = 0;
            BtnOptionsSave.Text = "Save";
            BtnOptionsSave.UseVisualStyleBackColor = true;
            BtnOptionsSave.Click += BtnOptionsSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 27);
            label1.Name = "label1";
            label1.Size = new Size(200, 25);
            label1.TabIndex = 1;
            label1.Text = "Portal 2 Install Directory";
            // 
            // P2InstallDirectory
            // 
            P2InstallDirectory.Location = new Point(212, 24);
            P2InstallDirectory.Name = "P2InstallDirectory";
            P2InstallDirectory.Size = new Size(467, 31);
            P2InstallDirectory.TabIndex = 2;
            // 
            // Btn_LocateP2InstallDir
            // 
            Btn_LocateP2InstallDir.Location = new Point(685, 22);
            Btn_LocateP2InstallDir.Name = "Btn_LocateP2InstallDir";
            Btn_LocateP2InstallDir.Size = new Size(85, 34);
            Btn_LocateP2InstallDir.TabIndex = 4;
            Btn_LocateP2InstallDir.Text = "Locate";
            Btn_LocateP2InstallDir.UseVisualStyleBackColor = true;
            Btn_LocateP2InstallDir.Click += Btn_LocateP2InstallDir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(307, 211);
            label2.Name = "label2";
            label2.Size = new Size(164, 25);
            label2.TabIndex = 3;
            label2.Text = "That's it for now lol";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(BtnOptionsSave);
            groupBox1.Controls.Add(Btn_LocateP2InstallDir);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(P2InstallDirectory);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 426);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options";
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "OptionsForm";
            Text = "Options";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BtnOptionsSave;
        private Label label1;
        private TextBox P2InstallDirectory;
        private Button Btn_LocateP2InstallDir;
        private Label label2;
        private GroupBox groupBox1;
    }
}