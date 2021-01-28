using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public List<FileTreeItemModel> GetTreeInfo(string path)
        {
            var result = new List<FileTreeItemModel>();

            string[] rootDirectories = Directory.GetDirectories(path);

            foreach (var directory in rootDirectories)
            {
                var childTreeInfo = GetChildTreeInfo(directory);
                result.Add(childTreeInfo);
            }

            var filesInfo = GetFilesInfo(path);
            result.AddRange(filesInfo);

            return result;
        }

        private FileTreeItemModel GetChildTreeInfo(string path)
        {
            var result = new FileTreeItemModel();

            var childDirectories = GetChildDirectoriesInfo(path);
            var files = GetFilesInfo(path);

            result.SubTrees.AddRange(childDirectories);
            result.SubTrees.AddRange(files);
            result.Name = Path.GetFileName(path);
            result.Size = result.SubTrees.Sum(x => x.Size);
            result.ImageUri = "/Images/folder.png";

            return result;
        }

        private List<FileTreeItemModel> GetChildDirectoriesInfo(string path)
        {
            var result = new List<FileTreeItemModel>();

            var directories = GetDirectoriesWithGrantedAccess(path);

            foreach (var directory in directories)
            {
                var childTreeInfo = GetChildTreeInfo(directory);
                result.Add(childTreeInfo);
            }

            return result;
        }

        private List<FileTreeItemModel> GetFilesInfo(string path)
        {
            var result = new List<FileTreeItemModel>();

            var files = GetFilesWithGrantedAcess(path);

            foreach (var file in files)
            {
                var fileInfo = GetSingleFileInfo(file);
                result.Add(fileInfo);
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
