using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using RestSharp;
using SettlementSystem.Dao;
using SettlementSystem.Models;
using SettlementSystem.Models.PO;
using SettlementSystem.Models.Temp;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Service
{
    public class CalculateService
    {
        public static MyResponse CalculateFee(string[] ksIds, string[] feeTypes, string[] dates)
        {
            var db = SugarDao.GetInstance();
            var orgMap = new Dictionary<string, JsonObject>();
            db.Queryable<OrganizationPO>()
                .ToList()
                .ForEach(x => {
                    orgMap[x.Id] = new JsonObject { ["Name"] = x.Name };
                    if (x.Type != 3)
                        orgMap[x.Id]["Children"] = new List<string>();
                    //将自己记录到父节点，防止出现本月没人挂号，不显示统计的问题
                    if (x.Type != 1)
                        ((List<string>)(orgMap[x.Id.Substring(0, (x.Type - 1) * 3)]["Children"])).Add(x.Id);
                });

            var ksSet = new HashSet<string>(ksIds);
            var dateSet = new HashSet<string>(dates);
            var typeSet = new HashSet<string>(feeTypes);
            var temp = new List<TempCalculateYyxx>();
            db.Queryable<YyxxPO>()
                .Where(x => dateSet.Contains(x.Id.Substring(0, 6)))
                .Where(x => ksSet.Contains(x.Ksczid))
                .Where(x => typeSet.Contains(x.Jsqd))
                .GroupBy(x => x.Ddzt).GroupBy(x => x.Ksczid).GroupBy(x => x.Jsqd)
                .Select(x => new { ksczid = x.Ksczid, ddzt = x.Ddzt, jsqd = x.Jsqd, count = SqlFunc.AggregateCount(1) })
                .ToList()
                .ForEach(x => temp.Add(
                    new TempCalculateYyxx {
                        Ksczid = x.ksczid, Ddzt = ParseDdzt(x.ddzt), Jsqd = x.jsqd, Count = x.count
                    })
                );

            var ruleMap = new Dictionary<string, NormalRulesPO>();
            db.Queryable<NormalRulesPO>()
                .Where(x => ksSet.Contains(x.Id.Substring(0, 9)))
                .ToList()
                .ForEach(x => ruleMap[x.Id] = x);

            var resultList = new List<HospitalVO>();
            var resultMap = new Dictionary<string, HospitalVO>();
            foreach(var obj in temp)
            {
                var zyId = obj.Ksczid.Substring(0, 3);
                var fyId = obj.Ksczid.Substring(0, 6);
                HospitalVO zyObj, fyObj;
                if (!resultMap.ContainsKey(zyId))
                {
                    resultMap[zyId] = new HospitalVO { Id = zyId, Zymc = (string)orgMap[zyId]["Name"]};
                    resultList.Add(resultMap[zyId]);
                }
                zyObj = resultMap[zyId];

                if (!resultMap.ContainsKey(fyId))
                {
                    resultMap[fyId] = new HospitalVO { Id = fyId, Fymc = (string)orgMap[fyId]["Name"]};
                    if (zyObj.Children == null)
                        zyObj.Children = new List<HospitalVO>();
                    zyObj.Children.Add(resultMap[fyId]);
                    //如果组织结构中有科室，先创建所有科室，防止没人挂号，导致不显示的问题
                    if(((List<string>)(orgMap[fyId]["Children"])).Count != 0)
                    {
                        resultMap[fyId].Children = new List<HospitalVO>();
                        ((List<string>)(orgMap[fyId]["Children"]))
                            .ForEach(x => {
                                var tempKsObj = new HospitalVO { Id = x, Ks = (string)(orgMap[x]["Name"])};
                                resultMap[fyId].Children.Add(tempKsObj);
                                resultMap[x] = tempKsObj;
                            });
                    }
                }
                    
                fyObj = resultMap[fyId];
                var fee = ruleMap.ContainsKey(obj.Ksczid + obj.Ddzt) ? ruleMap[obj.Ksczid + obj.Ddzt].Fee : 0;
                var ksObj = resultMap[obj.Ksczid];
                if (String.IsNullOrEmpty(ksObj.Qd))
                    ksObj.Qd = obj.Jsqd;
                //因为一个科室可能有很多个ddzt，所以需要针对当前订单状态得到一个Obj，再加到已有的Obj中
                HospitalVO newObj = GetObj(obj, obj.Count * fee);

                AddCount(zyObj, newObj, obj.Ddzt);
                AddCount(fyObj, newObj, obj.Ddzt);
                AddCount(ksObj, newObj, obj.Ddzt);
            }

            // Sort
            foreach(var item in resultMap)
            {
                if (item.Value.Children == null)
                    continue;
                item.Value.Children.Sort((x, y) => (int)(y.Ze - x.Ze));
            }
            resultList.Sort((x,y) => (int)(y.Ze - x.Ze));
            //如果只有一个分院，那么去掉一个层级
            resultList.ForEach(item =>
            {
                if(item.Children.Count == 1)
                {
                    var fymc = item.Children[0].Fymc;
                    item.Children = item.Children[0].Children;
                    item.Children.ForEach(x => x.Fymc = fymc);
                }
            });

            return MyResponse.Success(resultList);
        }

        private static string ParseDdzt(string ddzt)
        {
            if (ddzt.Equals("取消")) return "qx";
            else if (ddzt.Equals("已就诊")) return "yjz";
            else return "wdz";
        }

        private static void AddCount(HospitalVO target, HospitalVO source, string ddzt)
        {
            if(ddzt.Equals("取消"))
                target.Qxl += source.Qxl;
            else if(ddzt.Equals("未到诊"))
                target.Wjzl += source.Wjzl;
            else
                target.Jzl += source.Jzl;
            target.Yyzl += source.Yyzl;
            target.Ze += source.Ze;
        }

        public static HospitalVO GetObj(TempCalculateYyxx obj, double ze)
        {
            var newObj = new HospitalVO { Id = obj.Ksczid , Qd = obj.Jsqd, Ze = ze};
            if (obj.Ddzt.Equals("qx"))
                newObj.Qxl = obj.Count;
            else if (obj.Ddzt.Equals("yjz"))
                newObj.Jzl = obj.Count;
            else newObj.Wjzl = obj.Count;
            newObj.Yyzl = obj.Count;
            return newObj;
        }
    }
}
