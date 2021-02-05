using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewFileExplorerLibrary.Models
{
    /// <summary>
    /// Represents a composite element of file tree structure (folder).
    /// </summary>
    public interface IFolderModel : ITreeComponent
    {
        /// <summary>
        /// Represents a list of child tree items for a given composite item (folder).
        /// </summary>
        List<ITreeComponent> ChildTreeItems { get; set; }
        /// <summary>
        /// Calculates folder size (including all of the child tree items size).
        /// </summary>
        void CalculateSize();
    }
}
