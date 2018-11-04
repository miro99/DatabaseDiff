using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDiff;

namespace DbDiffTest
{
    public class MockINamed : INamed
    {
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MockINamed))
            {
                return false;
            }
            return this.Name.Equals(((MockINamed)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
