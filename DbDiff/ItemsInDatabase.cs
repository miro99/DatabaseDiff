using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbDiff
{
    public abstract class ItemsInDatabase<T> where T : INamed
    {
        protected IEnumerable<T> _AllItems;

        public void InitializeItems(IDataReader dataReader)
        {
            List<T> tables = new List<T>();
            while (dataReader.Read())
            {
                T table = InitializeItemFromReader(dataReader);
                tables.Add(table);
            }
            _AllItems = tables;
        }

        public IEnumerable<T> ListMissingItemNames(ItemsInDatabase<T> itemsInDB2)
        {
            if ((this._AllItems == null) || (itemsInDB2._AllItems == null))
            {
                throw new Exception("ItemsInDatabase class must be initialized before use");
            }

            if (this._AllItems.Count() == 0)
            {
                throw new Exception("Database of record must have tables defined");
            }

            IEnumerable<T> missingTables = GetDiff(_AllItems, itemsInDB2._AllItems);
            return missingTables;
        }

        public static IEnumerable<T> GetDiff(IEnumerable<T> copyOfRecord, IEnumerable<T> itemsToCheckAgainst)
        {
            List<T> missingItems = new List<T>(copyOfRecord.Count());
            foreach (var item in copyOfRecord)
            {
                if (itemsToCheckAgainst.Contains(item) == false)
                {
                    missingItems.Add(item);
                }
            }
            return missingItems;
        }

        protected abstract T InitializeItemFromReader(IDataReader dataReader);
    }
}