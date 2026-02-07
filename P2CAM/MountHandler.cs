using System;
using System.Diagnostics;

namespace P2CAM
{
    public static class MountHandler
    {
        /// <summary>
        /// Adds a #base statement to gameinfo.txt to include a custom asset config file.
        /// </summary>
        /// <param name="Portal2Dir">Root Portal 2 directory</param>
        /// <param name="customFolder">Path to the custom folder

        public static void AddCustomSearchPathsToGameInfo(string Portal2Dir, string customFolder)
        {
            string gameinfoPath = Path.Combine(Portal2Dir, "portal2", "gameinfo.txt");
            if (!File.Exists(gameinfoPath))
                throw new FileNotFoundException("gameinfo.txt not found.", gameinfoPath);

            var assetDirs = AssetManager.FindCustomAssetDirectories(customFolder)
                .Select(dir => Path.GetFileName(dir))
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList();

            var searchPathsLines = assetDirs
                .Select(dirName => $"\t\t\tGame\t\"|gameinfo_path|custom/{dirName}\"")
                .ToList();

            List<string> lines = File.ReadAllLines(gameinfoPath).ToList();
            int searchPathsIndex = lines.FindIndex(l => l.Trim().Equals("SearchPaths", StringComparison.OrdinalIgnoreCase));
            if (searchPathsIndex == -1)
                throw new InvalidOperationException("SearchPaths section not found in gameinfo.txt.");

            // Find the opening brace for SearchPaths
            int openBraceIndex = lines.FindIndex(searchPathsIndex, l => l.Trim() == "{");
            if (openBraceIndex == -1)
                throw new InvalidOperationException("Opening brace for SearchPaths not found.");

            // Find the opening brace for SearchPaths
            int closeBraceIndex = lines.FindIndex(openBraceIndex, l => l.Trim() == "}");
            if (closeBraceIndex == -1)
                throw new InvalidOperationException("Opening brace for SearchPaths not found.");

            // Extract existing mounted folders
            HashSet<string> mountedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = openBraceIndex + 1; i < closeBraceIndex; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("Game"))
                {
                    // Remove the "Game" prefix
                    string remainder = line.Substring("Game".Length).Trim();
                    string folderName;

                    // Check if the folder name is quoted
                    int startQuote = remainder.IndexOf('"');
                    int endQuote = remainder.LastIndexOf('"');

                    if (startQuote != -1 && endQuote != -1 && endQuote > startQuote)
                    {
                        folderName = remainder.Substring(startQuote + 1, endQuote - startQuote - 1);
                    }
                    else
                    {
                        // No quotes, assume the remainder is the folder name
                        folderName = remainder.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }

                    if (!string.IsNullOrEmpty(folderName))
                    {
                        //Debug.WriteLine($"Detected mount for {folderName}");
                        mountedFolders.Add(folderName);
                    }
                }
            }

            List<string> foldersToAdd = new List<string>();
            foreach (string folder in searchPathsLines)
            {
                string folderName = folder.Trim().Substring("Game".Length).Trim();
                if (!mountedFolders.Contains(folderName.Replace("\"", string.Empty)))
                {
                    Debug.WriteLine($"Mounting {folderName}");
                    foldersToAdd.Add(folder);
                }
            }

            // Insert after the opening brace
            lines.InsertRange(closeBraceIndex - 0, foldersToAdd);

            foreach (var line in lines)
                //Debug.WriteLine(line);

            BackupGameinfo(gameinfoPath);
            File.WriteAllLines(gameinfoPath, lines);
        }

        public static void RemoveCustomSearchPathFromGameinfo(string Portal2Dir, string customFolder, string searchPath)
        {
            string gameinfoPath = Path.Combine(Portal2Dir, "portal2", "gameinfo.txt");
            if (!File.Exists(gameinfoPath))
                throw new FileNotFoundException("gameinfo.txt not found.", gameinfoPath);

            var searchPathsLine = $"\t\t\tGame\t\"|gameinfo_path|custom/{Path.GetFileName(searchPath)}\"";
            Debug.WriteLine(searchPathsLine);

            List<string> lines = File.ReadAllLines(gameinfoPath).ToList();
            lines.Remove(searchPathsLine);
            lines.Remove(searchPathsLine.Replace("\"", ""));

            foreach (var line in lines)
                Debug.WriteLine(line);

            File.WriteAllLines(gameinfoPath, lines);
        }

        public static void BackupGameinfo(string gameinfoPath)
        {
            string backupPath = gameinfoPath.Replace("gameinfo.txt", "gameinfo_backup.txt");

            // Check if the original exists
            if (!File.Exists(gameinfoPath))
            {
                Console.WriteLine("gameinfo.txt not found.");
                return;
            }

            // Check if a backup already exist
            if (File.Exists(backupPath))
            {
                Console.WriteLine("Backup already exists.");
                return;
            }

            // Create the backup
            File.Copy(gameinfoPath, backupPath);
            Console.WriteLine("Backup created successfully.");
        }
    }
}