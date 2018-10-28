﻿using Moq;
using NUnit.Framework;
using System.Data;
using System.Linq;

namespace DbDiffTest
{
    [TestFixture]
    class ItemsInDatabaseTest
    {
        [Test]
        public void InitializeItems_Fills_AllItems()
        {
            //Arrange
            var mockItemsInDatabase = new MockItemsInDatabase();
            var mockDataReader = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle = true;

            mockDataReader.Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => { readToggle = false; });

            mockDataReader.Setup(x => x.GetString(0))
                .Returns("INamed Test");
            //Act
            mockItemsInDatabase.InitializeItems(mockDataReader.Object);
            //Assert
            Assert.IsTrue(mockItemsInDatabase.AllItems.Count() == 1);
        }
    }
}
