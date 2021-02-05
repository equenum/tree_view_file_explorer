using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single folder.
    /// </summary>
    public class FolderModel : IFolderModel
    {
        private long _size;
        private string _sizeForShow;
        
        public string Name { get; set; }
        public string ImageUri { get; set; } 
        public List<ITreeComponent> ChildTreeItems { get; set; } 
        public long Size
        {
            get  => _size; 
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

        public FolderModel()
        {
            ImageUri = "/Images/folder.png";
            ChildTreeItems = new List<ITreeComponent>();
        }

        public void CalculateSize()
        {
            Size = ChildTreeItems.Sum(x => x.Size);
        }
    }
}
