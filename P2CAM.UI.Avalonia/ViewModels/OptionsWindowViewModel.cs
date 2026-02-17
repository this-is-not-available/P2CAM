using CommunityToolkit.Mvvm.ComponentModel;
using P2CAM.Core;
using P2CAM.UI.Avalonia.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace P2CAM.UI.Avalonia.ViewModels
{
    public partial class OptionsWindowViewModel : ViewModelBase
    {
        public ObservableCollection<OptionItem> Options { get; }
            = new ObservableCollection<OptionItem>();

        private AssetManager assetManager;

        public OptionsWindowViewModel(AssetManager _assetManager)
        {
            assetManager = _assetManager;
            Options.Add(new OptionItem("Portal 2 Directory", assetManager.options.Portal2_Dir));
        }

        public void Save()
        {
            assetManager.options.Portal2_Dir = Options.FirstOrDefault(o => o.Name == "Portal 2 Directory")!.Value;
        }
    }
}
