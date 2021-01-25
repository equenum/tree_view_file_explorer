using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    public class FileTreeItem
    {
        public string Name { get; set; }
        public decimal Size { get; set; }  // maybe long?
        public List<FileTreeItem> SubTrees { get; set; } = new List<FileTreeItem>();
    }
}
