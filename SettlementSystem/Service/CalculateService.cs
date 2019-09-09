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
        public IList<Hospital> GetResultByHospitalId(string hospitalId)
        {
            var dataService = new DataService();
            var departments = dataService.GetDepartmentsHospitalId(hospitalId);
            var rules = dataService.GetDepartmentCalculateRules();

            var result = new List<Hospital>();

            return result;
        }
    }
}
