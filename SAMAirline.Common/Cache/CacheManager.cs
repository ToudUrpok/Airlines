using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAM.Airline.Common.Cache
{
    public class CacheManager
    {
        public static readonly object Locker = new object();
    }
}