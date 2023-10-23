using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunTimeRecords_CSDNF;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;

namespace UnitTests
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void TimeFormatStringTest()
        {
            // Arrange
            TimeSpan timeSpan = new TimeSpan(02, 04, 06); // 02:04:06
            string expected = "02:04:06";

            // Act
            string actual = Utilities.TimeFormatString(timeSpan);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDirectoryTest()
        {
            // Arrange
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\demo\test.txt", new MockFileData("test") }
            });
            string targetDirectory = @".\save";

            // Act
            Utilities.CreateDirectory(targetDirectory, fileSystem);

            // Assert
            Assert.AreEqual(true, fileSystem.Directory.Exists(targetDirectory));
        }


    }
}
