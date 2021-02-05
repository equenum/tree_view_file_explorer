using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.FileSystemManagement;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerLibrary
{
    /// <summary>
    /// Represents a file system reader.
    /// </summary>
    public class FileSystemReader : IFileSystemReader 
    {
        public List<string> FilePaths { get; set; }

        public FileSystemReader()
        {
            FilePaths = DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed)
                .Select(x => x.Name)
                .ToList();
        }
        
        public async Task<List<IFolderModel>> GetFileSystemTreeAsync() 
        {
            var resultFileSystemTrees = new List<IFolderModel>();

            foreach (var path in FilePaths)
            {
                var driveTree = new FolderModel
                {
                    Name = path,
                    ChildTreeItems = await GetTreeInfoAsync(path),
                    ImageUri = "/Images/drive.png"
                };

                driveTree.CalculateSize();

                var sizedDriveTree = new SizedFolder(driveTree);
                sizedDriveTree.SetSizeUnits();

                resultFileSystemTrees.Add(sizedDriveTree);
            }

            return resultFileSystemTrees;
        }

        private async Task<List<ITreeComponent>> GetTreeInfoAsync(string path)
        {
            var resultTreeInfo = new List<ITreeComponent>();

            string[] rootFolders = Directory.GetDirectories(path);

            List<Task<ITreeComponent>> tasks = new List<Task<ITreeComponent>>();

            foreach (var folder in rootFolders)
            {
                tasks.Add(Task.Run(() => GetChildFoldersInfo(folder)));
            }

            var childFoldersInfo = await Task.WhenAll(tasks);

            foreach (var folderInfo in childFoldersInfo)
            {
                resultTreeInfo.Add(folderInfo);
            }

            var filesInfo = await GetFilesInfoAsync(path);
            resultTreeInfo.AddRange(filesInfo);

            return resultTreeInfo;
        }

        private async Task<ITreeComponent> GetChildFoldersInfo(string path) 
        {
            var resultFolder = new FolderModel();

            var childDirectories = await GetFoldersInfoAsync(path);
            var files = await GetFilesInfoAsync(path);

            resultFolder.ChildTreeItems.AddRange(childDirectories);
            resultFolder.ChildTreeItems.AddRange(files);
            resultFolder.Name = Path.GetFileName(path);
            resultFolder.CalculateSize();

            var sizedResultFolder = new SizedFolder(resultFolder);
            sizedResultFolder.SetSizeUnits();

            return sizedResultFolder;
        }

        private async Task<List<ITreeComponent>> GetFoldersInfoAsync(string path)
        {
            var resultFoldersInfo = new List<ITreeComponent>();

            var folders = GetAccessableFolders(path);

            List<Task<ITreeComponent>> tasks = new List<Task<ITreeComponent>>();

            foreach (var folder in folders)
            {
                tasks.Add(Task.Run(() => GetChildFoldersInfo(folder)));
            }

            var childFoldersInfo = await Task.WhenAll(tasks);

            foreach (var folderInfo in childFoldersInfo)
            {
                resultFoldersInfo.Add(folderInfo);
            }

            return resultFoldersInfo;
        }

        private async Task<List<ITreeComponent>> GetFilesInfoAsync(string path)
        {
            var resultFilesInfo = new List<ITreeComponent>();

            var files = GetAccessableFiles(path);

            List<Task<ITreeComponent>> tasks = new List<Task<ITreeComponent>>();

            foreach (var file in files)
            {
                tasks.Add(Task.Run(() => GetSingleFileInfo(file)));
            }

            var filesInfo = await Task.WhenAll(tasks);

            foreach (var fileInfo in filesInfo)
            {
                resultFilesInfo.Add(fileInfo);
            }

            return resultFilesInfo;
        }

        private ITreeComponent GetSingleFileInfo(string path) 
        {
            var fileInfoHelper = new FileInfo(path);

            var fileInfo = new FileModel()
            {
                Name = fileInfoHelper.Name,
                Size = GetSingleFileSize(fileInfoHelper)
            };

            var sizedFile = new SizedFile(fileInfo);
            sizedFile.SetSizeUnits();

            return sizedFile;
        }

        /// <summary>
        /// Avoids UnauthorizedAccessException for cases, when the program 
        /// attempts to get system directory path(s) from a system 
        /// drive (C:\\) without granted access.
        /// </summary>
        /// <param name="path">Full directory path.</param>
        /// <returns>Directory path(s) or empty array.</returns>
        private string[] GetAccessableFolders(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch (Exception)
            {
                return new string[0];
            }
        }

        /// <summary>
        /// Avoids UnauthorizedAccessException for cases, when the program 
        /// attempts to get system file path(s) from a system 
        /// directory (C:\\) without granted access.
        /// </summary>
        /// <param name="path">Full file path.</param>
        /// <returns>File path(s) or empty array.</returns>
        private string[] GetAccessableFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch (Exception)
            {
                return new string[0];
            }
        }
        
        /// <summary>
        /// Avoids FileNotFoundException for cases, when the program 
        /// attempts to get system file information from a system 
        /// directory (C:\\) without granted access.
        /// </summary>
        /// <param name="fileInfo">FileInfo helper.</param>
        /// <returns>File size or zero.</returns>
        private long GetSingleFileSize(FileInfo fileInfo)
        {
            try
            {
                return fileInfo.Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void ClearFilePaths()
        {
            FilePaths.Clear();
        }

        public void AddFilePath(string path)
        {
            FilePaths.Add(path);
        }
    }
}
