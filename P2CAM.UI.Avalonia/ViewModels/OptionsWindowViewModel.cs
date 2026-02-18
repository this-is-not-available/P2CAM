using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class OptionsWindowViewModel : ViewModelBase
    {
        public ObservableCollection<OptionItem> Options { get; }
            = new ObservableCollection<OptionItem>();

        private AssetManager assetManager;
        private Action onSaved;

        public OptionsWindowViewModel(AssetManager _assetManager, Action _onSaved)
        {
            assetManager = _assetManager;
            onSaved = _onSaved;
            Options.Add(new PathOption("Portal 2 Directory", assetManager.options.Portal2_Dir!));
        }

        public void Save()
        {
            assetManager.options.Portal2_Dir = (Options.FirstOrDefault(o => o.Name == "Portal 2 Directory") as PathOption)!.Path;
            onSaved?.Invoke();
        }

        public async void SelectPathCommand(object parameter)
        {
            var values = parameter as IList<object>;
            var optionItem = values?[0] as PathOption;
            var window = values?[1] as Window;

            if (optionItem != null && window != null)
            {
                var topLevel = TopLevel.GetTopLevel(window);

                var folders = await topLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = $"Select {optionItem.Name}",
                    AllowMultiple = false
                });

                if (folders.Count > 0)
                {
                    // Update the specific item's value
                    optionItem.Path = folders[0].TryGetLocalPath()!;
                }
            }
        }
    }
}
