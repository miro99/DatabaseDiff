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
        IEnumerable<Table> _AllTables;
        public IEnumerable<Table> AllTables
        {
            get
            {
                return _AllTables;
            }
        }

        public TablesInDatabase()
        {            
        }

        public void InitializeTables(IDataReader dataReader)
        {
            List<Table> tables = new List<Table>();
            while (dataReader.Read())
            {
                Table table = InitializeTableFromReader(dataReader);
                tables.Add(table);
            }
            _AllTables = tables;            
        }

        private Table InitializeTableFromReader(IDataReader dataReader)
        {
            Table table = new Table(dataReader.GetString(0));
            return table;
        }

        public IEnumerable<INamed> ListMissingTableNames(TablesInDatabase tablesInDB2, Func<IEnumerable<INamed>,IEnumerable<INamed>, IEnumerable<INamed>> diffFunction)
        {
            if ((this.AllTables == null) || (tablesInDB2.AllTables == null))
            {
                throw new Exception("TablesInDatabase class must be initialized before use");
            }

            if (this.AllTables.Count() == 0)
            {
                throw new Exception("Database of record must have tables defined");
            }

            IEnumerable<INamed> missingTables = diffFunction(this.AllTables, tablesInDB2.AllTables);
            return missingTables;
        }       
    }
}
