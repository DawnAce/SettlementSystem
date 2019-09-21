using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models.PO
{
    [SugarTable("SomeValues")]
    public class SomeValues
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string Content { get; set; }

        public string Comments { get; set; }
    }
}
