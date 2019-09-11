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
        public IList<Hospital> GetHospitalResult(string id, string typeArray)
        {
            var dataService = new DataService();
            var departments = dataService.GetHospitals(id, typeArray);
            var rules = dataService.GetDepartmentCalculateRules();

            var rulesMap = new Dictionary<string, Rules>();
            foreach (var rule in rules)
                rulesMap.Add(rule.Id, rule);
            var hospitalsMap = new Dictionary<string, Hospital>();
            
            foreach(var department in departments)
            {
                var rule = rulesMap[department.Id];
                var hospitalId = department.Id.Substring(0, 3);
                Hospital hospital = null;
                if (hospitalsMap.ContainsKey(hospitalId))
                    hospital = hospitalsMap[hospitalId];
                else
                {
                    hospital = new Hospital
                    {
                        Id = hospitalId,
                        Mc = department.Mc
                    };
                    hospitalsMap.Add(hospitalId, hospital);
                }

                hospital.Wjzl += department.Wjzl;
                hospital.Qxl += department.Qxl;
                hospital.Jzl += department.Jzl;
                hospital.Yyzl += department.Wjzl + department.Qxl + department.Jzl;
                hospital.Ze += department.Wjzl * rule.Wjzje + department.Qxl * rule.Qxje + department.Jzl * rule.Dzje;
            }

            var result = new List<Hospital>(hospitalsMap.Values);
            return result;
        }

        public IList<Hospital> GetDepartmentResult(string id, string typeArray)
        {
            var dataService = new DataService();
            var departments = dataService.GetDepartments(id, typeArray);
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
