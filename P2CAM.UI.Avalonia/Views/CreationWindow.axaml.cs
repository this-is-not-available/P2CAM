using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using P2CAM.UI.Avalonia.ViewModels;
using System;

namespace P2CAM.UI.Avalonia;

public partial class CreationWindow : Window
{
    public CreationWindow()
    {
        InitializeComponent();
    }

    public void SelectImageHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext is CreationWindowViewModel vm)
        {
            // 'this' is the Window, which GetTopLevel accepts
            vm.SelectImage(GetTopLevel(this)!);
        }
    }
    
    public void SelectFolderHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext is CreationWindowViewModel vm)
        {
            // 'this' is the Window, which GetTopLevel accepts
            vm.SelectFolder(GetTopLevel(this)!);
        }
    }
    
    public void CreateButtonHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext is CreationWindowViewModel vm)
        {
            // 'this' is the Window, which GetTopLevel accepts
            vm.CreateButton(GetTopLevel(this)!);
        }
    }
}