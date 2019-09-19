using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    public class Rules
    {
        public string Id { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string Mc { get; set; }

        /// <summary>
        /// 医院科室名称
        /// </summary>
        public string Ks { get; set; }

        /// <summary>
        /// 计算类型，目前只有普通和阶梯两种
        /// </summary>
        public string Jslx { get; set; }

        /// <summary>
        /// 未就诊可以获得的钱
        /// </summary>
        public double Wjzje { get; set; }

        /// <summary>
        /// 取消获得的钱
        /// </summary>
        public double Qxje { get; set; }

        /// <summary>
        /// 预约渠道(114、快医等)
        /// </summary>
        public string Qd { get; set; }

        /// <summary>
        /// 到诊获得的钱
        /// </summary>
        public double Dzje { get; set; }
    }
}
