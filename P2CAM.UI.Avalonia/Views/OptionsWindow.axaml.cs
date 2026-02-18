using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace P2CAM.UI.Avalonia;

public partial class OptionsWindow : Window
{
    public event EventHandler? SettingsSaved;

    public OptionsWindow()
    {
        InitializeComponent();
    }
}