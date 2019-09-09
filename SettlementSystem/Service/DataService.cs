﻿using SettlementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Service
{
    public class DataService
    {
        public IList<Hospital> GetDepartmentsHospitalId(string hospitalId)
        {
            var result = new List<Hospital>();
            result.Add(new Hospital { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Wjzl = 4, Qxl = 8, Jzl = 6, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002001,002002" });
            result.Add(new Hospital { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Wjzl = 3, Qxl = 5, Jzl = 3, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002001,002002" });
            result.Add(new Hospital { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002003,002004,002005" });
            result.Add(new Hospital { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Wjzl = 3, Qxl = 3, Jzl = 6, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002003,002004,002005" });
            result.Add(new Hospital { Id = "002005", Mc = "长峰医院", Ks = "妇科", Wjzl = 3, Qxl = 4, Jzl = 8, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002003,002004,002005" });
            result.Add(new Hospital { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });
            result.Add(new Hospital { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });
            result.Add(new Hospital { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });
            result.Add(new Hospital { Id = "002009", Mc = "长峰医院", Ks = "心血管", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });
            result.Add(new Hospital { Id = "002010", Mc = "长峰医院", Ks = "神内", Wjzl = 7, Qxl = 15, Jzl = 9, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });
            result.Add(new Hospital { Id = "002011", Mc = "长峰医院", Ks = "中医", Wjzl = 7, Qxl = 10, Jzl = 8, Yyzl = 0, Bz = "", Ze = 0, Xsjh = "002006,002007,002008,002009,002010,002011" });

            return result;
        }

        public IList<Rules> GetDepartmentCalculateRules()
        {
            var result = new List<Rules>();
            result.Add(new Rules { Id = "002001", Mc = "长峰医院", Ks = "血管外科", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 300});
            result.Add(new Rules { Id = "002002", Mc = "长峰医院", Ks = "血管瘤", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 300 });
            result.Add(new Rules { Id = "002003", Mc = "长峰医院", Ks = "男科", Wjzje = 0, Qxje = 0, Dzje = 240 });
            result.Add(new Rules { Id = "002004", Mc = "长峰医院", Ks = "泌尿", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 240 });
            result.Add(new Rules { Id = "002005", Mc = "长峰医院", Ks = "妇科", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 240 });
            result.Add(new Rules { Id = "002006", Mc = "长峰医院", Ks = "内分泌", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });
            result.Add(new Rules { Id = "002007", Mc = "长峰医院", Ks = "糖尿", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });
            result.Add(new Rules { Id = "002008", Mc = "长峰医院", Ks = "甲状腺", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });
            result.Add(new Rules { Id = "002009", Mc = "长峰医院", Ks = "心血管", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });
            result.Add(new Rules { Id = "002010", Mc = "长峰医院", Ks = "神内", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });
            result.Add(new Rules { Id = "002011", Mc = "长峰医院", Ks = "中医", Jslx = "普通", Wjzje = 0, Qxje = 0, Dzje = 120 });

            return result;
        }
    }
}