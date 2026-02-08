using System;
using System.Data;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Tomlyn;

namespace P2CAM
{
    public partial class AssetCreation : Form
    {
        string? selectedFolder;
        string? selectedImage;

        public AssetCreation()
        {
            InitializeComponent();
        }

        public void Init()
        {
            CreditDropdown.SelectedIndex = 0;
            FolderImage.Image = SystemIcons.Error.ToBitmap();
        }

        private void BtnCreateAsset_Click(object sender, EventArgs e)
        {
            string[] tags = AssetTagsBox.Text.Split(',');
            int i = 0;
            foreach (string tag in tags)
            {
                tag.Trim();
                tags[i] = tag;
                i++;
            }

            AssetDefinition assetInfo = new AssetDefinition();
            assetInfo.Name = AssetNameBox.Text;
            assetInfo.Description = DescriptionBox.Text;
            assetInfo.Version = VersionBox.Text;
            assetInfo.Source = AssetSourceBox.Text;
            assetInfo.Author = AuthorName.Text;
            assetInfo.Tags = tags;
            assetInfo.Credit = (CreditType)CreditDropdown.SelectedIndex;

            if (selectedImage != null & string.IsNullOrEmpty(selectedImage) & File.Exists(selectedImage))
            {
                MessageBox.Show("The asset must have a thumbnail image selected!", "Invalid asset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedFolder != null & string.IsNullOrEmpty(selectedFolder) & Path.Exists(selectedFolder))
            {
                MessageBox.Show("The asset must have a content root selected!", "Invalid asset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                AssetManager.ValidateAssetDefinition(assetInfo);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Invalid asset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ask the user where to save it

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckWriteAccess = true;
                sfd.DefaultExt = "p2asset";
                sfd.Filter = "Asset files|*.p2asset|All files|*.*";
                sfd.OverwritePrompt = true;
                sfd.ShowPinnedPlaces = true;
                DialogResult result = sfd.ShowDialog();

                // OK button was pressed.
                if (result == DialogResult.OK)
                {
                    string savePath = sfd.FileName;

                    if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                    {
                        MessageBox.Show("This folder for this path doesn't exist!");
                        return;
                    }

                    FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                    AssetManager.CreateAsset(assetInfo, selectedFolder!, selectedImage!, fileStream);
                    fileStream.Close();
                    this.Close();
                }

                // Cancel button was pressed.
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void DisplayFolderIcon(string selectedFolder)
        {
            FolderImage.Image = SystemIcons.WinLogo.ToBitmap();
            try
            {
                // Try to use the standard folder icon
                Icon folderIcon = SystemIcons.WinLogo;
                string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                // Use SHGetFileInfo to get the folder icon
                var shfi = new NativeMethods.SHFILEINFO();
                IntPtr hImg = NativeMethods.SHGetFileInfo(
                    selectedFolder,
                    0,
                    ref shfi,
                    (uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
                    NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_SMALLICON
                );

                if (shfi.hIcon != IntPtr.Zero)
                {
                    folderIcon = Icon.FromHandle(shfi.hIcon);
                    FolderImage.Image = folderIcon.ToBitmap();
                    NativeMethods.DestroyIcon(shfi.hIcon);
                }
            }
            catch
            {
                // fallback to default icon if any error
                FolderImage.Image = SystemIcons.WinLogo.ToBitmap();
            }
        }

        private void ShowFolderPreview()
        {
            if (selectedFolder == null)
                return;

            // Get the name of the directory from the path
            var folderName = System.IO.Path.GetFileName(selectedFolder.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar));
            DisplayFolderIcon(selectedFolder);
            FolderName.Text = folderName;
            BtnOpenFolder.Enabled = true;

            Task.Run(() =>
            {
                // Prepare data in the background
                var entries = System.IO.Directory.GetFileSystemEntries(selectedFolder);
                var filePanels = new List<FlowLayoutPanel>();

                foreach (var entry in entries)
                {
                    Icon? fileIcon = null;
                    var shfi = new NativeMethods.SHFILEINFO();
                    IntPtr hImg = NativeMethods.SHGetFileInfo(
                        entry,
                        0,
                        ref shfi,
                        (uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
                        NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_SMALLICON
                    );
                    if (shfi.hIcon != IntPtr.Zero)
                    {
                        fileIcon = Icon.FromHandle(shfi.hIcon);
                    }

                    var filePanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        Width = FolderPreviewFlowLayoutPanel.Width - 32,
                        Height = 28,
                        Margin = new Padding(0, 0, 0, 2)
                    };

                    var iconBox = new PictureBox
                    {
                        Width = 20,
                        Height = 20,
                        SizeMode = PictureBoxSizeMode.CenterImage,
                        Margin = new Padding(2, 2, 2, 2),
                        Image = fileIcon != null ? fileIcon.ToBitmap() : SystemIcons.WinLogo.ToBitmap()
                    };
                    if (shfi.hIcon != IntPtr.Zero)
                        NativeMethods.DestroyIcon(shfi.hIcon);

                    var nameLabel = new Label
                    {
                        Text = System.IO.Path.GetFileName(entry),
                        Width = filePanel.Width - iconBox.Width - 16,
                        Height = 28,
                        AutoEllipsis = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(2, 2, 2, 2)
                    };

                    filePanel.Controls.Add(iconBox);
                    filePanel.Controls.Add(nameLabel);

                    filePanels.Add(filePanel);
                }

                // Marshal UI updates to the UI thread
                Invoke(() =>
                {
                    FolderPreviewFlowLayoutPanel.Controls.Clear();
                    foreach (var panel in filePanels)
                        FolderPreviewFlowLayoutPanel.Controls.Add(panel);
                });
            });
        }

        private void BtnSelectAssetFiles_Click(object sender, EventArgs e)
        {
            if (Program.options.Portal2_Dir == null)
            {
                return;
            }

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.InitialDirectory = Program.options.Portal2_Dir;
                DialogResult result = fbd.ShowDialog();

                // OK button was pressed.
                if (result == DialogResult.OK)
                {
                    selectedFolder = fbd.SelectedPath;

                    string[] directories;
                    try
                    {
                        directories = Directory.GetDirectories(selectedFolder);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unexpected exception!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var assetDirectories = new Dictionary<string, string>
                    {
                        { "materials", "Materials" },
                        { "models", "Models" },
                        { "particles", "Particles" },
                        { "scripts", "Scripts" },
                        { "sound", "Sound" },
                        { "instances", "Instances" },
                        { "prefabs", "Prefabs" }
                    };

                    // Find detected asset types
                    var detectedTypes = new List<string>();
                    foreach (var kvp in assetDirectories)
                    {
                        string subDir = Path.Combine(selectedFolder, kvp.Key);
                        if (Directory.Exists(subDir))
                        {
                            detectedTypes.Add(kvp.Value);
                        }
                    }

                    if (detectedTypes.Count == 0)
                    {
                        selectedFolder = null;
                        MessageBox.Show("Folder not detected as a valid root directory!", "Unrecognized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Display detected asset types in DetectedAssetsPanel
                    DetectedAssetsPanel.Controls.Clear();
                    foreach (var type in detectedTypes)
                    {
                        var label = new Label
                        {
                            Text = "•" + type,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Padding = new Padding(4, 2, 4, 2),
                            Margin = new Padding(2)
                        };
                        DetectedAssetsPanel.Controls.Add(label);
                    }

                    ShowFolderPreview();
                }
                // Cancel button was pressed.
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void VersionBox_TextChanged(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null)
                return;

            char[] allowedCharacters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
            string filtered = new string(textBox.Text.Where(c => allowedCharacters.Contains(c)).ToArray());
            if (textBox.Text != filtered)
            {
                int selectionStart = textBox.SelectionStart;
                textBox.Text = filtered;
                textBox.SelectionStart = Math.Min(selectionStart, filtered.Length);
            }
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            AssetCreationHelp window = new AssetCreationHelp();
            window.ShowDialog();
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            if (selectedFolder == null) return;
            Process.Start("explorer.exe", selectedFolder);
        }

        private void BtnAssetImageNew_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                ofd.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.tiff)|*.png;*.jpg;*.jpeg;*.bmp;*.tiff|All files|*.*";
                ofd.ShowPinnedPlaces = true;
                ofd.Multiselect = false;
                DialogResult result = ofd.ShowDialog();

                // OK button was pressed.
                if (result == DialogResult.OK)
                {
                    selectedImage = ofd.FileName;
                    if (File.Exists(selectedImage))
                    {
                        try
                        {
                            AssetImage.Image = ImageUtils.ResizeImage(Image.FromFile(selectedImage), AssetImage.Width, AssetImage.Height);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to load image: {ex.Message}");
                            AssetImage.Image = null;
                        }
                    }
                }

                // Cancel button was pressed.
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }
    }

    // Source - https://stackoverflow.com/a/37806517
    // Posted by Cody Gray, modified by community. See post 'Timeline' for change history
    // Retrieved 2026-01-31, License - CC BY-SA 3.0

    internal static class NativeMethods
    {
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETIMAGELIST = (LVM_FIRST + 2);
        public const uint LVM_SETIMAGELIST = (LVM_FIRST + 3);

        public const uint LVSIL_NORMAL = 0;
        public const uint LVSIL_SMALL = 1;
        public const uint LVSIL_STATE = 2;
        public const uint LVSIL_GROUPHEADER = 3;

        [DllImport("user32")]
        public static extern IntPtr SendMessage(IntPtr hWnd,
                                                uint msg,
                                                uint wParam,
                                                IntPtr lParam);

        [DllImport("comctl32")]
        public static extern bool ImageList_Destroy(IntPtr hImageList);

        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint SHGFI_SYSICONINDEX = 0x4000;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 /* MAX_PATH */)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                                                  uint dwFileAttributes,
                                                  ref SHFILEINFO psfi,
                                                  uint cbSizeFileInfo,
                                                  uint uFlags);

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd,
                                                string pszSubAppName,
                                                string pszSubIdList);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
    }
}
