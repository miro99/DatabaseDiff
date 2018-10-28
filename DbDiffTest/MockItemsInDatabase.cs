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
        protected override MockINamed InitializeItemFromReader(IDataReader dataReader)
        {
            var mockINamed = new MockINamed
            {
                Name = "Mock INamed"
            };
            return mockINamed;
        }
    }
}
