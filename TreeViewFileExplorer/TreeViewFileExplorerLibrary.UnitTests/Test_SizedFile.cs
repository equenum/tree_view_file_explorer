using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;
using Xunit;

namespace TreeViewFileExplorerLibrary.UnitTests
{
    public class Test_SizedFile
    {
        [Fact]
        public async void SetSizeUnits_SetsAppropriateFileSizeUnit()
        {
            // Arrange
            string testDataPath = @"TestData";
            string expectedFileSizeForShow = "(3 KB)";

            // Act
            var fileSystemReader = new FileSystemReader();
            fileSystemReader.ClearFilePaths();
            fileSystemReader.AddFilePath(testDataPath);

            var fileSystem = await fileSystemReader.GetFileSystemTreeAsync();

            SizedFolder targetFolder = (SizedFolder)fileSystem[0].ChildTreeItems[0];
            SizedFile actualFile = (SizedFile)targetFolder.ChildTreeItems[0];
            actualFile.SetSizeUnits();

            // Assert
            Assert.Equal(actualFile.SizeForShow, expectedFileSizeForShow);
        }
    }
}
