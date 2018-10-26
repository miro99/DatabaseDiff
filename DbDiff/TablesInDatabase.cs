using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiff
{
    public class TablesInDatabase
    {
        private readonly IDataReader DataReader;

        public TablesInDatabase(IDataReader dbReader)
        {
            DataReader = dbReader;            
        }

        public IEnumerable<Table> GetAllTables()
        {
            List<Table> tables = new List<Table>();
            while (DataReader.Read())
            {
                Table table = InitializeTableFromReader(DataReader);
                tables.Add(table);
            }
            return tables;
        }

        private Table InitializeTableFromReader(IDataReader dataReader)
        {
            Table table = new Table(dataReader.GetString(0));
            return table;
        }
    }
}
