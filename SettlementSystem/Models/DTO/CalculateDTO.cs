using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.DTO
{
    public class CalculateDTO
    {
        public string[] KsIds { get; set; }
        public string[] FeeTypes { get; set; }
        public string[] Dates { get; set; }
    }
}
