using System.Collections.Generic;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a single element of file tree structure.
    /// </summary>
    public interface ITreeComponent
    {
        /// <summary>
        /// Represents file tree item icon image uri.
        /// </summary>
        string ImageUri { get; set; }
        /// <summary>
        /// Represents file tree item name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Represents file tree item size in bytes
        /// (to be used for calculation puproses).
        /// </summary>
        long Size { get; set; }
        /// <summary>
        /// Represents file tree item size 
        /// (to be used for user interface illustrations purposes).
        /// </summary>
        string SizeForShow { get; set; }
    }
}