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

        protected override Table InitializeItemFromReader(IDataReader dataReader)
        {
            Table table = new Table(dataReader.GetString(0));
            return table;
        }
    }
}
