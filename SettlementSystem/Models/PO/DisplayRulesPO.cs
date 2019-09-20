using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.PO
{
    [SugarTable("DisplayRules")]
    public class DisplayRulesPO
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string Pre { get; set; }

        public string Next { get; set; }

        public bool IsDisplay { get; set; }
    }
}
