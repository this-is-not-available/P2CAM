using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using P2CAM.Core;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class ZipInstallWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string zipPath = string.Empty;
        [ObservableProperty]
        public string assetName = string.Empty;
        [ObservableProperty]
        public string author = string.Empty;
        public ObservableCollection<CreditType> CreditTypes { get; } = new ObservableCollection<CreditType>((CreditType[])Enum.GetValues(typeof(CreditType)));
        [ObservableProperty]
        public CreditType creditType = CreditType.Unknown;
        private AssetManager assetManager;

        public ZipInstallWindowViewModel(AssetManager AssetManager)
        {
            assetManager = AssetManager;
        }

        public bool Install()
        {
            string errorText = string.Empty;

            if (string.IsNullOrEmpty(ZipPath) | !File.Exists(ZipPath))
            {
                //if (!string.IsNullOrEmpty(errorText))
                //    errorText += ", "; this check is first, so this will never run
                errorText += "path to zip file";
            }

            if (string.IsNullOrEmpty(AssetName))
            {
                if (!string.IsNullOrEmpty(errorText))
                    errorText += ", ";
                errorText += "name";
            }

            if (string.IsNullOrEmpty(Author))
            {
                if (!string.IsNullOrEmpty(errorText))
                    errorText += ", ";
                errorText += "author";
            }

            if (!string.IsNullOrEmpty(errorText))
            {
                var box = MessageBoxManager.GetMessageBoxStandard(
                    "Invalid information",
                    "The information provided is missing or incorrect: " + errorText,
                    ButtonEnum.Ok,
                    Icon.Warning
                );

                _ = box.ShowAsync();
                return false;
            }

            AssetDefinition definition = new AssetDefinition
            {
                Author = Author,
                Name = AssetName,
                Credit = CreditType
            };
            bool result = assetManager.InstallAssetFromZip(ZipPath, definition);

            return result;
        }
    }
}
