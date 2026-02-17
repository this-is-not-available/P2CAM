using Avalonia.Controls;
using Avalonia.Interactivity;
using P2CAM.UI.Avalonia.ViewModels;
using System.Diagnostics;

namespace P2CAM.UI.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void InstallAssetHandler(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                // 'this' is the Window, which GetTopLevel accepts
                vm.InstallAsset(GetTopLevel(this)!);
            }
        }
    }
}