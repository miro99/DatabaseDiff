using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDiff;

namespace DbDiffTest
{
    public class MockItemsInDatabase : ItemsInDatabase<MockINamed>
    {
        public IEnumerable<MockINamed> AllItems
        {
            get
            {
                return this._AllItems;
            }
        }

        protected override MockINamed InitializeItemFromReader(IDataReader dataReader)
        {
            var mockINamed = new MockINamed
            {
                Name = dataReader.GetString(0)
            };
            return mockINamed;
        }
    }
}
