using DbDiff;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiffTest
{
    [TestFixture]
    class TablesInDatabaseTest
    {
        [Test]
        public void TablesInDatabaseTest_Constructor_DbConnection_Parameter_Cannot_Be_Null()
        {
            DbConnection dbConnection = null;
            TablesInDatabase tablesInDatabase;
            Assert.Throws<ArgumentException>(() => tablesInDatabase = new TablesInDatabase(dbConnection));
        }

        [Test]
        public void TablesInDatabaseTest_Constructor_Does_Not_Throw_ArgumentException_With_Valid_Connection()
        {
            IDbConnection mockConnection = Mock.Of<IDbConnection>();
            TablesInDatabase tablesInDatabase = new TablesInDatabase(mockConnection);
        }

        [Test]
        public void TablesInDatabaseTest_GetTables_Returns_Tables_In_Database()
        {
            //Arrange
            IDbConnection mockConnection = Mock.Of<IDbConnection>();
            TablesInDatabase tablesInDatabase = new TablesInDatabase(mockConnection);

            //Act
            IEnumerable<Table> tables = tablesInDatabase.GetAllTables();

            //Assert
            Assert.IsTrue(tables.SequenceEqual(new List<Table> { new Table("table1") }));
        }
    }
}
