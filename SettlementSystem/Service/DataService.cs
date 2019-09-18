using Newtonsoft.Json;
using RestSharp;
using SettlementSystem.Dao;
using SettlementSystem.Models;
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
        public IList<HospitalVO> GetHospitals(string id, string typeArray)
        {
            var result = new List<HospitalVO>
            {
                new HospitalVO { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "ky", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Wjzl = 3, Qxl = 5, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002005", Mc = "长峰医院", Ks = "妇科", Wjzl = 3, Qxl = 4, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002009", Mc = "长峰医院", Ks = "心血管", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002010", Mc = "长峰医院", Ks = "神内", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002011", Mc = "长峰医院", Ks = "中医", Wjzl = 7, Qxl = 10, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" }
            };

            var types = new HashSet<string>(typeArray.Split(","));
            return result.Where(x => types.Contains(x.Qd)).ToList();
        }

        public IList<HospitalVO> GetDepartments(string id, string typeArray)
        {
            var result = new List<HospitalVO>
            {
                new HospitalVO { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "ky", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Wjzl = 3, Qxl = 5, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new HospitalVO { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002005", Mc = "长峰医院", Ks = "妇科", Wjzl = 3, Qxl = 4, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new HospitalVO { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002009", Mc = "长峰医院", Ks = "心血管", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002010", Mc = "长峰医院", Ks = "神内", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new HospitalVO { Id = "002011", Mc = "长峰医院", Ks = "中医", Wjzl = 7, Qxl = 10, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" }
            };

            // ids为医院代码的集合或者科室代码的集合，如果为医院代码，那么需要比较前3位，如果为科室代码，那么只需要比较相等性
            // 科室代码前3位是医院代码
            var ids = new HashSet<string>(id.Split(","));
            result = result.Where(x => ids.Contains(x.Id) || ids.Contains(x.Id.Substring(0, 3))).ToList();
            
            var types = new HashSet<string>(typeArray.Split(","));
            result = result.Where(x => types.Contains(x.Qd)).ToList();
            return result;
        }

        public IList<Rules> GetDepartmentCalculateRules()
        {
            var result = new List<Rules>
            {
                new Rules { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 300 },
                new Rules { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 300 },
                new Rules { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 240 },
                new Rules { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 240 },
                new Rules { Id = "002005", Mc = "长峰医院", Ks = "妇科", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 240 },
                new Rules { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 },
                new Rules { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 },
                new Rules { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 },
                new Rules { Id = "002009", Mc = "长峰医院", Ks = "心血管", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 },
                new Rules { Id = "002010", Mc = "长峰医院", Ks = "神内", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 },
                new Rules { Id = "002011", Mc = "长峰医院", Ks = "中医", Jslx = "普通", Wjzje = 0, Qxje = 0, Qd = "114", Dzje = 120 }
            };

            return result;
        }

        public static MyResponse GetYyxxByPagenation(string ksIds,string date, int pageIndex, int pageSize, bool needTotalCount)
        {
            var db = SugarDao.GetInstance();

            var totalCount = 0;
            var temp = db.Queryable<Yyxx>()
                .Where(x => x.Id.StartsWith(date))
                .OrderBy(x => x.Jzrq);
            IList<Yyxx> list = null;
            
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
        /// 归根结底还是要靠科室来进行计算查询
        /// </summary>
        /// <returns></returns>
        public static MyResponse GetHospitalTree()
        {
            //排序后，可以保证遍历顺序为：总院-分院-科室
            var result = SugarDao.GetInstance().Queryable<OrganizationPO>().OrderBy(x => x.Id).ToList();
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
