using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forganizer.DomainModel.Extensions
{
    public static class EKeyValuePairStringInt
    {
        public static decimal TagSize(this KeyValuePair<string, int> entry, int total, decimal min, decimal max)
        {
            return (((decimal)entry.Value / (decimal)total) * (min + max) / 2);
        }
    }
}
