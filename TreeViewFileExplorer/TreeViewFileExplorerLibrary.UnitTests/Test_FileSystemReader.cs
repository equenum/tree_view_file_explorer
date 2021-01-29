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
        public async void GetTreeInfoAsync_GetsTreeInfo()
        {
            // Arrange
            string testDataPath = @"TestData";

            // File system tree item
            string fileSystemTreeItemName = @"TestData";
            decimal fileSystemTreeItemSize = 30388;
            int fileSystemTreeItemSubTreesCount = 4;
            string fileSystemTreeItemImageUri = null;

            // Level 1 child tree item
            string level1TreeItemName = "TestFolder1";
            decimal level1TreeItemSize = 3274;
            int level1TreeItemSubTreesCount = 1;
            string level1TreeItemImageUri = "/Images/folder.png";

            // Level 2 child tree item
            string level2TreeItemName = "TestFile1.txt";
            decimal level2TreeItemSize = 3274;
            int level2TreeItemSubTreesCount = 0;
            string level2TreeItemImageUri = "/Images/file.png";

            // Act
            var fileReader = new FileSystemReader();
            var fileInfoHelper = new FileInfo(testDataPath);

            var fileSystem = new FileTreeItemModel
            {
                Name = fileInfoHelper.Name,
                SubTrees = await fileReader.GetTreeInfoAsync(testDataPath)
            };

            fileSystem.Size = fileSystem.SubTrees.Sum(x => x.Size);

            // Assert

            // File system tree item
            Assert.Equal(fileSystem.Name, fileSystemTreeItemName);
            Assert.Equal(fileSystem.Size, fileSystemTreeItemSize);
            Assert.Equal(fileSystem.SubTrees.Count, fileSystemTreeItemSubTreesCount);
            Assert.Equal(fileSystem.ImageUri, fileSystemTreeItemImageUri);

            // Level 1 child tree item
            Assert.Equal(fileSystem.SubTrees[0].Name, level1TreeItemName);
            Assert.Equal(fileSystem.SubTrees[0].Size, level1TreeItemSize);
            Assert.Equal(fileSystem.SubTrees[0].SubTrees.Count, level1TreeItemSubTreesCount);
            Assert.Equal(fileSystem.SubTrees[0].ImageUri, level1TreeItemImageUri);

            // Level 2 child tree item
            Assert.Equal(fileSystem.SubTrees[0].SubTrees[0].Name, level2TreeItemName);
            Assert.Equal(fileSystem.SubTrees[0].SubTrees[0].Size, level2TreeItemSize);
            Assert.Equal(fileSystem.SubTrees[0].SubTrees[0].SubTrees.Count, level2TreeItemSubTreesCount);
            Assert.Equal(fileSystem.SubTrees[0].SubTrees[0].ImageUri, level2TreeItemImageUri);
        }
    }
}
