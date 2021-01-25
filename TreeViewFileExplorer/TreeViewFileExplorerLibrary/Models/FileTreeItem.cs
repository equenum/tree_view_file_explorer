using System;
using System.Collections.Generic;
using System.Text;

namespace TreeViewFileExplorerLibrary.Models
{
    public class FileTreeItem
    {
        public string Name { get; set; }
        public decimal Size { get; set; }  // maybe long?
        public List<FileTreeItem> SubTrees { get; set; } = new List<FileTreeItem>();
    }
}
