using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public Bitmap selectedImage = new WriteableBitmap(new PixelSize(1, 1), new Vector(96, 96), PixelFormat.Bgra8888);
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
        private OptionsWindow? options;

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

            SelectedImage = new WriteableBitmap(new PixelSize(1, 1), new Vector(96, 96), PixelFormat.Bgra8888);
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

        public void UpdateGlobalTheme()
        {
            Application.Current!.RequestedThemeVariant = (assetManager.options as AppOptions)!.AppTheme switch
            {
                Theme.Light => ThemeVariant.Light,
                Theme.Dark => ThemeVariant.Dark,
                _ => ThemeVariant.Default // Theme.System
            };
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

        public void OpenOptionsHandler()
        {
            if (options == null)
            {
                var vm = new OptionsWindowViewModel(assetManager,
                () => // Invoked when user saves
                {
                    Refresh();
                    UpdateGlobalTheme();
                });
                options = new OptionsWindow { DataContext = vm };

                options.Show();

                options.Closed += (object? sender, EventArgs e) =>
                {
                    options = null;
                };
            }
        }

        // Return true to allow closing
        public bool OnClosing()
        {
            options?.Close();
            return true;
        }
        
        // TODO: Implement
        public void CreateHandler()
        {

        }

        public MainWindowViewModel(AssetManager _assetManager)
        {
            assetManager = _assetManager;
            Refresh();
            UpdateGlobalTheme();
        }
    }
}
