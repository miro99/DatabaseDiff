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
        private TablesInDatabase ArrangeTablesInDatabaseObject(string tableName)
        {
            TablesInDatabase tablesInDatabase = new TablesInDatabase();
            var mockDataReader = new Mock<IDataReader>(MockBehavior.Strict);
            bool readToggle = true;
            if (tableName.Equals(string.Empty) == true)
            {
                readToggle = false;
            }
            mockDataReader
                .Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => readToggle = false);

            mockDataReader
                .Setup(x => x.GetString(0))
                .Returns(tableName);

            tablesInDatabase.InitializeItems(mockDataReader.Object);

            return tablesInDatabase;
        }

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
            tablesInDatabase.InitializeItems(mockDataReader.Object);

            //Assert
            Assert.IsTrue(tablesInDatabase.AllItems.SequenceEqual(new List<Table> { new Table("table1") }));            
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Returns_Table_Names_That_Are_In_The_Calling_Object_And_Not_In_The_Ojbect_Passed_In()
        {
            //Arrange           
            TablesInDatabase tablesInDB1 = ArrangeTablesInDatabaseObject("table1");
            TablesInDatabase tablesInDB2 = ArrangeTablesInDatabaseObject("table2");

            //Act
            IEnumerable<INamed> missingTableNames = tablesInDB1.ListMissingItemNames(tablesInDB2);

            //Assert
            Assert.IsTrue(missingTableNames.SequenceEqual(new List<INamed> { new Table("table1") }));
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Throws_Exception_When_Caller_Is_Not_Initialzied()
        {
            //Arrange
            TablesInDatabase tablesInDatabase1 = new TablesInDatabase();
            TablesInDatabase tablesInDatabase2 = new TablesInDatabase();

            //ActAndAssert
            Assert.Throws<Exception>(() => tablesInDatabase1.ListMissingItemNames(tablesInDatabase2));            
        }

        [Test]
        public void TablesInDatabaseTest_ListMissingTables_Method_Throws_Exception_When_Caller_Has_No_Tables()
        {
            //Arrange            
            TablesInDatabase tablesInDB1 = ArrangeTablesInDatabaseObject("");
            TablesInDatabase tablesInDB2 = ArrangeTablesInDatabaseObject("table2");
            
            Assert.Throws<Exception>(() => tablesInDB1.ListMissingItemNames(tablesInDB2));
        }
    }
}
