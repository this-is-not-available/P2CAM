using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2CAM.UI.Avalonia.Models
{
    public class FileListItem
    {
        public string FileName { get; set; } = "";
        public string FileExt { get; set; } = "";
        public double ExtSpacing { get; set; } = 0;
        public Bitmap? Icon { get; set; }

        public FileListItem(string fileName, string fileExt, Bitmap icon)
        {
            FileName = fileName;
            FileExt = fileExt;
            Icon = icon;

            if (FileExt.Length > 5)
            {
                // Cap file extensions at 4 characters after . (e.g. .jpeg)
                FileExt = FileExt.Remove(5);
            }

            if (FileExt.Length > 4)
            {
                // If at 4 characters, collapse characters so they don't overflow
                ExtSpacing = -1;
            }
        }
    }
}