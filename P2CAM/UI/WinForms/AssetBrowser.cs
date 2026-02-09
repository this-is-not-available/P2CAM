using System.Windows.Forms;
using System;
using Image = System.Drawing.Image;
using P2CAM.Core;

namespace P2CAM.UI.WinForms
{
    public partial class AssetBrowser : Form
    {
        private AssetManager assetManager = new AssetManager();
        private List<GroupBox> assetList = new List<GroupBox>();
        private static string? SelectedAssetId { get; set; } = null;

        public AssetBrowser()
        {
            InitializeComponent();
            assetList = new List<GroupBox>();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            string customFolder = Path.Combine(Program.options.Portal2_Dir!, "portal2", "custom");
            if (Directory.Exists(customFolder))
            {
                InitCustomFolderWatcher(customFolder);
            }
        }

        public void Init()
        {
            HideProgressBar();

            if (string.IsNullOrWhiteSpace(Program.options.Portal2_Dir))
            {
                Program.options.Portal2_Dir = SteamUtils.FindPortal2Directory();
                if (Program.options.Portal2_Dir == null)
                {
                    Program.options.Portal2_Dir = AskForPortal2Folder();
                }
                if (Program.options.Portal2_Dir == null)
                {
                    this.Close();
                    return; // User pressed 'Cancel'
                }
            }

            LoadAssets();
        }
        public void LoadAssets()
        {
            assetManager.LoadAssetsInInstallation();
            RefreshAssetUI();
        }

        public void SelectAsset(Asset asset)
        {
            string imagePath = Path.Combine(asset.FilePath, asset.RelativeImagePath);

            if (SelectedAssetImage.Image != null)
            {
                SelectedAssetImage.Image.Dispose();
            }

            if (File.Exists(imagePath))
            {
                try
                {
                    SelectedAssetImage.Image = ImageUtils.ResizeImage(Image.FromFile(imagePath), SelectedAssetImage.Width, SelectedAssetImage.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex.Message}");
                    SelectedAssetImage.Image = null;
                }
            }
            else
            {
                MessageBox.Show($"Image not found: {imagePath}");
                SelectedAssetImage.Image = null; // Set to null to avoid further issues
            }

            SelectedAssetBox.Text = asset.Name;
            SelectedAssetName.Text = asset.Name;
            SelectedAssetAuthor.Enabled = true;
            SelectedAssetAuthor.Text = "Author: " + asset.Author;
            SelectedAssetVersion.Enabled = true;
            SelectedAssetVersion.Text = "Version: " + asset.Version;
            SelectedAssetCredit.Enabled = true;
            SelectedAssetCredit.Text = ("Credit: " + asset.Credit).Replace("NotRequired", "Not Required");
            SelectedAssetDescription.Enabled = true;
            SelectedAssetDescription.Text = asset.Description;
            BtnUninstallAsset.Enabled = true;
            SelectedAssetId = asset.Id;

            if (string.IsNullOrEmpty(asset.Description))
            {
                SelectedAssetDescription.Enabled = false;
                SelectedAssetDescription.Text = "(no description)";
            }
        }

        public void UnselectAsset()
        {
            if (SelectedAssetImage.Image != null)
            {
                SelectedAssetImage.Image.Dispose();
                SelectedAssetImage.Image = null;
            }

            SelectedAssetBox.Text = "none";
            SelectedAssetName.Text = "none";
            SelectedAssetAuthor.Enabled = false;
            SelectedAssetAuthor.Text = "no one";
            SelectedAssetVersion.Enabled = false;
            SelectedAssetVersion.Text = "?.?.?";
            SelectedAssetCredit.Enabled = false;
            SelectedAssetCredit.Text = "Unknown";
            SelectedAssetDescription.Enabled = false;
            SelectedAssetDescription.Text = "(no asset selected)";
            BtnUninstallAsset.Enabled = false;
        }

        public void CreateAssetInList(Asset asset)
        {
            string imagePath = Path.Combine(asset.FilePath, asset.RelativeImagePath);

            GroupBox groupBox = new GroupBox
            {
                Size = new System.Drawing.Size(300, 205)
            };

            Label AssetName = new Label
            {
                Text = asset.Name,
                Size = new System.Drawing.Size(250, 32),
                Location = new System.Drawing.Point(25, 15)
            };

            Image? image = null;
            if (File.Exists(imagePath))
            {
                image = ImageUtils.ResizeImage(Image.FromFile(imagePath), 270, 140);
            }
            else
            {
                MessageBox.Show($"Image not found: {imagePath}");
            }

            PictureBox AssetImage = new PictureBox
            {
                Size = new System.Drawing.Size(270, 140),
                Location = new System.Drawing.Point(20, 64),
                Image = image
            };

            groupBox.Controls.Add(AssetName);
            groupBox.Controls.Add(AssetImage);

            void OnClick(object? sender, EventArgs e)
            {
                SelectAsset(asset);
            }

            groupBox.Click += OnClick;
            AssetName.Click += OnClick;
            AssetImage.Click += OnClick;

            // Add button to the FlowLayoutPanel and the list
            CustomAssetLayout.Controls.Add(groupBox);
            assetList.Add(groupBox);
        }

