using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    /// <summary>
    /// 预约信息
    /// </summary>
    [SugarTable("Yyxx")]
    public class Yyxx
    {
        /// <summary>
        /// 如果只是主键只能加一个　
        /// 如果是主键并且是自增列还需要加上IsIdentity=true
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 分院名称
        /// </summary>
        [JsonProperty("fymc")]
        public string Fymc { get; set; }

        /// <summary>
        /// 总院名称
        /// </summary>
        [JsonProperty("zymc")]
        public string Zymc { get; set; }

        /// <summary>
        /// 挂号人姓名
        /// </summary>
        [JsonProperty("ghr")]
        public string Ghr { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        [JsonProperty("zjh")]
        public string Zjh { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("sjh")]
        public string Sjh { get; set; }

        /// <summary>
        /// 识别码
        /// </summary>
        [JsonProperty("sbm")]
        public string Sbm { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        [JsonProperty("jzrq")]
        public string Jzrq { get; set; }

        /// <summary>
        /// 时段
        /// </summary>
        [JsonProperty("sd")]
        public string Sd { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [JsonProperty("ks")]
        public string Ks { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [JsonProperty("ys")]
        public string Ys { get; set; }

        /// <summary>
        /// 医生职称
        /// </summary>
        [JsonProperty("zc")]
        public string Zc { get; set; }

        /// <summary>
        /// 挂号费
        /// </summary>
        [JsonProperty("ghf")]
        public int Ghf { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string Yhqmc { get; set; }

        /// <summary>
        /// 优惠价格
        /// </summary>
        public int Yhjg { get; set; }

        /// <summary>
        /// 约成时间
        /// </summary>
        public string Ycsj { get; set; }

        /// <summary>
        /// 预约信息备注
        /// </summary>
        public string Yyxxbz { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [JsonProperty("ddzt")]
        public string Ddzt { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        [JsonProperty("zfzt")]
        public string Zfzt { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [JsonProperty("qxyy")]
        public string Qxyy { get; set; }

        /// <summary>
        /// 坐席工号
        /// </summary>
        public string Zxgg { get; set; }

        /// <summary>
        /// 坐席职场
        /// </summary>
        public string Zxzc { get; set; }

        /// <summary>
        /// 预约渠道
        /// </summary>
        [JsonProperty("yyqd")]
        public string Yyqd { get; set; }

        /// <summary>
        /// 结算渠道
        /// </summary>
        [JsonProperty("jsqd")]
        public string Jsqd { get; set; }

        /// <summary>
        /// 第三方订单
        /// </summary>
        public string Dsfdd { get; set; }

        /// <summary>
        /// 是否到诊
        /// </summary>
        [JsonProperty("sfdz")]
        public bool Sfdz { get; set; }

        /// <summary>
        /// 是否缴费
        /// </summary>
        public bool Sfjf { get; set; }

        /// <summary>
        /// 未消费原因
        /// </summary>
        public string Wxfyy { get; set; }

        /// <summary>
        /// 到诊人姓名
        /// </summary>
        public string Dzrxm { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string Blh { get; set; }

        /// <summary>
        /// 到诊日期时间
        /// </summary>
        public string Dzrqsj { get; set; }

        /// <summary>
        /// 预约咨询项目
        /// </summary>
        public string Yyzxxm { get; set; }

        /// <summary>
        /// 最终治疗项目
        /// </summary>
        public string Zzzlxm { get; set; }

        /// <summary>
        /// 是否接通
        /// </summary>
        public bool Sfjt { get; set; }

        /// <summary>
        /// 是否就诊
        /// </summary>
        public bool Sfjz { get; set; }

        /// <summary>
        /// 消费详情
        /// </summary>
        public string Xfxq { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string Qt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Bz { get; set; }

        /// <summary>
        /// 用来快速查找病人属于哪个科室（即科室ID)
        /// </summary>
        public string Ksczid { get; set; }
  }
}
