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
            var orgMap = GetOrgMap(db);
            var temp = GetFliterResult(db, ksIds, dates, feeTypes);
            var ruleMap = GetNormalRuls(db, ksIds);
            var rateRuleMap = GetRateRuls(db, ksIds);
            var displayRuleMap = GetDisplayRules(db);

            // 用于计算阶梯，因为阶梯一定是集体显示，且在普通计算规则中没有，所以可以利用普通规则计算出人数，这样可以统一逻辑
            // 所以只需要记录一个阶梯中任意一个ksid，然后计算出来价格就行了，不要重复计算
            // entryRateSet记录的就是集体中的一个，visitedRateSet是为了防止将一个集体中记录多次
            var entryRateSet = new HashSet<string>();
            var visitedRateSet = new HashSet<string>();

            var resultList = new List<HospitalVO>();
            var resultMap = new Dictionary<string, HospitalVO>();
            foreach (var obj in temp)
            {
                var zyId = obj.Ksczid.Substring(0, 3);
                var fyId = obj.Ksczid.Substring(0, 6);
                HospitalVO zyObj, fyObj;
                if (!resultMap.ContainsKey(zyId))
                {
                    resultMap[zyId] = new HospitalVO { Id = zyId, Zymc = (string)orgMap[zyId]["Name"] };
                    resultList.Add(resultMap[zyId]);
                }
                zyObj = resultMap[zyId];

                if (!resultMap.ContainsKey(fyId))
                {
                    resultMap[fyId] = new HospitalVO { Id = fyId, Fymc = (string)orgMap[fyId]["Name"] };
                    if (zyObj.Children == null)
                        zyObj.Children = new List<HospitalVO>();
                    zyObj.Children.Add(resultMap[fyId]);
                    //如果组织结构中有分院，先创建所有科室，防止没人挂号，导致不显示的问题
                    if (((List<string>)(orgMap[fyId]["Children"])).Count != 0)
                    {
                        resultMap[fyId].Children = new List<HospitalVO>();
                        ((List<string>)(orgMap[fyId]["Children"]))
                            .ForEach(x => {
                                if (!resultMap.ContainsKey(x))
                                {
                                    var tempKsObj = new HospitalVO { Id = x, Ks = (string)(orgMap[x]["Name"]) };
                                    resultMap[fyId].Children.Add(tempKsObj);
                                    resultMap[x] = tempKsObj;

                                    // 如果对象需要与其他科室一起展示，那么进行遍历
                                    if (displayRuleMap.ContainsKey(x))
                                    {
                                        SetDiplayDepartment(resultMap, x, displayRuleMap, orgMap, true);
                                        SetDiplayDepartment(resultMap, x, displayRuleMap, orgMap, false);
                                    }
                                }
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

                if (rateRuleMap.ContainsKey(obj.Ksczid) && !visitedRateSet.Contains(obj.Ksczid))
                {
                    entryRateSet.Add(obj.Ksczid);
                    visitedRateSet.UnionWith(ksObj.Id.Split(","));
                }
            }

            // 计算阶梯值
            foreach(var ksId in entryRateSet)
            {
                SetRateFee(ksId, rateRuleMap, resultMap);
            }

            // Sort
            foreach (var item in resultMap)
            {
                if (item.Value.Children == null)
                    continue;
                item.Value.Children.Sort((x, y) => (int)(y.Ze - x.Ze));
            }
            resultList.Sort((x, y) => (int)(y.Ze - x.Ze));
            //如果只有一个分院，那么去掉一个层级
            resultList.ForEach(item =>
            {
                if (item.Children.Count == 1)
                {
                    var fymc = item.Children[0].Fymc;
                    item.Children = item.Children[0].Children;
                    item.Children.ForEach(x => x.Fymc = fymc);
                }
            });

            return MyResponse.Success(resultList);
        }

        private static Dictionary<string, JsonObject> GetOrgMap(SqlSugarClient db)
        {
            var orgMap = new Dictionary<string, JsonObject>();
            db.Queryable<OrganizationPO>()
                .ToList()
                .ForEach(x => {
                    orgMap[x.Id] = new JsonObject { ["Name"] = x.Name, ["Id"] = x.Id };
                    if (x.Type != 3)
                        orgMap[x.Id]["Children"] = new List<string>();
                    //将自己记录到父节点，防止出现本月没人挂号，不显示统计的问题
                    if (x.Type != 1)
                        ((List<string>)(orgMap[x.Id.Substring(0, (x.Type - 1) * 3)]["Children"])).Add(x.Id);
                });
            return orgMap;
        }

        private static List<TempCalculateYyxx> GetFliterResult(SqlSugarClient db, string[] ksIds, string[] dates, string[] feeTypes)
        {
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
                    new TempCalculateYyxx
                    {
                        Ksczid = x.ksczid,
                        Ddzt = ParseDdzt(x.ddzt),
                        Jsqd = x.jsqd,
                        Count = x.count
                    })
                );
            return temp;
        }

        private static Dictionary<string, NormalRulesPO> GetNormalRuls(SqlSugarClient db, string[] ksIds)
        {
            var ruleMap = new Dictionary<string, NormalRulesPO>();
            var ksSet = new HashSet<string>(ksIds);
            db.Queryable<NormalRulesPO>()
                .Where(x => ksSet.Contains(x.Id.Substring(0, 9)))
                .ToList()
                .ForEach(x => ruleMap[x.Id] = x);
            return ruleMap;
        }

        private static Dictionary<string, List<RateRulesPO>> GetRateRuls(SqlSugarClient db, string[] ksIds)
        {
            var rateRuls = new Dictionary<string, List<RateRulesPO>>();
            var ksSet = new HashSet<string>(ksIds);
            db.Queryable<RateRulesPO>()
                            .Where(x => ksSet.Contains(x.KsId))
                            .ToList()
                            .ForEach(x => {
                                if (!rateRuls.ContainsKey(x.KsId))
                                    rateRuls[x.KsId] = new List<RateRulesPO>();
                                rateRuls[x.KsId].Add(x);
                            });
            return rateRuls;
        }

        private static Dictionary<string, DisplayRulesPO> GetDisplayRules(SqlSugarClient db)
        {
            var displayRuleMap = new Dictionary<string, DisplayRulesPO>();
            db.Queryable<DisplayRulesPO>()
                .Where(x => x.IsDisplay)
                .ToList()
                .ForEach(x => displayRuleMap[x.Id] = x);
            return displayRuleMap;
        }

        // 将组合一起显示的科室对象进行显示
        private static void SetDiplayDepartment(Dictionary<string, HospitalVO> resultMap, string currentKey, 
            Dictionary<string, DisplayRulesPO> displayRules, Dictionary<string, JsonObject> orgMap, bool down)
        {
            var obj = resultMap[currentKey];
            string nextSearchKey = down ? displayRules[currentKey].Next : displayRules[currentKey].Pre;
            if (nextSearchKey == null) return;

            resultMap[nextSearchKey] = obj;
            if (down)
            {
                obj.Ks += "," + orgMap[nextSearchKey]["Name"];
                obj.Id += "," + orgMap[nextSearchKey]["Id"];
            }
            else
            {
                obj.Ks = orgMap[nextSearchKey]["Name"] + "," + obj.Ks;
                obj.Id = orgMap[nextSearchKey]["Id"] + "," + obj.Id;
            }
            SetDiplayDepartment(resultMap, nextSearchKey, displayRules, orgMap, down);
        }

        private static string ParseDdzt(string ddzt)
        {
            if (ddzt.Equals("取消")) return "qx";
            else if (ddzt.Equals("已就诊")) return "yjz";
            else return "wdz";
        }

        private static void AddCount(HospitalVO target, HospitalVO source, string ddzt)
        {
            if (ddzt.Equals("qx"))
                target.Qxl += source.Qxl;
            else if (ddzt.Equals("wdz"))
                target.Wjzl += source.Wjzl;
            else
                target.Jzl += source.Jzl;
            target.Yyzl += source.Yyzl;
            target.Ze += source.Ze;
        }

        public static HospitalVO GetObj(TempCalculateYyxx obj, double ze)
        {
            var newObj = new HospitalVO { Id = obj.Ksczid, Qd = obj.Jsqd, Ze = ze };
            if (obj.Ddzt.Equals("qx"))
                newObj.Qxl = obj.Count;
            else if (obj.Ddzt.Equals("yjz"))
                newObj.Jzl = obj.Count;
            else newObj.Wjzl = obj.Count;
            newObj.Yyzl = obj.Count;
            return newObj;
        }

        public static void SetRateFee(string ksId, Dictionary<string, List<RateRulesPO>> rateRuleMap,
            Dictionary<string, HospitalVO> resultMap)
        {
            // 从大到小排序，然后从后往前找第一个大于start的区间
            rateRuleMap[ksId].Sort((x, y) => (int)(y.Start - x.Start));
            var ksObj = resultMap[ksId];
            double denoCount = 0, moleCount = 0;
            var rule = rateRuleMap[ksId][0];
            if (rule.Deno.IndexOf("qx") != -1)
                denoCount += ksObj.Qxl;
            if (rule.Deno.IndexOf("wdz") != -1)
                denoCount += ksObj.Wjzl;
            if (rule.Deno.IndexOf("yjz") != -1)
                denoCount += ksObj.Jzl;

            if (!Double.TryParse(rule.Mole, out moleCount))
            {
                if (rule.Mole.IndexOf("qx") != -1)
                    moleCount += ksObj.Qxl;
                if (rule.Mole.IndexOf("wdz") != -1)
                    moleCount += ksObj.Wjzl;
                if (rule.Mole.IndexOf("yjz") != -1)
                    moleCount += ksObj.Jzl;
            }

            foreach (var tempRule in rateRuleMap[ksId])
            {
                if ((denoCount / moleCount) >= tempRule.Start)
                {
                    ksObj.Ze = denoCount * tempRule.Fee;
                    break;
                }
            }

            //还需要将Ze加到分院和总院
            resultMap[ksId.Substring(0, 6)].Ze += ksObj.Ze;
            resultMap[ksId.Substring(0, 3)].Ze += ksObj.Ze;
        }
    }
}
