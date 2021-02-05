using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;
using Xunit;

namespace TreeViewFileExplorerLibrary.UnitTests
{
    public class Test_FileSystemReader
    {
        [Fact]
        public async void GetFileSystemTreeAsync_GetsFileSystemTreeInfo()
        {
            // Arrange

            string testDataPath = @"TestData";

            // File system root folder
            string fileSystemRootFolderName = @"TestData";
            decimal fileSystemRootFolderSize = 30388;
            int fileSystemRootFolderSubTreesCount = 4;

            // File system level 1 child folder
            string level1ChildFolderName = "TestFolder1";
            decimal level1ChildFolderSize = 3274;
            int level1ChildFolderSubTreesCount = 1;

            // File system level 2 child file
            string level2ChildFileName = "TestFile1.txt";
            decimal level2ChildFileSize = 3274;

            // Act

            var fileSystemReader = new FileSystemReader();
            fileSystemReader.ClearFilePaths();
            fileSystemReader.AddFilePath(testDataPath);
            
            var fileSystem = await fileSystemReader.GetFileSystemTreeAsync();
            SizedFolder level1Folder = (SizedFolder)fileSystem[0].ChildTreeItems[0];
            SizedFile level2File = (SizedFile)level1Folder.ChildTreeItems[0];

            // Assert

            // File system root folder
            Assert.Equal(fileSystem[0].Name, fileSystemRootFolderName);
            Assert.Equal(fileSystem[0].Size, fileSystemRootFolderSize);
            Assert.Equal(fileSystem[0].ChildTreeItems.Count, fileSystemRootFolderSubTreesCount);

            // File system level 1 child folder
            Assert.Equal(level1Folder.Name, level1ChildFolderName);
            Assert.Equal(level1Folder.Size, level1ChildFolderSize);
            Assert.Equal(level1Folder.ChildTreeItems.Count, level1ChildFolderSubTreesCount);

            // File system level 2 child file
            Assert.Equal(level2File.Name, level2ChildFileName);
            Assert.Equal(level2File.Size, level2ChildFileSize);
        }
    }
}
