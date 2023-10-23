using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunTimeRecords_CSDNF;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ListFileDaoTest
    {
        [TestMethod]
        public void LoadListFileTest_ShouldReadFileContent()
        {
            // Arrange Data
            string filePath = @"C:\master\listFile.txt";
            List<string> data = new List<string>
            {
                "sample contet",
                "list data"
            };

            // Arrange File System
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { filePath, new MockFileData("") }
            });
            fileSystem.File.WriteAllLines(filePath, data);
            // Arrange Dao
            ListFileDao listFileDao = new ListFileDao(fileSystem);

            // Act
            ListFileDto actual = listFileDao.LoadListFile(filePath);

            // Assert
            Assert.AreEqual(filePath, actual.FilePath);
            CollectionAssert.AreEqual(data, actual.DataList);
        }

        [TestMethod]
        public void SaveListFileTest_ShoulWriteFileContent()
        {
            // Arrange Data
            string filePath = @"C:\master\listFile.txt";
            List<string> data = new List<string>
            {
                "sample contet",
                "list data"
            };

            // Arrange File System
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { filePath, new MockFileData("") }
            });

            // Arrange Dto
            ListFileDto listFileDto = new ListFileDto
            {
                FilePath = filePath,
                DataList = data
            };

            // Act
            ListFileDao listFileDao = new ListFileDao(fileSystem);
            listFileDao.SaveListFile(listFileDto);
            List<string> actual = fileSystem.File.ReadAllLines(filePath).ToList();

            // Assert
            CollectionAssert.AreEqual(data, actual);
        }
    }
}
