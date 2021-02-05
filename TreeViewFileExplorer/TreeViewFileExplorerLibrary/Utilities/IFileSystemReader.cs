using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerLibrary.FileSystemManagement
{
    /// <summary>
    /// Represents a file system reader.
    /// </summary>
    public interface IFileSystemReader
    {
        /// <summary>
        /// Represents a target file system path(s).
        /// </summary>
        List<string> FilePaths { get; set; }

        /// <summary>
        /// Gets file system tree information.
        /// </summary>
        /// <returns>File system tree information.</returns>
        Task<List<IFolderModel>> GetFileSystemTreeAsync();
        /// <summary>
        /// Clears current target file system path(s).
        /// </summary>
        void ClearFilePaths();
        /// <summary>
        /// Adds new target file system path.
        /// </summary>
        /// <param name="path">New target file system path.</param>
        void AddFilePath(string path);
    }
}
