using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunTimeRecords_CSDNF;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            TimeSpan timeSpan = DateTime.Now - DateTime.Now.Date;
            string expected = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);

            // Act
            string actual = Utilities.TimeFormatString(timeSpan);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
