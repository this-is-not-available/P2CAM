using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using P2CAM.UI.Avalonia.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class CreationWindowViewModel : ViewModelBase
    {
        // Properties for bindings
        [ObservableProperty]
        public Bitmap? assetImage;
        [ObservableProperty]
        public string assetName = string.Empty;
        [ObservableProperty]
        public string assetDescription = string.Empty;
        [ObservableProperty]
        public string assetVersion = string.Empty;
        [ObservableProperty]
        public string assetSource = string.Empty;
        [ObservableProperty]
        public string assetAuthor = string.Empty;
        [ObservableProperty]
        public string assetTags = string.Empty;
        [ObservableProperty]
        public CreditType assetCredit = CreditType.NotRequired;
        public ObservableCollection<string> AssetFiles { get; }
            = new ObservableCollection<string>();

        public ObservableCollection<CreditType> CreditTypes { get; }
            = new ObservableCollection<CreditType>
        {
            CreditType.NotRequired,
            CreditType.Optional,
            CreditType.Required
        };

        private string SelectedImage = string.Empty;
        private string SelectedPath = string.Empty;

        public async void SelectImage(TopLevel topLevel)
        {
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select image",
                AllowMultiple = false,

                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Image Files (*.png; *.jpg; *.jpeg; *.bmp; *.tiff)") { Patterns = new[] { "*.png; *.jpg; *.jpeg; *.bmp; *.tiff" } },
                    FilePickerFileTypes.All      // Allows any file
                }
            });

            if (files.Count == 1)
            {
                string? localPath = files[0].TryGetLocalPath();

                if (localPath != null)
                {
                    SelectedImage = localPath;
                    Console.WriteLine($"Selected image: {localPath}");
                    try
                    {
                        Stream stream = File.OpenRead(localPath);
                        AssetImage = new Bitmap(stream);
                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        var box = MessageBoxManager.GetMessageBoxStandard(
                            "Error",
                            "There was an error reading or decoding this image and it cannot be previewed.\nError message: " + ex.Message,
                            ButtonEnum.Ok,
                            Icon.Error
                        );

                        var result = await box.ShowAsync();
                        return;
                    }
                }
            }
        }

        public async void SelectFolder(TopLevel topLevel)
        {
            var folders = await topLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = $"Select asset files",
                AllowMultiple = false
            });

            if (folders.Count > 0)
            {
                // Update the specific item's value
                string path = folders[0].TryGetLocalPath()!;
                SelectedPath = path;

                foreach (var file in AssetFiles)
                {
                    AssetFiles.Remove(file);
                }
                foreach (var folder in Directory.GetDirectories(path))
                {
                    // This is the first thing that came to mind when I thought of how to get the name of the folder
                    // Please forgive me
                    AssetFiles.Add(Path.GetFileName(Path.TrimEndingDirectorySeparator(folder) + ".png").TrimEnd(['p', 'n', 'g']).TrimEnd('.'));
                }

                foreach (var file in Directory.GetFiles(path))
                {
                    AssetFiles.Add(Path.GetFileName(file));
                }
            }
        }

        public async void CreateButton(TopLevel topLevel)
        {
            string[] tags = AssetTags.Split(',');

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].Trim();
            }

            AssetDefinition assetInfo = new AssetDefinition();
            assetInfo.Name = AssetName;
            assetInfo.Description = AssetDescription;
            assetInfo.Version = AssetVersion;
            assetInfo.Source = AssetSource;
            assetInfo.Author = AssetAuthor;
            assetInfo.Tags = tags;
            assetInfo.Credit = AssetCredit;

            if (SelectedImage == null || string.IsNullOrEmpty(SelectedImage) || !File.Exists(SelectedImage))
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                            "Invalid asset",
                            "The asset must have a thumbnail image selected!",
                            ButtonEnum.Ok,
                            Icon.Error
                        );

                var result = await box.ShowAsync();
                return;
            }

            if (SelectedPath == null || string.IsNullOrEmpty(SelectedPath) || !Path.Exists(SelectedPath))
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                            "Invalid asset",
                            "The asset must have a content root selected!",
                            ButtonEnum.Ok,
                            Icon.Error
                        );

                var result = await box.ShowAsync();
                return;
            }

            try
            {
                AssetManager.ValidateAssetDefinition(assetInfo);
            }
            catch (Exception exception)
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                            "Invalid asset",
                            exception.Message,
                            ButtonEnum.Ok,
                            Icon.Error
                        );

                var result = await box.ShowAsync();
                return;
            }

            // Ask the user where to save it

            var file = await topLevel!.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = $"Select asset files",
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("Custom Asset Files (*.p2asset)") { Patterns = new[] { "*.p2asset" } },
                    FilePickerFileTypes.All
                }
            });

            if (file != null)
            {
                // Write data to stream
                string savePath = file.TryGetLocalPath()!;

                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    var box = MessageBoxManager.GetMessageBoxStandard(
                                "Asset creation error",
                                "This folder for this path doesn't exist!",
                                ButtonEnum.Ok,
                                Icon.Error
                            );

                    var result = await box.ShowAsync();
                    return;
                }

                FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                try
                {
                    AssetManager.CreateAsset(assetInfo, SelectedPath!, SelectedImage!, fileStream);
                }
                catch (Exception exception)
                {
                    var box = MessageBoxManager.GetMessageBoxStandard(
                                "Asset creation error",
                                exception.Message,
                                ButtonEnum.Ok,
                                Icon.Error
                            );

                    var result = await box.ShowAsync();

                    fileStream.Close();

                    // this way the window doesn't close
                    return;
                }

                // TODO: close the window riiight here
                fileStream.Close();
            }
        }

        public void OpenFolderHandler()
        {
            // TODO: add cross-platform way to open the folder (explorer.exe, open, xdg-open..) and uncomment the button in the view
            //Process.Open
        }

        public CreationWindowViewModel()
        {

        }
    }
}
