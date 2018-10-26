using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace DbDiff
{
    public class MySqlTablesInDatabase : TablesInDatabase<MySqlCommand, MySqlConnection>
    {
        IEnumerable<Table> AllTables;

        public MySqlTablesInDatabase(MySqlConnection connection) : base(connection)
        {
        }

        public override IEnumerable<Table> GetAllTables(MySqlCommand getAllTablesCommand)
        {
            
            throw new System.NotImplementedException();
        }
    }
}