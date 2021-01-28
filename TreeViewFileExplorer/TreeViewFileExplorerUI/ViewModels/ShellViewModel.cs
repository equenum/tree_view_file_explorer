using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TreeViewFileExplorerLibrary.FileSystemManagement;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IFileSystemReader _fileSystemReader;

        public ObservableCollection<FileTreeItemModel> FileSystem { get; set; }
        // TODO - Unit tests with build in test file system (xUnit)
        // TODO - Async file system reading
        // TODO - Implement worker thread pool pattern

        public ShellViewModel(IFileSystemReader fileSystemReader)
        {
            _fileSystemReader = fileSystemReader;
            FileSystem = new ObservableCollection<FileTreeItemModel>();

            DisplayName = "Tree View File Explorer";
        }
        
        public void LoadFileSystem()
        {
            DriveInfo[] drives = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed).ToArray();

            foreach (var drive in drives)
            {
                var driveFileTreeItem = new FileTreeItemModel
                {
                    Name = drive.Name,
                    SubTrees = _fileSystemReader.GetTreeInfo(drive.Name),
                    ImageUri = "/Images/drive.png"
                };

                driveFileTreeItem.Size = driveFileTreeItem.SubTrees.Sum(x => x.Size);

                FileSystem.Add(driveFileTreeItem);
            }
        }
    }
}
