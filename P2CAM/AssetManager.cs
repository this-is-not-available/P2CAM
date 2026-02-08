using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using Tomlyn;

namespace P2CAM
{
    public enum CreditType
    {
        Required,
        Optional,
        NotRequired,
        Unknown
    }

    public class AssetDefinition
    {
        public AssetDefinition()
        {
            Name = "Default Asset Name";
            Description = string.Empty;
            Author = "Unknown";
            Version = "?.?.?";
            RelativeImagePath = "thumbnail.png";
            Credit = CreditType.Unknown;
            Tags = [];
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string? Source { get; set; }
        public string Author { get; set; }
        public string[] Tags { get; set; }
        public CreditType Credit { get; set; }

        public string Image
        {
            get { return RelativeImagePath; }   // get method
            set { RelativeImagePath = value; }  // set method
        }
        public string RelativeImagePath { get; set; }
    }
    public class Asset : AssetDefinition
    {
        public Asset()
        {
            FilePath = "C:/";
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string FilePath { get; set; }
    }

    public class AssetManager
    {
        public bool InstallationInProgress = false;
        public List<Asset> Assets { get; } = new();

        public void LoadAssetsInInstallation()
        {
            Assets.Clear();
            string? installDirectory = Program.options.Portal2_Dir;
            if (installDirectory == null)
            {
                Debug.WriteLine("No Portal 2 install found!");
                return;
            }

            // Ensure the custom folder exists
            string customFolder = Path.Combine(installDirectory, "portal2", "custom");
            if (!Directory.Exists(customFolder))
            {
                Debug.WriteLine("No custom folder");
                return;
            }

            // Find all asset directories containing def.toml
            var assetDirs = FindCustomAssetDirectories(customFolder);

            foreach (var dir in assetDirs)
            {
                // Find the definition file (def.toml)
                string defAcfPath = Path.Combine(dir, "def.toml");
                if (File.Exists(defAcfPath))
                {
                    string data = File.ReadAllText(defAcfPath);
                    Asset? asset = ConfigParser.LoadConfigFromString(data);
                    if (asset != null)
                    {
                        asset.FilePath = dir;
                        Assets.Add(asset);
                    }
                }
            }
        }

        public static List<string> FindCustomAssetDirectories(string portal2CustomPath)
        {
            var result = new List<string>();
            if (!Directory.Exists(portal2CustomPath))
            {
                Debug.WriteLine("No Portal 2 custom path found!");
                return result;
            }

            // Search all immediate subdirectories for def.toml
            foreach (var dir in Directory.GetDirectories(portal2CustomPath))
            {
                string defAcfPath = Path.Combine(dir, "def.toml");
                if (File.Exists(defAcfPath))
                {
                    result.Add(dir);
                }
            }
            return result;
        }

        private bool _InstallAsset(string assetFilePath)
        {
            string? Portal2Dir = Program.options.Portal2_Dir;
            if (Portal2Dir == null)
            {
                Debug.WriteLine("Portal 2 directory not found.");
                return false;
            }

            // Read asset metadata from the .p2asset file
            Asset? asset;
            try
            {
                asset = ConfigParser.LoadConfigFromFile(assetFilePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading .p2asset: {ex.Message}");
                return false;
            }

            if (asset == null)
            {
                Debug.WriteLine("Asset is null???");
                return false;
            }

            // Create a unique folder for the asset
            string dirName = SanitizeDirectoryName(asset.Name);
            if (string.IsNullOrWhiteSpace(dirName))
                dirName = "Asset";

            string customDir = Path.Combine(Portal2Dir, "portal2", "custom");
            string assetFolder = Path.Combine(customDir, dirName);
            int suffix = 1;
            while (Directory.Exists(assetFolder))
            {
                assetFolder = Path.Combine(customDir, dirName + $"_{suffix++}");
            }
            Directory.CreateDirectory(assetFolder);

            // Extract all files from the .p2asset to the new folder
            try
            {
                ZipUtils.ExtractAndFlatten(assetFilePath, assetFolder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error extracting .p2asset: {ex.Message}");
                Debug.WriteLine($"{ex.StackTrace}");
                return false;
            }

            AssetBrowser.MountAssets(customDir);

            Debug.WriteLine($"Installed asset to: {assetFolder}");
            return true;
        }

        public bool InstallAsset(string assetFilePath)
        {
            InstallationInProgress = true;
            bool result = _InstallAsset(assetFilePath);
            InstallationInProgress = false;
            return result;
        }

        public void UninstallAsset(string id)
        {
            Asset? asset = Assets.FirstOrDefault(asset => asset.Id == id);
            if (asset == null)
            {
                Debug.WriteLine($"No asset to uninstall with id {id}");
                return;
            }

            string? Portal2Dir = Program.options.Portal2_Dir;
            if (Portal2Dir == null)
            {
                Debug.WriteLine("Portal 2 directory not found.");
                return;
            }
            string customDir = Path.Combine(Portal2Dir, "portal2", "custom");
            MountHandler.RemoveCustomSearchPathFromGameinfo(Portal2Dir, customDir, asset.FilePath);

            Debug.WriteLine($"Deleting directory {asset.FilePath}");
            FileSystem.DeleteDirectory(asset.FilePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

            Assets.Remove(asset);
        }

        public static void CreateAsset(AssetDefinition assetInfo, string fileDirectory, string imagePath, Stream destinationStream)
        {
            // TODO: validate assetInfo

            var tempDir = Path.GetTempPath();
            while (Path.Exists(tempDir))
            {
                tempDir = Path.GetTempPath() + Guid.NewGuid().ToString("n");
            }

            // This works because we always place the image in the root of the .p2asset file
            assetInfo.RelativeImagePath = Path.GetFileName(imagePath);

            StreamWriter definitionWriter;

            try
            {
                Directory.CreateDirectory(tempDir);
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(fileDirectory, tempDir);
                File.Copy(imagePath, Path.Combine(tempDir, Path.GetFileName(imagePath)));
                definitionWriter = File.CreateText(Path.Combine(tempDir, "def.toml"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown creation error " + ex.Message);
                return;
            }

            string tomlString = "";
            try
            {
                tomlString = Toml.FromModel(assetInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error" + ex.Message);
                return;
            }

            try
            {
                definitionWriter.Write(tomlString);
                definitionWriter.Flush();
                definitionWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filesystem write error" + ex.Message);
                return;
            }

            try
            {
                ZipFile.CreateFromDirectory(tempDir, destinationStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zip write error" + ex.Message);
                return;
            }

            Directory.Delete(tempDir, true);
        }

        public void Clear()
        {
            Assets.Clear();
        }

        private static string SanitizeDirectoryName(string name)
        {
            // Remove diacritics
            string normalized = name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            string ascii = sb.ToString().Normalize(NormalizationForm.FormC);

            // Remove non-ASCII and invalid path chars
            var invalidChars = Path.GetInvalidFileNameChars();
            var result = new StringBuilder();
            foreach (char c in ascii)
            {
                if (c >= 32 && c < 127 && !invalidChars.Contains(c))
                    result.Append(c);
                else if (c == ' ')
                    result.Append('_');

                // Custom character rules
                else if (char.IsSymbol(c) && c != '-' && c != '_')
                    continue;

                // else skip or replace with '_'
            }
            return result.ToString();
        }
    }
}