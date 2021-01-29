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
    /// Represents file system reader.
    /// </summary>
    public class FileSystemReader : IFileSystemReader 
    {
        /// <summary>
        /// Gets file system tree information.
        /// </summary>
        /// <param name="path">Target file system path.</param>
        /// <returns>File system tree information</returns>
        public async Task<List<FileTreeItemModel>> GetTreeInfoAsync(string path)
        {
            var result = new List<FileTreeItemModel>();

            string[] rootDirectories = Directory.GetDirectories(path);

            List<Task<FileTreeItemModel>> tasks = new List<Task<FileTreeItemModel>>();

            foreach (var directory in rootDirectories)
            {
                tasks.Add(Task.Run(() => GetChildTreeInfo(directory)));
            }

            var filesInfoResults = await Task.WhenAll(tasks);

            foreach (var item in filesInfoResults)
            {
                result.Add(item);
            }

            var filesInfo = await GetFilesInfoAsync(path);
            result.AddRange(filesInfo);

            return result;
        }

        private async Task<FileTreeItemModel> GetChildTreeInfo(string path) 
        {
            var result = new FileTreeItemModel();

            var childDirectories = await GetChildDirectoriesInfoAsync(path);
            var files = await GetFilesInfoAsync(path);

            result.SubTrees.AddRange(childDirectories);
            result.SubTrees.AddRange(files);
            result.Name = Path.GetFileName(path);
            result.Size = result.SubTrees.Sum(x => x.Size);
            result.ImageUri = "/Images/folder.png";

            return result;
        }

        private async Task<List<FileTreeItemModel>> GetChildDirectoriesInfoAsync(string path)
        {
            var result = new List<FileTreeItemModel>();

            var directories = GetDirectoriesWithGrantedAccess(path);

            List<Task<FileTreeItemModel>> tasks = new List<Task<FileTreeItemModel>>();

            foreach (var directory in directories)
            {
                tasks.Add(Task.Run(() => GetChildTreeInfo(directory)));
            }

            var childTreeInfoResults = await Task.WhenAll(tasks);

            foreach (var item in childTreeInfoResults)
            {
                result.Add(item);
            }

            return result;
        }

        private async Task<List<FileTreeItemModel>> GetFilesInfoAsync(string path)
        {
            var result = new List<FileTreeItemModel>();

            var files = GetFilesWithGrantedAcess(path);

            List<Task<FileTreeItemModel>> tasks = new List<Task<FileTreeItemModel>>();

            foreach (var file in files)
            {
                tasks.Add(Task.Run(() => GetSingleFileInfo(file)));
            }

            var fileInfoResults = await Task.WhenAll(tasks);

            foreach (var item in fileInfoResults)
            {
                result.Add(item);
            }
            return result;
        }

        private FileTreeItemModel GetSingleFileInfo(string path) 
        {
            var fileInfoHelper = new FileInfo(path);

            var fileInfo = new FileTreeItemModel()
            {
                Name = fileInfoHelper.Name,
                Size = GetSingleFileSize(fileInfoHelper),
                ImageUri = "/Images/file.png"
            };

            return fileInfo;
        }

        /// <summary>
        /// Avoids UnauthorizedAccessException for cases, when the program 
        /// attempts to get system directory path(s) from a system 
        /// drive (C:\\) without granted access.
        /// </summary>
        /// <param name="path">Full directory path.</param>
        /// <returns>Directory path(s) or empty array.</returns>
        private string[] GetDirectoriesWithGrantedAccess(string path)
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
        private string[] GetFilesWithGrantedAcess(string path)
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
    }
}
