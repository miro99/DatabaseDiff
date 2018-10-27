using System;
using System.Collections.Generic;
using System.Data;

namespace DbDiff
{
    public abstract class ItemsInDatabase<T>
    {
        IEnumerable<T> _AllItems;

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

        protected abstract T InitializeItemFromReader(IDataReader dataReader);
    }
}