        public void SetProgressBar(int progress, string currentTask)
        {
            statusStrip.Visible = (progress is >= 100 or <= 0) ? true : false;
            statusStripProgressBar.Value = Math.Clamp(progress, 0, 100);
            progressBarStatusLabel.Text = currentTask;
        }

        public void HideProgressBar()
        {
            statusStrip.Visible = false;
        }

        private void InitCustomFolderWatcher(string customFolder)
        {
            customFolderWatcher?.Dispose(); // Dispose previous watcher if any

            customFolderWatcher = new FileSystemWatcher(customFolder)
            {
                NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            customFolderWatcher.Changed += OnCustomFolderChanged;
            customFolderWatcher.Created += OnCustomFolderChanged;
            customFolderWatcher.Deleted += OnCustomFolderChanged;
            customFolderWatcher.Renamed += OnCustomFolderChanged;
        }

        private string? AskForPortal2Folder()
        {
            while (true)
            {
                // Ask the user where the Portal 2 folder is
                using (FolderBrowserDialog fbd = new())
                {
                    fbd.Description = "Select the Portal 2 folder";
                    var result = fbd.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        if (!SteamUtils.ValidatePortal2Directory(fbd.SelectedPath))
                        {
                            // No game executable and no gameinfo.txt
                            MessageBox.Show("Folder is not a valid Portal 2 installation");
                        }
                        else
                        {
                            return fbd.SelectedPath;
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // Quit asking and close the app
                        this.Close();
                        return null;
                    }
                }
            }
        }

        private void RefreshAssetUI()
        {
            // TODO: is this logic correct?
            int progress = 0;

            assetList.ForEach(asset =>
            {
                asset.Dispose();
                progress += 50 / Math.Max(assetManager.Assets.Count, 1);
                SetProgressBar(progress, "Cleaning out old containers");
            });
            assetManager.Assets.ForEach(asset =>
            {
                CreateAssetInList(asset);
                progress += 50 / Math.Max(assetManager.Assets.Count, 1);
                SetProgressBar(progress, "Creating new containers");
            });

            HideProgressBar();


            if (assetManager.Assets.ToArray().Length > 0)
            {
                SelectAsset(assetManager.Assets[0]);

                string customFolder = Path.Combine(Program.options.Portal2_Dir!, "portal2", "custom");
                // TODO: error handling
                MountHandler.AddCustomSearchPathsToGameInfo(Program.options.Portal2_Dir!, customFolder);
            }
            else
            {
                UnselectAsset();
            }
        }

        private void BtnAssetInstallItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Custom Asset Files (*.p2asset)|*.p2asset";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    bool success = assetManager.InstallAsset(ofd.FileName);
                    if (success)
                    {
                        MessageBox.Show("Asset installed!");
                        assetManager.LoadAssetsInInstallation();
                        RefreshAssetUI();
                    }
                    else
                    {
                        MessageBox.Show("Failed to install asset.");
                    }
                }
            }
        }

        private void BtnAssetRefreshItem_Click(object sender, EventArgs e)
        {
            LoadAssets();
        }

        private void OnCustomFolderChanged(object sender, FileSystemEventArgs e)
        {
            if (assetManager.InstallationInProgress)
                return;

            BeginInvoke((Action)(() =>
            {
                LoadAssets();
            }));
        }

        private void BtnUninstall_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to uninstall this asset?",
                                     "Confirm Uninstallation",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                assetManager.UninstallAsset(SelectedAssetId!);
                assetManager.LoadAssetsInInstallation();
                RefreshAssetUI();
            }
        }

        private void BtnCreateAsset_Click(object sender, EventArgs e)
        {
            AssetCreation window = new AssetCreation();
            window.Init();
            window.ShowDialog();
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm window = new OptionsForm(this);
            window.Init();
            window.ShowDialog();
        }

        private void AssetBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.options.Save();
        }
    }
}
