using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBot
{
    using System;

    [Serializable]
    public class QueryResult
    {
        public QueryResult(bool succeed)
        {
            this.Succeed = succeed;
        }
        public string NewIntent { get; set; }

        public bool Succeed { get; private set; }

    }
}