using Newtonsoft.Json;
using RestSharp;
using SettlementSystem.Dao;
using SettlementSystem.Models;
using SettlementSystem.Models.PO;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Service
{
    public class DataService
    {
        public static MyResponse GetYyxxByPagenation(string[] ksIds,string[] dates, int pageIndex, int pageSize, bool needTotalCount)
        {
            var db = SugarDao.GetInstance();

            var ksSet = new HashSet<string>(ksIds);
            var dateSet = new HashSet<string>(dates);
            var totalCount = 0;
            var temp = db.Queryable<YyxxPO>()
                .Where(x => dateSet.Contains(x.Id.Substring(0,6)))
                .Where(x => ksSet.Contains(x.Ksczid))
                .OrderBy(x => x.Ycsj, OrderByType.Desc);
            IList<YyxxPO> list = null;
            
            //如果是第一次查询的话，需要带上ref totalCount，否则不需要
            if (needTotalCount)
                list = temp.ToPageList(pageIndex, pageSize, ref totalCount);  
            else
                list = temp.ToPageList(pageIndex, pageSize);
            var obj= new JsonObject();
            obj["totalCount"] = totalCount;
            obj["list"] = list;

            return MyResponse.Success(obj);
        }

        public static MyResponse GetDateChoices(string year)
        {
            var db = SugarDao.GetInstance();
            var result = db.SqlQueryable<TempSearchResult>($"select distinct(substr(jzrq,1,7)) as result from Yyxx where substr(jzrq,1,4)='{year}' order by result").ToList();

            return MyResponse.Success(result);
        }

        /// <summary>
        /// 归根结底还是要靠科室来进行计算查询，当需要过滤时，需要剔除一些
        /// </summary>
        /// <returns></returns>
        public static MyResponse GetHospitalTree(bool disableHospitals)
        {
            var db = SugarDao.GetInstance();
            var tempResult = db.Queryable<OrganizationPO>();
            if (disableHospitals)
            {
                var hospitalSet = new HashSet<string>(db.GetSimpleClient<SomeValues>().GetById("ComputerHospitals").Content.Split(","));
                tempResult.Where(x => hospitalSet.Contains(x.Id.Substring(0, 3)));
            }

            //排序后，可以保证遍历顺序为：总院-分院-科室
            var result = tempResult.OrderBy(x => x.Id).ToList();
            var map = new Dictionary<string, JsonObject>();
            var root = new JsonObject
            {
                ["children"] = new List<JsonObject>(),
                ["default"] = new List<string[]>()
            };
            foreach (var obj in result)
            {
                var node = CreateNode(obj);
                if(obj.Type == 1)
                {
                    ((List<JsonObject>)root["children"]).Add(node);
                }
                else
                {
                    var parentId = obj.Id.Substring(0, (obj.Type - 1)*3);
                    var parentNode = map[parentId];
                    if (!parentNode.ContainsKey("children"))
                        parentNode["children"] = new List<JsonObject>();
                    ((List<JsonObject>)parentNode["children"]).Add(node);
                }
                if(obj.Type != 3)
                    map.Add(obj.Id, node);
                else
                {
                    string[] temp = { obj.Id.Substring(0, 3), obj.Id.Substring(0, 6), obj.Id };
                    ((List<string[]>)root["default"]).Add(temp);
                }
                    
            }

            return MyResponse.Success(root);
        }

        private static JsonObject CreateNode(OrganizationPO obj)
        {
            return new JsonObject
            {
                ["value"] = obj.Id,
                ["label"] = obj.Name,
                ["type"] = obj.Type
            };
        }
    }
}
