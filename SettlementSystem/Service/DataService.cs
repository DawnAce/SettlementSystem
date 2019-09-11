using SettlementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Service
{
    public class DataService
    {
        public IList<Hospital> GetHospitals(string id, string typeArray)
        {
            var result = new List<Hospital>
            {
                new Hospital { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "ky", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Wjzl = 3, Qxl = 5, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002005", Mc = "长峰医院", Ks = "妇科", Wjzl = 3, Qxl = 4, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002009", Mc = "长峰医院", Ks = "心血管", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002010", Mc = "长峰医院", Ks = "神内", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002011", Mc = "长峰医院", Ks = "中医", Wjzl = 7, Qxl = 10, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" }
            };

            var types = new HashSet<string>(typeArray.Split(","));
            return result.Where(x => types.Contains(x.Qd)).ToList();
        }

        public IList<Hospital> GetDepartments(string id, string typeArray)
        {
            var result = new List<Hospital>
            {
                new Hospital { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 2, Qxl = 4, Jzl = 3, Yyzl = 0, Qd = "ky", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Wjzl = 3, Qxl = 5, Jzl = 3, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002001,002002" },
                new Hospital { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002005", Mc = "长峰医院", Ks = "妇科", Wjzl = 3, Qxl = 4, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002003,002004,002005" },
                new Hospital { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002009", Mc = "长峰医院", Ks = "心血管", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002010", Mc = "长峰医院", Ks = "神内", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" },
                new Hospital { Id = "002011", Mc = "长峰医院", Ks = "中医", Wjzl = 7, Qxl = 10, Jzl = 8, Yyzl = 0, Qd = "114", Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" }
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
    }
}
