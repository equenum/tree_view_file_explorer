using Caliburn.Micro;
using System.Collections.Generic;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        public List<FileTreeItem> FileSystem { get; set; } = new List<FileTreeItem>();
    }
}
