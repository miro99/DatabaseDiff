﻿using DbDiff;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbDiffTest
{
    [TestFixture]
    class TablesInDatabaseTest
    {         
        [Test]
        public void TablesInDatabaseTest_GetTables_Returns_Tables_In_Database()
        {
            //Arrange
            var mockDataReader = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle = true;
            mockDataReader
                .Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => readToggle = false);

            mockDataReader.Setup(x => x.GetString(0))
                .Returns("table1");

            TablesInDatabase tablesInDatabase = new TablesInDatabase(mockDataReader.Object);

            //Act
            IEnumerable<Table> tables = tablesInDatabase.GetAllTables();

            //Assert
            Assert.IsTrue(tables.SequenceEqual(new List<Table> { new Table("table1") }));
        }
    }
}
