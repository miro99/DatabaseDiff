using DbDiff;
using Moq;
using NUnit.Framework;
using System;
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
                .Callback(() => {
                        readToggle = false; 
                });

            mockDataReader.Setup(x => x.GetString(0))
                .Returns("table1");

            TablesInDatabase tablesInDatabase = new TablesInDatabase();

            //Act
            tablesInDatabase.InitializeTables(mockDataReader.Object);

            //Assert
            Assert.IsTrue(tablesInDatabase.AllTables.SequenceEqual(new List<Table> { new Table("table1") }));            
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Returns_Table_Names_That_Are_In_The_Calling_Object_And_Not_In_The_Ojbect_Passed_In()
        {
            //Arrange
            TablesInDatabase tablesInDB1 = new TablesInDatabase();
            TablesInDatabase tablesInDB2 = new TablesInDatabase();

            var mockDataReaderDB1 = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle = true;

            mockDataReaderDB1
                .Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => {
                    readToggle = false;
                });

            mockDataReaderDB1.Setup(x => x.GetString(0))
                .Returns("table1");

            var mockDataReaderDB2 = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle2 = true;
            mockDataReaderDB2.Setup(x => x.Read())
                .Returns(() => readToggle2)
                .Callback(() => readToggle2 = false);

            mockDataReaderDB2.Setup(x => x.GetString(0))
                .Returns("table2");

            tablesInDB1.InitializeTables(mockDataReaderDB1.Object);
            tablesInDB2.InitializeTables(mockDataReaderDB2.Object);

            //Act
            IEnumerable<Table> missingTables = tablesInDB1.ListMissingTables(tablesInDB2);

            //Assert
            Assert.IsTrue(tablesInDB1.AllTables.SequenceEqual(new List<Table> { new Table("table1") }));
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Throws_Exception_When_Caller_Is_Not_Initialzied()
        {
            //Arrange
            TablesInDatabase tablesInDatabase1 = new TablesInDatabase();
            TablesInDatabase tablesInDatabase2 = new TablesInDatabase();

            //Act
            Assert.Throws<Exception>(() => tablesInDatabase1.ListMissingTables(tablesInDatabase2));

            //Assert
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Throws_Exception_When_Caller_Has_No_Tables()
        {
            //Arrange
            TablesInDatabase tablesInDB1 = new TablesInDatabase();
            TablesInDatabase tablesInDB2 = new TablesInDatabase();

            var mockDataReaderDB1 = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle = false;

            mockDataReaderDB1
                .Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => {
                    readToggle = false;
                });

            mockDataReaderDB1.Setup(x => x.GetString(0))
                .Returns("table1");

            var mockDataReaderDB2 = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle2 = true;
            mockDataReaderDB2.Setup(x => x.Read())
                .Returns(() => readToggle2)
                .Callback(() => readToggle2 = false);

            mockDataReaderDB2.Setup(x => x.GetString(0))
                .Returns("table2");

            tablesInDB1.InitializeTables(mockDataReaderDB1.Object);
            tablesInDB2.InitializeTables(mockDataReaderDB2.Object);

            Assert.Throws<Exception>(() => tablesInDB1.ListMissingTables(tablesInDB2));
        }
    }
}
