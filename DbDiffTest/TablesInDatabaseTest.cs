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
