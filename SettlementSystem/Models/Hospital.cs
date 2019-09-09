using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    public class Hospital
    {
        public string Id { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string Mc { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string Ks { get; set; }

        /// <summary>
        /// 未就诊量
        /// </summary>
        public int Wjzl { get; set; }

        /// <summary>
        /// 取消量
        /// </summary>
        public int Qxl { get; set; }

        /// <summary>
        /// 就诊量
        /// </summary>
        public int Jzl { get; set; }

        /// <summary>
        /// 预约总量
        /// </summary>
        public int Yyzl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Bz { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        public double Ze { get; set; }

        /// <summary>
        /// 显示集合，即该科室要与哪些科室一起显示
        /// </summary>
        public string Xsjh { get; set; }
    }
}
