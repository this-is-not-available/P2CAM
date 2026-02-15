using Avalonia.Media.Imaging;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public Bitmap SelectedImage { get; } = new Bitmap("G:/SteamLibrary/steamapps/common/Portal 2/portal2_dlc2/materials/puzzlemaker/palette/turret.png");
        public string SelectedName { get; } = string.Empty;
        public string SelectedDescription { get; } = string.Empty;
        public string SelectedAuthor { get; } = string.Empty;
        public string SelectedCredit { get; } = string.Empty;
        public string SelectedVersion { get; } = string.Empty;

        public ObservableCollection<DisplayItem> Items { get; }
            = new ObservableCollection<DisplayItem>();

        private AssetManager assetManager;

        public MainWindowViewModel(AssetManager _assetManager)
        {
            assetManager = _assetManager;

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

            for (int i = 0; i < 2; i++)
            {
                Items.Add(new DisplayItem
                {
                    Title = "Item " + (i + 1).ToString(),
                    ImagePath = "avares://P2CAM.UI.Avalonia/Assets/avalonia-logo.png"
                });
            }
        }
    }
}
