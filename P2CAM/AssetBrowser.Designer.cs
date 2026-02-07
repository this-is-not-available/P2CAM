namespace P2CAM
{
    partial class AssetBrowser
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetBrowser));
            CustomAssetLayout = new FlowLayoutPanel();
            toolStrip1 = new ToolStrip();
            BtnAssets = new ToolStripDropDownButton();
            BtnAssetInstallItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            BtnCreateAsset = new ToolStripMenuItem();
            BtnOptions = new ToolStripButton();
            SelectedAssetBox = new GroupBox();
            SelectedAssetVersion = new Label();
            SelectedAssetCredit = new Label();
            SelectedAssetAuthor = new Label();
            BtnUninstallAsset = new Button();
            SelectedAssetDescription = new Label();
            SelectedAssetImage = new PictureBox();
            SelectedAssetName = new Label();
            customFolderWatcher = new FileSystemWatcher();
            statusStrip = new StatusStrip();
            statusStripProgressBar = new ToolStripProgressBar();
            progressBarStatusLabel = new ToolStripStatusLabel();
            toolStrip1.SuspendLayout();
            SelectedAssetBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SelectedAssetImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customFolderWatcher).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // CustomAssetLayout
            // 
            CustomAssetLayout.AutoScroll = true;
            CustomAssetLayout.Location = new Point(0, 445);
            CustomAssetLayout.Margin = new Padding(0);
            CustomAssetLayout.Name = "CustomAssetLayout";
            CustomAssetLayout.Size = new Size(980, 240);
            CustomAssetLayout.TabIndex = 0;
            CustomAssetLayout.WrapContents = false;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { BtnAssets, BtnOptions });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(978, 34);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // BtnAssets
            // 
            BtnAssets.AutoToolTip = false;
            BtnAssets.DisplayStyle = ToolStripItemDisplayStyle.Text;
            BtnAssets.DropDownItems.AddRange(new ToolStripItem[] { BtnAssetInstallItem, refreshToolStripMenuItem, BtnCreateAsset });
            BtnAssets.Image = (Image)resources.GetObject("BtnAssets.Image");
            BtnAssets.ImageTransparentColor = Color.Magenta;
            BtnAssets.Name = "BtnAssets";
            BtnAssets.Size = new Size(81, 29);
            BtnAssets.Text = "Assets";
            // 
            // BtnAssetInstallItem
            // 
            BtnAssetInstallItem.Name = "BtnAssetInstallItem";
            BtnAssetInstallItem.Size = new Size(203, 34);
            BtnAssetInstallItem.Text = "Install...";
            BtnAssetInstallItem.Click += BtnAssetInstallItem_Click;
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
            refreshToolStripMenuItem.Size = new Size(203, 34);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += BtnAssetRefreshItem_Click;
            // 
            // BtnCreateAsset
            // 
            BtnCreateAsset.Name = "BtnCreateAsset";
            BtnCreateAsset.Size = new Size(203, 34);
            BtnCreateAsset.Text = "Create";
            BtnCreateAsset.Click += BtnCreateAsset_Click;
            // 
            // BtnOptions
            // 
            BtnOptions.AutoToolTip = false;
            BtnOptions.DisplayStyle = ToolStripItemDisplayStyle.Text;
            BtnOptions.Image = (Image)resources.GetObject("BtnOptions.Image");
            BtnOptions.ImageTransparentColor = Color.Magenta;
            BtnOptions.Name = "BtnOptions";
            BtnOptions.Size = new Size(80, 29);
            BtnOptions.Text = "Options";
            BtnOptions.Click += BtnOptions_Click;
            // 
            // SelectedAssetBox
            // 
            SelectedAssetBox.Controls.Add(SelectedAssetVersion);
            SelectedAssetBox.Controls.Add(SelectedAssetCredit);
            SelectedAssetBox.Controls.Add(SelectedAssetAuthor);
            SelectedAssetBox.Controls.Add(BtnUninstallAsset);
            SelectedAssetBox.Controls.Add(SelectedAssetDescription);
            SelectedAssetBox.Controls.Add(SelectedAssetImage);
            SelectedAssetBox.Controls.Add(SelectedAssetName);
            SelectedAssetBox.Location = new Point(0, 37);
            SelectedAssetBox.Name = "SelectedAssetBox";
            SelectedAssetBox.Size = new Size(978, 405);
            SelectedAssetBox.TabIndex = 4;
            SelectedAssetBox.TabStop = false;
            SelectedAssetBox.Text = "Example Asset";
            // 
            // SelectedAssetVersion
            // 
            SelectedAssetVersion.AutoSize = true;
            SelectedAssetVersion.Location = new Point(640, 323);
            SelectedAssetVersion.Name = "SelectedAssetVersion";
            SelectedAssetVersion.Size = new Size(117, 25);
            SelectedAssetVersion.TabIndex = 6;
            SelectedAssetVersion.Text = "Version: 1.0.0";
            // 
            // SelectedAssetCredit
            // 
            SelectedAssetCredit.AutoSize = true;
            SelectedAssetCredit.Location = new Point(640, 298);
            SelectedAssetCredit.Name = "SelectedAssetCredit";
            SelectedAssetCredit.Size = new Size(169, 25);
            SelectedAssetCredit.TabIndex = 5;
            SelectedAssetCredit.Text = "Credit: Not required";
            // 
            // SelectedAssetAuthor
            // 
            SelectedAssetAuthor.AutoSize = true;
            SelectedAssetAuthor.Location = new Point(640, 273);
            SelectedAssetAuthor.Name = "SelectedAssetAuthor";
            SelectedAssetAuthor.Size = new Size(76, 25);
            SelectedAssetAuthor.TabIndex = 4;
            SelectedAssetAuthor.Text = "Author: ";
            // 
            // BtnUninstallAsset
            // 
            BtnUninstallAsset.Enabled = false;
            BtnUninstallAsset.Location = new Point(640, 363);
            BtnUninstallAsset.Name = "BtnUninstallAsset";
            BtnUninstallAsset.Size = new Size(332, 36);
            BtnUninstallAsset.TabIndex = 3;
            BtnUninstallAsset.Text = "Uninstall";
            BtnUninstallAsset.UseVisualStyleBackColor = true;
            BtnUninstallAsset.Click += BtnUninstall_Click;
            // 
            // SelectedAssetDescription
            // 
            SelectedAssetDescription.AutoEllipsis = true;
            SelectedAssetDescription.Location = new Point(640, 68);
            SelectedAssetDescription.Name = "SelectedAssetDescription";
            SelectedAssetDescription.Size = new Size(332, 205);
            SelectedAssetDescription.TabIndex = 2;
            // 
            // SelectedAssetImage
            // 
            SelectedAssetImage.Location = new Point(6, 30);
            SelectedAssetImage.Name = "SelectedAssetImage";
            SelectedAssetImage.Size = new Size(628, 369);
            SelectedAssetImage.TabIndex = 0;
            SelectedAssetImage.TabStop = false;
            // 
            // SelectedAssetName
            // 
            SelectedAssetName.AutoSize = true;
            SelectedAssetName.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SelectedAssetName.Location = new Point(640, 30);
            SelectedAssetName.Name = "SelectedAssetName";
            SelectedAssetName.Size = new Size(206, 38);
            SelectedAssetName.TabIndex = 1;
            SelectedAssetName.Text = "Example Asset";
            // 
            // customFolderWatcher
            // 
            customFolderWatcher.EnableRaisingEvents = true;
            customFolderWatcher.SynchronizingObject = this;
            // 
            // statusStrip
            // 
            statusStrip.Enabled = false;
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusStripProgressBar, progressBarStatusLabel });
            statusStrip.Location = new Point(0, 662);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(978, 32);
            statusStrip.TabIndex = 5;
            // 
            // statusStripProgressBar
            // 
            statusStripProgressBar.Name = "statusStripProgressBar";
            statusStripProgressBar.Size = new Size(200, 24);
            statusStripProgressBar.Value = 33;
            // 
            // progressBarStatusLabel
            // 
            progressBarStatusLabel.Name = "progressBarStatusLabel";
            progressBarStatusLabel.Size = new Size(102, 25);
            progressBarStatusLabel.Text = "Doing stuff";
            // 
            // AssetBrowser
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 694);
            Controls.Add(statusStrip);
            Controls.Add(SelectedAssetBox);
            Controls.Add(toolStrip1);
            Controls.Add(CustomAssetLayout);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AssetBrowser";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterParent;
            Text = "P2CAM";
            FormClosing += AssetBrowser_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            SelectedAssetBox.ResumeLayout(false);
            SelectedAssetBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SelectedAssetImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)customFolderWatcher).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel CustomAssetLayout;
        private ToolStrip toolStrip1;
        private ToolStripButton BtnOptions;
        private GroupBox SelectedAssetBox;
        private PictureBox SelectedAssetImage;
        private Label SelectedAssetName;
        private ToolStripDropDownButton BtnAssets;
        private ToolStripMenuItem BtnAssetInstallItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private FileSystemWatcher customFolderWatcher;
        private Label SelectedAssetDescription;
        private StatusStrip statusStrip;
        private ToolStripProgressBar statusStripProgressBar;
        private ToolStripStatusLabel progressBarStatusLabel;
        private Button BtnUninstallAsset;
        private ToolStripMenuItem BtnCreateAsset;
        private Label SelectedAssetVersion;
        private Label SelectedAssetCredit;
        private Label SelectedAssetAuthor;
    }
}
