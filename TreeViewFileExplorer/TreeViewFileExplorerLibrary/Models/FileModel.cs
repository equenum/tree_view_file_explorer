using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single file.
    /// </summary>
    public class FileModel : ITreeComponent
    {
        private long _size;
        private string _sizeForShow;
        
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public long Size 
        {
            get => _size;
            set
            {
                _size = value;
                _sizeForShow = value.ToString();
            }
        }
        public string SizeForShow 
        {
            get => _sizeForShow;
            set => _sizeForShow = value;
        }

        public FileModel()
        {
            ImageUri = "/Images/file.png";
        }
    }
}
