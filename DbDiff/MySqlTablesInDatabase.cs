using System.Collections.Generic;
using System.Data;

namespace DbDiff
{
    public class MySqlTablesInDatabase : TablesInDatabase
    {
        public MySqlTablesInDatabase(IDbConnection connection) : base(connection)
        {
        }

        public override IEnumerable<Table> GetAllTables()
        {
            throw new System.NotImplementedException();
        }
    }
}