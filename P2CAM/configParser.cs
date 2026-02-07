using System.Diagnostics;
using Tomlyn;

namespace P2CAM
{
    public class ConfigParser
    {
        public static Asset? LoadConfigFromString(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                Debug.WriteLine("TOML is Whitespace");
                return null;
            }

            try
            {
                var model = Toml.ToModel<Asset>(data);

                Asset asset = model;

                return asset;
            }
            catch (TomlException ex)
            {
                Debug.WriteLine($"TOML parse error: {ex.Message}");
                return null;
            }
        }

        public static Asset? LoadConfigFromFile(string assetFile = "Asset.p2asset")
        {
            string data = ZipUtils.ReadFileFromZipRoot(assetFile, "def.toml");
            if (string.IsNullOrWhiteSpace(data))
            {
                Debug.WriteLine("def.toml data is null or whitespace");
                return null;
            }

            Asset? asset = LoadConfigFromString(data);
            if (asset != null)
            {
                asset.FilePath = "";
            } else
            {
                Debug.WriteLine("Asset is null here too");
            }
            
            return asset;
        }
    }
}