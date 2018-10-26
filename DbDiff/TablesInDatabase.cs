using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiff
{
    public abstract class TablesInDatabase<T,C> : IDisposable where T : IDbCommand where C : IDbConnection
    {
        private IDbConnection dbConnection;

        public TablesInDatabase(C connection)
        {
            if (connection == null)
            {
                throw new ArgumentException("connection parameter cannot be null");
            }
            dbConnection = connection;
        }

        public void Dispose()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
        }

        public abstract IEnumerable<Table> GetAllTables(T getAllTablesCommand);        
    }
}
