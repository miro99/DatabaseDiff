using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbDiff
{
    public class TablesInDatabase : ItemsInDatabase<Table>
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
                Table table = InitializeItemFromReader(dataReader);
                tables.Add(table);
            }
            _AllTables = tables;            
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

        protected override Table InitializeItemFromReader(IDataReader dataReader)
        {
            Table table = new Table(dataReader.GetString(0));
            return table;
        }
    }
}
