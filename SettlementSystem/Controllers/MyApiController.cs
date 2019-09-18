using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SettlementSystem.Models;
using SettlementSystem.Service;

namespace SettlementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        IHostingEnvironment _env;
        public MyApiController(IHostingEnvironment env)
        {
            _env = env;
        }

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

        [HttpPost]
        [ActionName("Upload")]
        public string UploadExcel()
        {
            var file = Request.Form.Files[0];

            return JsonConvert.SerializeObject(FileService.UploadFile(file));
        }

        [HttpGet]
        [ActionName("GetPageData")]
        public string GetPageData(string ksIds,string date, int pageIndex, int pageSize, bool needTotalCount)
        {
            return JsonConvert.SerializeObject(DataService.GetYyxxByPagenation(ksIds, date, pageIndex, pageSize, needTotalCount));
        }

        [HttpGet]
        [ActionName("GetDateChoices")]
        public string GetAllDateChoices(string year)
        {
            return JsonConvert.SerializeObject(DataService.GetDateChoices(year));
        }

        [HttpGet]
        [ActionName("GetHospitalTree")]
        public string GetHospitalTree()
        {
            return JsonConvert.SerializeObject(DataService.GetHospitalTree());
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
