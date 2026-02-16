using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2CAM.UI.Avalonia.Models
{
    public class DisplayItem
    {
        public string Title { get; set; } = "";
        public Bitmap? Image { get; set; }
    }
}