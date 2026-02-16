using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public Bitmap selectedImage = new Bitmap("G:/SteamLibrary/steamapps/common/Portal 2/portal2_dlc2/materials/puzzlemaker/palette/turret.png");
        [ObservableProperty]
        public string selectedName = string.Empty;
        [ObservableProperty]
        public string selectedDescription = string.Empty;
        [ObservableProperty]
        public string selectedAuthor = string.Empty;
        [ObservableProperty]
        public string selectedCredit = string.Empty;
        [ObservableProperty]
        public string selectedVersion = string.Empty;

        public ObservableCollection<DisplayItem> Items { get; }
            = new ObservableCollection<DisplayItem>();

        private AssetManager assetManager;

        public void AssetClickHandler(string Id)
        {
            foreach (Asset asset in assetManager.Assets)
            {
                if (Id == asset.Id)
                {
                    Trace.WriteLine($"Asset {asset.Name}");

                    SelectedName = asset.Name;
                    SelectedDescription = asset.Description;
                    SelectedAuthor = "Author: " + asset.Author;
                    SelectedCredit = ("Credit: " + asset.Credit).Replace("NotRequired", "Not Required"); ;
                    SelectedVersion = "Version: " + asset.Version;

                    SelectedImage.Dispose();
                    SelectedImage = new Bitmap(Path.Combine(asset.FilePath, asset.Image));
                }
            }
        }

        public MainWindowViewModel(AssetManager _assetManager)
        {
            assetManager = _assetManager;
            assetManager.LoadAssetsInInstallation();

            // Temporary test data

            SelectedImage = new Bitmap("G:/SteamLibrary/steamapps/common/Portal 2/portal2_dlc2/materials/puzzlemaker/palette/turret.png");
            SelectedName = "teh epic aset";
            for (int i = 0; i < 200; i++)
            {
                SelectedDescription += "A very long description to test the capabilities of Avalonia.A very long description to test the capabilities of Avalonia.";
            }
            SelectedAuthor = "Author: me ofc";
            SelectedCredit = "Credit: Not required";
            SelectedVersion = "Version: 1.0.0";

            // Real data

            foreach (Asset asset in assetManager.Assets)
            {
                Items.Add(new DisplayItem
                {
                    Title = asset.Name,
                    Image = new Bitmap(Path.Combine(asset.FilePath, asset.Image)),
                    Id = asset.Id
                });
            }
        }
    }
}
