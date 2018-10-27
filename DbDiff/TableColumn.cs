using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiff
{
    class TableColumn : INamed
    {
        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                _Name = value;
            }
        }

        public TableColumn(string name)
        {
            Name = name;
        }
    }
}
