using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Metadata;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
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
        private string SelectedId = string.Empty;

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

                    SelectAsset(asset);
                }
            }
        }

        public void SelectAsset(Asset asset)
        {
            SelectedName = asset.Name;
            SelectedDescription = asset.Description;
            SelectedAuthor = "Author: " + asset.Author;
            SelectedCredit = ("Credit: " + asset.Credit).Replace("NotRequired", "Not Required"); ;
            SelectedVersion = "Version: " + asset.Version;
            SelectedId = asset.Id;

            SelectedImage.Dispose();
            SelectedImage = new Bitmap(Path.Combine(asset.FilePath, asset.Image));
        }

        public void Refresh()
        {
            assetManager.LoadAssetsInInstallation();

            // Unloaded selected asset

            // TODO: bad, bery bad, we should not use this image on my hard drive
            SelectedImage = new Bitmap("G:/SteamLibrary/steamapps/common/Portal 2/portal2_dlc2/materials/puzzlemaker/palette/turret.png");
            SelectedName = "unloaded";
            SelectedDescription += "unloaded";
            SelectedAuthor = "Author: unloaded";
            SelectedCredit = "Credit: unloaded";
            SelectedVersion = "Version: unloaded";
            SelectedId = string.Empty;

            // Real data

            Items.Clear();
            foreach (Asset asset in assetManager.Assets)
            {
                Items.Add(new DisplayItem
                {
                    Title = asset.Name,
                    Image = new Bitmap(Path.Combine(asset.FilePath, asset.Image)),
                    Id = asset.Id
                });
            }

            if (assetManager.Assets.Count > 0)
            {
                SelectAsset(assetManager.Assets[0]);
            }
        }

        public async void InstallAsset(TopLevel topLevel)
        {
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Asset",
                AllowMultiple = false,
                
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Custom Asset Files (*.p2asset)") { Patterns = new[] { "*.p2asset" } },
                    FilePickerFileTypes.All      // Allows any file
                }
            });

            foreach (var item in files)
            {
                Trace.WriteLine(item.Path);
            }

            if (files.Count == 1)
            {
                string? localPath = files[0].TryGetLocalPath();

                if (localPath != null)
                {
                    Console.WriteLine($"Selected file: {localPath}");
                    assetManager.InstallAsset(localPath);
                }
                Refresh();
            }
        }

        public async void UninstallAsset()
        {
            Trace.WriteLine(SelectedId);
            var box = MessageBoxManager.GetMessageBoxStandard(
                "Confirm deletion",
                "Are you sure you want to uninstall this asset?",
                ButtonEnum.YesNo,
                Icon.Warning
            );

            var result = await box.ShowAsync();

            if (result == ButtonResult.Yes)
            {
                assetManager.UninstallAsset(SelectedId);
                Refresh();
            }
        }

        // TODO: Implement
        public void OpenOptionsHandler()
        {

        }

        // TODO: Implement
        public void CreateHandler()
        {

        }

        public MainWindowViewModel(AssetManager _assetManager)
        {
            assetManager = _assetManager;
            Refresh();
        }
    }
}
