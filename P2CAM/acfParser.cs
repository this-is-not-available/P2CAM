using System.Diagnostics;
using System.IO.Compression;
using static P2CAM.AssetBrowser;

namespace P2CAM
{
    public class ACFParser
    {
        public static Asset? LoadACFFromString(string data)
        {
            //Debug.WriteLine("Raw definiton data: " + data);
            if (string.IsNullOrWhiteSpace(data))
            {
                Debug.WriteLine("ACF is Whitespace");
                return null;
            }

            data = data.Replace(System.Environment.NewLine, "\n");
            data = data.Replace("-\n", "-");

            Asset asset = new Asset();
            string[] lines = data.Split('-', StringSplitOptions.RemoveEmptyEntries); // This is a line-

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                switch (parts[0])
                {
                    case "Name":
                        asset.Name = line.Substring(5, line.Length - 5);
                        continue;
                    case "Description":
                        asset.Description = line.Substring(12, line.Length - 12);
                        continue;
                    case "Source":
                        asset.Source = parts[1];
                        continue;
                    case "Image":
                        string imagePath = line.Substring(6, line.Length - 6);
                        asset.RelativeImagePath = imagePath;
                        continue;
                    case "Comment":
                        continue;
                }
            }

            return asset;
        }

        public static Asset? LoadACFFromFile(string assetFile = "Asset.p2asset")
        {
            string data = ZipUtils.ReadFileFromZipRoot(assetFile, "def.toml");
            if (string.IsNullOrWhiteSpace(data))
                return null;

            Asset? asset = LoadACFFromString(data);
            if (asset != null)
                asset.FilePath = "";
            return asset;
        }
    }
}