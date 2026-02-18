using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2CAM.UI.Avalonia.Models
{
    public abstract class OptionItem : ObservableObject
    {
        public string Name { get; set; } = "";

        public OptionItem() { }

        public OptionItem(string defaultName)
        {
            Name = defaultName;
        }
    }

    public partial class PathOption : OptionItem
    {
        [ObservableProperty]
        public string path = "";

        public PathOption(string defaultName, string _path)
        {
            Name = defaultName;
            Path = _path;
        }
    }

    public partial class TextOption : OptionItem
    {
        [ObservableProperty]
        public string value = "";

        public TextOption(string defaultName, string _value)
        {
            Name = defaultName;
            Value = _value;
        }
    }
}