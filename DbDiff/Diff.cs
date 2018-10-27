using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDiff
{
    public class Diff
    {
        public static IEnumerable<INamed> GetDiff(IEnumerable<INamed> copyOfRecord, IEnumerable<INamed> itemsToCheckAgainst)
        {
            List<INamed> missingItems = new List<INamed>(copyOfRecord.Count());
            foreach (var item in copyOfRecord)
            {
                if (itemsToCheckAgainst.Contains(item) == false)
                {
                    missingItems.Add(item);
                }
            }
            return missingItems;
        }
    }
}
