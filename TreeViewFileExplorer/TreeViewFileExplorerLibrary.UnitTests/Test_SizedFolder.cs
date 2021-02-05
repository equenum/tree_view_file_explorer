using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewFileExplorerLibrary.Models;
using Xunit;

namespace TreeViewFileExplorerLibrary.UnitTests
{
    public class Test_SizedFolder
    {
        [Theory]
        [InlineData(@"TestData", "(30 KB)")]
        [InlineData(@"TestData\TestFolder1", "(3 KB)")]
        public async void SetSizeUnits_SetsAppropriateFolderSizeUnit(string testDataPath, string expectedSizeForShow)
        {
            // Act
            var fileSystemReader = new FileSystemReader();
            fileSystemReader.ClearFilePaths();
            fileSystemReader.AddFilePath(testDataPath);

            var fileSystem = await fileSystemReader.GetFileSystemTreeAsync();

            SizedFolder targetFolder = (SizedFolder)fileSystem[0];
            targetFolder.CalculateSize();
            targetFolder.SetSizeUnits();

            // Assert
            Assert.Equal(targetFolder.SizeForShow, expectedSizeForShow);
        }
    }
}
