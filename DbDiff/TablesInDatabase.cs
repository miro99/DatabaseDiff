using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbDiff
{
    public class TablesInDatabase : ItemsInDatabase<Table>
    {
        public IEnumerable<Table> AllItems
        {
            get
            {
                return _AllItems;
            }
        }

        public TablesInDatabase()
        {            
        }

        public IEnumerable<INamed> ListMissingItemNames(TablesInDatabase tablesInDB2, Func<IEnumerable<INamed>,IEnumerable<INamed>, IEnumerable<INamed>> diffFunction)
        {
            if ((this.AllItems == null) || (tablesInDB2.AllItems == null))
            {
                throw new Exception("TablesInDatabase class must be initialized before use");
            }

            if (this.AllItems.Count() == 0)
            {
                throw new Exception("Database of record must have tables defined");
            }

            IEnumerable<INamed> missingTables = diffFunction(this.AllItems, tablesInDB2.AllItems);
            return missingTables;
        }

        protected override Table InitializeItemFromReader(IDataReader dataReader)
        {
            Table table = new Table(dataReader.GetString(0));
            return table;
        }
    }
}
