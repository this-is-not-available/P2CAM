namespace P2CAM.UI.WinForms
{
    partial class AssetCreation
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
            AssetNameBox = new TextBox();
            DescriptionBox = new TextBox();
            groupBox1 = new GroupBox();
            FolderPreviewFlowLayoutPanel = new FlowLayoutPanel();
            BtnOpenFolder = new Button();
            FolderName = new Label();
            FolderImage = new PictureBox();
            BtnSelectAssetFiles = new Button();
            groupBox2 = new GroupBox();
            label6 = new Label();
            AssetTagsBox = new TextBox();
            DetectedAssetsPanel = new FlowLayoutPanel();
            label9 = new Label();
            label8 = new Label();
            AssetSourceBox = new TextBox();
            label4 = new Label();
            CreditDropdown = new ComboBox();
            label5 = new Label();
            AuthorName = new TextBox();
            label3 = new Label();
            VersionBox = new TextBox();
            label7 = new Label();
            BtnAssetImageNew = new Button();
            AssetImage = new PictureBox();
            groupBox3 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            BtnHelp = new Button();
            BtnCreateAsset = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FolderImage).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AssetImage).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // AssetNameBox
            // 
            AssetNameBox.Location = new Point(6, 55);
            AssetNameBox.Name = "AssetNameBox";
            AssetNameBox.PlaceholderText = "Awesome Asset";
            AssetNameBox.Size = new Size(461, 31);
            AssetNameBox.TabIndex = 0;
            // 
            // DescriptionBox
            // 
            DescriptionBox.Location = new Point(6, 117);
            DescriptionBox.Multiline = true;
            DescriptionBox.Name = "DescriptionBox";
            DescriptionBox.PlaceholderText = "Asset Description";
            DescriptionBox.Size = new Size(461, 343);
            DescriptionBox.TabIndex = 1;
            DescriptionBox.Text = "Asset Description";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FolderPreviewFlowLayoutPanel);
            groupBox1.Controls.Add(BtnOpenFolder);
            groupBox1.Controls.Add(FolderName);
            groupBox1.Controls.Add(FolderImage);
            groupBox1.Controls.Add(BtnSelectAssetFiles);
            groupBox1.Location = new Point(6, 466);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(461, 335);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Asset files";
            // 
            // FolderPreviewFlowLayoutPanel
            // 
            FolderPreviewFlowLayoutPanel.AutoScroll = true;
            FolderPreviewFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            FolderPreviewFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            FolderPreviewFlowLayoutPanel.Location = new Point(218, 26);
            FolderPreviewFlowLayoutPanel.Name = "FolderPreviewFlowLayoutPanel";
            FolderPreviewFlowLayoutPanel.Size = new Size(237, 303);
            FolderPreviewFlowLayoutPanel.TabIndex = 8;
            FolderPreviewFlowLayoutPanel.WrapContents = false;
            // 
            // BtnOpenFolder
            // 
            BtnOpenFolder.Enabled = false;
            BtnOpenFolder.Location = new Point(5, 112);
            BtnOpenFolder.Name = "BtnOpenFolder";
            BtnOpenFolder.Size = new Size(206, 34);
            BtnOpenFolder.TabIndex = 6;
            BtnOpenFolder.Text = "Open folder";
            BtnOpenFolder.UseVisualStyleBackColor = true;
            BtnOpenFolder.Click += BtnOpenFolder_Click;
            // 
            // FolderName
            // 
            FolderName.AutoSize = true;
            FolderName.Location = new Point(54, 37);
            FolderName.Name = "FolderName";
            FolderName.Size = new Size(111, 25);
            FolderName.TabIndex = 4;
            FolderName.Text = "Not selected";
            // 
            // FolderImage
            // 
            FolderImage.InitialImage = null;
            FolderImage.Location = new Point(14, 32);
            FolderImage.Name = "FolderImage";
            FolderImage.Size = new Size(34, 34);
            FolderImage.SizeMode = PictureBoxSizeMode.StretchImage;
            FolderImage.TabIndex = 5;
            FolderImage.TabStop = false;
            // 
            // BtnSelectAssetFiles
            // 
            BtnSelectAssetFiles.Location = new Point(6, 72);
            BtnSelectAssetFiles.Name = "BtnSelectAssetFiles";
            BtnSelectAssetFiles.Size = new Size(206, 34);
            BtnSelectAssetFiles.TabIndex = 4;
            BtnSelectAssetFiles.Text = "Select folder...";
            BtnSelectAssetFiles.UseVisualStyleBackColor = true;
            BtnSelectAssetFiles.Click += BtnSelectAssetFiles_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(AssetTagsBox);
            groupBox2.Controls.Add(DetectedAssetsPanel);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(AssetSourceBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(CreditDropdown);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(AuthorName);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(VersionBox);
            groupBox2.Location = new Point(491, 16);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(787, 281);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Asset properties";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(386, 27);
            label6.Name = "label6";
            label6.Size = new Size(136, 25);
            label6.TabIndex = 10;
            label6.Text = "Detected assets";
            // 
            // AssetTagsBox
            // 
            AssetTagsBox.Location = new Point(6, 243);
            AssetTagsBox.Name = "AssetTagsBox";
            AssetTagsBox.PlaceholderText = "overgrown,cube dropper,cube..";
            AssetTagsBox.Size = new Size(374, 31);
            AssetTagsBox.TabIndex = 16;
            AssetTagsBox.WordWrap = false;
            // 
            // DetectedAssetsPanel
            // 
            DetectedAssetsPanel.AutoScroll = true;
            DetectedAssetsPanel.BorderStyle = BorderStyle.FixedSingle;
            DetectedAssetsPanel.FlowDirection = FlowDirection.TopDown;
            DetectedAssetsPanel.Location = new Point(386, 55);
            DetectedAssetsPanel.Name = "DetectedAssetsPanel";
            DetectedAssetsPanel.Size = new Size(395, 219);
            DetectedAssetsPanel.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(7, 215);
            label9.Name = "label9";
            label9.Size = new Size(47, 25);
            label9.TabIndex = 15;
            label9.Text = "Tags";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(99, 27);
            label8.Name = "label8";
            label8.Size = new Size(147, 25);
            label8.TabIndex = 13;
            label8.Text = "Source (optional)";
            // 
            // AssetSourceBox
            // 
            AssetSourceBox.Location = new Point(99, 55);
            AssetSourceBox.MaxLength = 26;
            AssetSourceBox.Name = "AssetSourceBox";
            AssetSourceBox.PlaceholderText = "e.g. Where you will publish this";
            AssetSourceBox.Size = new Size(268, 31);
            AssetSourceBox.TabIndex = 14;
            AssetSourceBox.WordWrap = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 151);
            label4.Name = "label4";
            label4.Size = new Size(59, 25);
            label4.TabIndex = 5;
            label4.Text = "Credit";
            // 
            // CreditDropdown
            // 
            CreditDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            CreditDropdown.FormattingEnabled = true;
            CreditDropdown.Items.AddRange(new object[] { "Not required", "Optional", "Required" });
            CreditDropdown.Location = new Point(7, 179);
            CreditDropdown.Name = "CreditDropdown";
            CreditDropdown.Size = new Size(360, 33);
            CreditDropdown.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 89);
            label5.Name = "label5";
            label5.Size = new Size(85, 25);
            label5.TabIndex = 5;
            label5.Text = "Author(s)";
            // 
            // AuthorName
            // 
            AuthorName.Location = new Point(7, 117);
            AuthorName.MaxLength = 32;
            AuthorName.Name = "AuthorName";
            AuthorName.PlaceholderText = "Your username";
            AuthorName.Size = new Size(360, 31);
            AuthorName.TabIndex = 6;
            AuthorName.WordWrap = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 27);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 4;
            label3.Text = "Version";
            // 
            // VersionBox
            // 
            VersionBox.CharacterCasing = CharacterCasing.Lower;
            VersionBox.Location = new Point(6, 55);
            VersionBox.MaxLength = 8;
            VersionBox.Name = "VersionBox";
            VersionBox.PlaceholderText = "1.0.0";
            VersionBox.Size = new Size(76, 31);
            VersionBox.TabIndex = 4;
            VersionBox.WordWrap = false;
            VersionBox.TextChanged += VersionBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(495, 312);
            label7.Name = "label7";
            label7.Size = new Size(62, 25);
            label7.TabIndex = 12;
            label7.Text = "Image";
            // 
            // BtnAssetImageNew
            // 
            BtnAssetImageNew.Location = new Point(558, 307);
            BtnAssetImageNew.Name = "BtnAssetImageNew";
            BtnAssetImageNew.Size = new Size(89, 34);
            BtnAssetImageNew.TabIndex = 11;
            BtnAssetImageNew.Text = "New";
            BtnAssetImageNew.UseVisualStyleBackColor = true;
            BtnAssetImageNew.Click += BtnAssetImageNew_Click;
            // 
            // AssetImage
            // 
            AssetImage.BorderStyle = BorderStyle.FixedSingle;
            AssetImage.Location = new Point(490, 344);
            AssetImage.Name = "AssetImage";
            AssetImage.Size = new Size(781, 439);
            AssetImage.TabIndex = 10;
            AssetImage.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(AssetNameBox);
            groupBox3.Controls.Add(DescriptionBox);
            groupBox3.Controls.Add(groupBox1);
            groupBox3.Location = new Point(12, 16);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(473, 807);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Basic information";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 27);
            label2.Name = "label2";
            label2.Size = new Size(59, 25);
            label2.TabIndex = 3;
            label2.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 89);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 2;
            label1.Text = "Description";
            // 
            // BtnHelp
            // 
            BtnHelp.Location = new Point(1237, 789);
            BtnHelp.Name = "BtnHelp";
            BtnHelp.Size = new Size(34, 34);
            BtnHelp.TabIndex = 4;
            BtnHelp.Text = "?";
            BtnHelp.UseVisualStyleBackColor = true;
            BtnHelp.Click += BtnHelp_Click;
            // 
            // BtnCreateAsset
            // 
            BtnCreateAsset.Location = new Point(1025, 789);
            BtnCreateAsset.Name = "BtnCreateAsset";
            BtnCreateAsset.Size = new Size(206, 34);
            BtnCreateAsset.TabIndex = 9;
            BtnCreateAsset.Text = "Create!";
            BtnCreateAsset.UseVisualStyleBackColor = true;
            BtnCreateAsset.Click += BtnCreateAsset_Click;
            // 
            // AssetCreation
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1283, 835);
            Controls.Add(BtnCreateAsset);
            Controls.Add(BtnHelp);
            Controls.Add(label7);
            Controls.Add(groupBox3);
            Controls.Add(BtnAssetImageNew);
            Controls.Add(groupBox2);
            Controls.Add(AssetImage);
            MaximizeBox = false;
            Name = "AssetCreation";
            Text = "Asset Creation";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FolderImage).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AssetImage).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox AssetNameBox;
        private TextBox DescriptionBox;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button BtnSelectAssetFiles;
        private GroupBox groupBox3;
        private TextBox VersionBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button BtnHelp;
        private PictureBox FolderImage;
        private Label FolderName;
        private Button BtnOpenFolder;
        private FlowLayoutPanel FolderPreviewFlowLayoutPanel;
        private Label label4;
        private ComboBox CreditDropdown;
        private Label label5;
        private TextBox AuthorName;
        private Button BtnCreateAsset;
        private FlowLayoutPanel DetectedAssetsPanel;
        private Label label6;
        private PictureBox AssetImage;
        private Label label7;
        private Button BtnAssetImageNew;
        private Label label8;
        private TextBox AssetSourceBox;
        private TextBox AssetTagsBox;
        private Label label9;
    }
}