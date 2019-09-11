using SettlementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Service
{
    public class CalculateService
    {
        public IList<Hospital> GetResultByHospitalId(string id, string typeArray)
        {
            var dataService = new DataService();
            var departments = dataService.GetDepartmentsHospitalId(id, typeArray);
            var rules = dataService.GetDepartmentCalculateRules();

            var rulesMap = new Dictionary<string, Rules>();
            foreach(var rule in rules)
                rulesMap.Add(rule.Id, rule);

            foreach(var department in departments)
            {
                var rule = rulesMap[department.Id];
                department.Yyzl = department.Wjzl + department.Qxl + department.Jzl;
                department.Ze = department.Wjzl * rule.Wjzje + department.Qxl * rule.Qxje + department.Jzl * rule.Dzje;
            }

            return departments;
        }
    }
}
