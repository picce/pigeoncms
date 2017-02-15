using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonCms.Controls.ItemsAdmin
{
    public class UploadParameters
    {
        public string UniqueID { get; set; }
        public string AllowedFileTypes { get; set; }
        public int MaxFileSize { get; set; }
    }
}
