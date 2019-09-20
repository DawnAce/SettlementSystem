using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.PO
{
    [SugarTable("RateRules")]
    public class RateRulesPO
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string KsId { get; set; }

        public string No { get; set; }

        public string Deno { get; set; }

        public string Mole { get; set; }

        public double Start { get; set; }

        public double End { get; set; }

        public double Fee { get; set; }

        public string FeeType { get; set; }

        public string Pre { get; set; }

        public string Next { get; set; }

        public bool IsSet { get; set; }
    }
}
