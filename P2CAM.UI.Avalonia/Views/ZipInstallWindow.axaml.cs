using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using P2CAM.Core;
using P2CAM.UI.Avalonia.ViewModels;
using System;
using System.Diagnostics;

namespace P2CAM.UI.Avalonia;

public partial class ZipInstallWindow : Window
{
    public ZipInstallWindow()
    {
        InitializeComponent();
    }
    
    public async void SelectAssetHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext is ZipInstallWindowViewModel vm)
        {
            var files = await GetTopLevel(this)!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Asset",
                AllowMultiple = false,

                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Zip files (*.zip)") { Patterns = new[] { "*.zip" } },
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
                    Debug.WriteLine($"Selected file: {localPath}");
                    vm.ZipPath = localPath;
                }
            }
        }
    }

    public void InstallHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext is ZipInstallWindowViewModel vm)
        {
            vm.Install();
        }
    }

    public void CancelHandler(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}