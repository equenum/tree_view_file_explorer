using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single file tree item.
    /// </summary>
    public class FileTreeItemModel
    {
        // TODO - make bytes, kilobates other as constant fields
        private long _size;
        private long _sizeForShow;

        /// <summary>
        /// Represents file tree item name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents file tree item icon image uri.
        /// </summary>
        public string ImageUri { get; set; } 
        public List<FileTreeItemModel> SubTrees { get; set; } = new List<FileTreeItemModel>();
        /// <summary>
        /// Represents file tree item size 
        /// (to be used for calculation puproses).
        /// </summary>
        public long Size
        {
            get { return _size; }
            
            set 
            { 
                _size = value;
                _sizeForShow = value;
            }
        }
        /// <summary>
        /// Represents file tree item size 
        /// (to be used for user interface illustrations purposes).
        /// </summary>
        public string SizeForShow
        {
            get 
            {
                string size = _sizeForShow.ToString();
                string sizeUnit = "B";

                if (_sizeForShow > 1024 && _sizeForShow < 1048576)
                {
                    decimal value = (decimal)_sizeForShow / 1024;
                    size = Math.Round(value, MidpointRounding.AwayFromZero).ToString();
                    sizeUnit = "KB";
                }
                else if (_sizeForShow > 1048576 && _sizeForShow < 1073741824)
                {
                    decimal value = (decimal)_sizeForShow / 1048576;
                    size = Math.Round(value, MidpointRounding.AwayFromZero).ToString();
                    sizeUnit = "MB";
                }
                else if (_sizeForShow > 1073741824)
                {
                    decimal value = (decimal)_sizeForShow / 1073741824;
                    size = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString();
                    sizeUnit = "GB";
                }

                return $"({size} {sizeUnit})"; 
            }
        }
    }
}
