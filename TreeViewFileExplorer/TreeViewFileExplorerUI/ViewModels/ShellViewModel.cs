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
        private readonly SynchronizationContext _uiThreadContext;
        private readonly IFileSystemReader _fileSystemReader;

        /// <summary>
        /// Represents the entire file system.
        /// </summary>
        public ObservableCollection<ITreeComponent> FileSystem { get; set; }
        /// <summary>
        /// Represents current application status.
        /// </summary>
        public string Status { get; set; } = "Click To Start";

        public ShellViewModel(IFileSystemReader fileSystemReader)
        {
            _fileSystemReader = fileSystemReader;
            _uiThreadContext = SynchronizationContext.Current;
            FileSystem = new ObservableCollection<ITreeComponent>();

            DisplayName = "Tree View File Explorer";
        }
        
        public void GetFileSystem()
        {
            Status = "Loading...";
            NotifyOfPropertyChange(() => Status);

            ThreadPool.QueueUserWorkItem(async (i) =>
            {
                var fileSystemTrees = await _fileSystemReader.GetFileSystemTreeAsync();

                foreach (var fileTree in fileSystemTrees)
                {
                    _uiThreadContext.Send(x => FileSystem.Add(fileTree), null);
                }

                Status = "Click To Refresh";
                NotifyOfPropertyChange(() => Status);
            });
        }
    }
}
