using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.PostType
{
    public class QueryDTO
    {
        public string[] KsIds { get; set; }

        public string[] Dates { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool NeedTotalCount { get; set; }
    }
}
