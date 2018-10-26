using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiff
{
    public abstract class TablesInDatabase
    {
        private IDbConnection dbConnection;

        public TablesInDatabase(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentException("connection parameter cannot be null");
            }
            dbConnection = connection;
        }

        public abstract IEnumerable<Table> GetAllTables();        
    }
}
