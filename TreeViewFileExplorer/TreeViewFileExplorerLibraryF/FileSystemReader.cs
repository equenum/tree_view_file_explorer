using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;

namespace TreeViewFileExplorerLibrary
{
    public class FileSystemReader // don't get hidden files and directories
    {
        public List<FileTreeItem> GetRootTreeInfo(string path)
        {
            var result = new List<FileTreeItem>();

            string[] directories = Directory.GetDirectories(path);

            foreach (var directory in directories)
            {
                var subTreeInfo = GetSubTreeInfo(directory);
                result.Add(subTreeInfo);
            }

            var filesInfo = GetFilesInfo(path);
            result.AddRange(filesInfo);

            return result;
        }

        private FileTreeItem GetSubTreeInfo(string path) // name?
        {
            var result = new FileTreeItem();

            var subDirectories = GetDirectoriesInfo(path);
            var files = GetFilesInfo(path);

            result.SubTrees.AddRange(subDirectories);
            result.SubTrees.AddRange(files);
            /*
            if (subDirectories.Count > 0)
            {
                result.SubTrees.AddRange(subDirectories);
            }
            //result.SubTrees.AddRange(subDirectories);

            if (files.Count > 0)
            {
                result.SubTrees.AddRange(files);
            }
            //result.SubTrees.AddRange(files);
            */
            result.Name = Path.GetFileName(path);

            result.Size = result.SubTrees.Sum(x => x.Size);

            return result;
        }

        private List<FileTreeItem> GetDirectoriesInfo(string path)
        {
            var result = new List<FileTreeItem>();
            var directories = GetDirectoriesWithAllowedAccess(path);

            foreach (var directory in directories)
            {
                var subTreeInfo = GetSubTreeInfo(directory);
                result.Add(subTreeInfo);
            }

            return result;
        }

        private string[] GetDirectoriesWithAllowedAccess(string path)
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

        private string[] GetFilesWithAllowedAcess(string path)
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

        private FileTreeItem GetFileInfo(string path) // think about the name
        {
            var fileInfo = new FileInfo(path);

            var file = new FileTreeItem()
            {
                Name = fileInfo.Name,
                Size = FileSize(fileInfo) // does it work?

            };

            return file;
        }

        private List<FileTreeItem> GetFilesInfo(string path)
        {
            var result = new List<FileTreeItem>();
            var files = GetFilesWithAllowedAcess(path);

            foreach (var file in files)
            {
                var fileInfo = GetFileInfo(file);
                result.Add(fileInfo);
            }

            return result;
        }

        private long FileSize(FileInfo info) // size not null
        {
            try
            {
                return info.Length;
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}
