using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2CAM.UI.Avalonia.Models
{
    public partial class OptionItem : ObservableObject
    {
        public string Name { get; set; } = "";
        [ObservableProperty]
        public string value = "";

        public OptionItem() { }

        public OptionItem(string defaultName)
        {
            Name = defaultName;
        }

        public OptionItem(string defaultName, string? defaultValue)
        {
            Name = defaultName;
            Value = defaultValue != null ? defaultValue : string.Empty;
        }
    }
}