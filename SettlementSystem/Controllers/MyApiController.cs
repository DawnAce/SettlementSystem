﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SettlementSystem.Service;

namespace SettlementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        // GET: api/MyApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MyApi/5
        [HttpGet("{id}")]
        [ActionName("Gets")]
        public string Get(string id)
        {
            return "{\"id\":" + id + "}";
        }

        [HttpGet("{id}")]
        [ActionName("CalculateDepartment")]
        public string QueryResultByHospitalId(string id, string qd)
        {
            return JsonConvert.SerializeObject(new CalculateService().GetDepartmentResult(id, qd));
        }

        [HttpGet("{id}")]
        [ActionName("CalculateHospital")]
        public string QueryHospitalResult(string id, string qd)
        {
            return JsonConvert.SerializeObject(new CalculateService().GetHospitalResult(id, qd));
        }

        // POST: api/MyApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MyApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        // Refer to https://www.c-sharpcorner.com/article/httpdelete-method-in-asp-net-web-api-part/
        [HttpGet("{id}")]
        public string Delete(int id)
        {
            return "Success";
        }
    }
}
