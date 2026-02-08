using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace P2CAM
{
    public static class ImageUtils
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            image.Dispose();
            return destImage;
        }
    }

    public static class SteamUtils
    {
        public static bool ValidatePortal2Directory(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            if (!Directory.Exists(path))
                return false;

            if (!File.Exists(Path.Combine(path, "portal2.exe")))
                return false;
            if (!File.Exists(Path.Combine(path, "portal2", "gameinfo.txt")))
                return false;

            return true;
        }

        public static string? FindPortal2Directory()
        {
            // TODO: Add cross-platform default Steam locations
            // 1. Check default Steam location (C:\Program Files (x86)\Steam\steamapps\common\Portal 2)
            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Steam", "steamapps", "common", "Portal 2");
            if (ValidatePortal2Directory(defaultPath))
                return defaultPath;

            // TODO: Add cross-platform default Steam locations
            // 2. Parse libraryfolders.vdf for all "path" entries
            string vdfPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Steam", "steamapps", "libraryfolders.vdf");
            if (File.Exists(vdfPath))
            {
                foreach (var path in GetSteamLibraryPathsFromVdf(vdfPath))
                {
                    string portal2Path = Path.Combine(path, "steamapps", "common", "Portal 2");
                    if (ValidatePortal2Directory(portal2Path))
                        return portal2Path;
                }
            }

            return null;
        }

        private static IEnumerable<string> GetSteamLibraryPathsFromVdf(string vdfPath) {
            var paths = new List<string>();
            foreach (var line in File.ReadAllLines(vdfPath))
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("\"path\""))
                {
                    // Extract the value between the second and third quote
                    int firstQuote = trimmed.IndexOf('"', 6);
                    int secondQuote = trimmed.IndexOf('"', firstQuote + 1);
                    if (firstQuote >= 0 && secondQuote > firstQuote)
                    {
                        string path = trimmed.Substring(firstQuote + 1, secondQuote - firstQuote - 1)
                            .Replace(@"\\", @"\")
                            .Trim();
                        paths.Add(path);
                    }
                }
            }
            return paths;
        }
    }

    public static class ZipUtils
    {
        public static void ExtractAndFlatten(string zipPath, string extractPath)
        {
            // Extract the ZIP to the specified directory
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            // Get all entries in the zip to analyze its structure
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                // Find top-level folders
                var topLevelFolders = archive.Entries
                    .Select(e => e.FullName.Split('/').FirstOrDefault())
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList();

                if (topLevelFolders.Count == 1)
                {
                    string singleFolder = topLevelFolders[0]!;
                    string folderPath = Path.Combine(extractPath, singleFolder);

                    // Move contents of the folder up one level
                    if (Directory.Exists(folderPath))
                    {
                        var directoryInfo = new DirectoryInfo(folderPath);

                        // Move top-level files
                        foreach (var file in directoryInfo.GetFiles())
                        {
                            string destFile = Path.Combine(extractPath, file.Name);
                            file.MoveTo(destFile);
                        }

                        // Move top-level folders and their contents
                        foreach (var dir in directoryInfo.GetDirectories())
                        {
                            string destDir = Path.Combine(extractPath, dir.Name);
                            dir.MoveTo(destDir);
                        }

                        // Remove the now empty folder
                        Directory.Delete(folderPath);
                    }
                }
            }
        }

        public static string ReadFileFromZipRoot(string zipPath, string fileName)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                // First, check for the file at the root
                var rootFileEntry = archive.Entries
                    .FirstOrDefault(e => e.FullName.Equals(fileName, StringComparison.OrdinalIgnoreCase));

                if (rootFileEntry != null)
                {
                    using (var stream = rootFileEntry.Open())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }

                // If not at root, check if there's exactly one folder at the root
                var folderNames = archive.Entries
                    .Select(e => e.FullName.Split('/').FirstOrDefault())
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList();

                if (folderNames.Count == 1)
                {
                    string folderName = folderNames[0]!;

                    // Search for the file inside that folder
                    var folderFileEntry = archive.Entries
                        .FirstOrDefault(e => e.FullName.Equals($"{folderName}/{fileName}", StringComparison.OrdinalIgnoreCase));

                    if (folderFileEntry != null)
                    {
                        using (var stream = folderFileEntry.Open())
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }

                // File not found
                return string.Empty;
            }
        }
    }
}