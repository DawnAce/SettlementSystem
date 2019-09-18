using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    [SugarTable("Organization")]
    public class OrganizationPO
    {
        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
