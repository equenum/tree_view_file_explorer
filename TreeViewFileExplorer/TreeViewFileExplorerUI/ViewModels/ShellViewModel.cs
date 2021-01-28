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
        // TODO - Add application status
        // TODO - Shell view window icon

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
