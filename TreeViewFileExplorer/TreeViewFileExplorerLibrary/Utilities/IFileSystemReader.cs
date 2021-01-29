using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerLibrary.FileSystemManagement
{
    public interface IFileSystemReader
    {
        Task<List<FileTreeItemModel>> GetTreeInfoAsync(string path);
    }
}
