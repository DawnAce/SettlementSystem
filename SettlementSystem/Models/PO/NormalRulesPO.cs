using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.PO
{
    [SugarTable("NormalRules")]
    public class NormalRulesPO
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public double Fee { get; set; }

        public string FeeType { get; set; }
    }
}
