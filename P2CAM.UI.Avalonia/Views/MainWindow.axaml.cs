using Avalonia.Controls;
using Avalonia.Interactivity;
using P2CAM.UI.Avalonia.ViewModels;
using System.Diagnostics;

namespace P2CAM.UI.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        bool closing = false;
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => {
                if (DataContext is MainWindowViewModel vm)
                {
                    if (closing) return;
                    e.Cancel = true;

                    Program.optionsLoader.Save();
                    // Return true to allow closing, false to cancel
                    if (vm.OnClosing())
                    {
                        closing = true;
                        Close();
                    }
                }
            };
        }

        public void InstallAssetHandler(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                // 'this' is the Window, which GetTopLevel accepts
                vm.InstallAsset(GetTopLevel(this)!);
            }
        }
    }
}