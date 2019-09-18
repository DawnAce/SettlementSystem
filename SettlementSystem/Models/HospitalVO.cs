using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    public class HospitalVO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        [JsonProperty("mc")]
        public string Mc { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [JsonProperty("ks")]
        public string Ks { get; set; }

        /// <summary>
        /// 未就诊量
        /// </summary>
        [JsonProperty("wjzl")]
        public int Wjzl { get; set; }

        /// <summary>
        /// 取消量
        /// </summary>
        [JsonProperty("qxl")]
        public int Qxl { get; set; }

        /// <summary>
        /// 就诊量
        /// </summary>
        [JsonProperty("jzl")]
        public int Jzl { get; set; }

        /// <summary>
        /// 预约总量
        /// </summary>
        [JsonProperty("yyzl")]
        public int Yyzl { get; set; }

        /// <summary>
        /// 预约渠道(114、快医等)
        /// </summary>
        [JsonProperty("qd")]
        public string Qd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("bz")]
        public string Bz { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        [JsonProperty("ze")]
        public double Ze { get; set; }

        /// <summary>
        /// 显示集合，即该科室要与哪些科室一起显示
        /// </summary>
        [JsonProperty("xsjh")]
        public string Xsjh { get; set; }
    }
}
