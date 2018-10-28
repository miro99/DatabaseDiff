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
            throw new NotImplementedException();
        }
    }
}
