using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TreeViewFileExplorerLibrary.FileSystemManagement;
using TreeViewFileExplorerLibrary.Models;
using System.Threading.Tasks;
using System.Threading;

namespace TreeViewFileExplorerUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        // TODO - Unit tests with build in test file system (xUnit)
        // TODO - Async file system reading +
        // TODO - Implement worker thread pool ?
        // TODO - Application status +

        private readonly IFileSystemReader _fileSystemReader;

        /// <summary>
        /// Represents the entire file system.
        /// </summary>
        public ObservableCollection<FileTreeItemModel> FileSystem { get; set; }
        /// <summary>
        /// Represents current application status.
        /// </summary>
        public string Status { get; set; } = "Push To Start";

        public ShellViewModel(IFileSystemReader fileSystemReader)
        {
            _fileSystemReader = fileSystemReader;
            FileSystem = new ObservableCollection<FileTreeItemModel>();

            DisplayName = "Tree View File Explorer";
        }
        
        public async Task LoadFileSystem()
        {
            Status = "Loading...";
            NotifyOfPropertyChange(() => Status);

            DriveInfo[] drives = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed).ToArray();

            foreach (var drive in drives)
            {
                var driveFileTreeItem = new FileTreeItemModel
                {
                    Name = drive.Name,
                    SubTrees = await _fileSystemReader.GetTreeInfoAsync(drive.Name),
                    ImageUri = "/Images/drive.png"
                };

                driveFileTreeItem.Size = driveFileTreeItem.SubTrees.Sum(x => x.Size);

                FileSystem.Add(driveFileTreeItem);
            }

            Status = "Push To Refresh";
            NotifyOfPropertyChange(() => Status);
        }
    }
}
