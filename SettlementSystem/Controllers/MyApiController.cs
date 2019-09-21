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
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RestSharp;
using SettlementSystem.Models;
using SettlementSystem.Models.DTO;
using SettlementSystem.Models.PostType;
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

        [HttpPost]
        [ActionName("Calculate")]
        public string Calculate([FromBody]CalculateDTO calculateRequest)
        {
            return JsonConvert.SerializeObject(
                CalculateService.CalculateFee(calculateRequest.KsIds, calculateRequest.FeeTypes, calculateRequest.Dates)
            );
        }

        [HttpPost]
        [ActionName("Upload")]
        public string UploadExcel()
        {
            var file = Request.Form.Files[0];

            return JsonConvert.SerializeObject(FileService.UploadFile(file));
        }

        [HttpPost]
        [ActionName("GetPageData")]
        public string GetPageData([FromBody]QueryDTO queryRequest)
        {
            return JsonConvert.SerializeObject(
                DataService.GetYyxxByPagenation(
                    queryRequest.KsIds, 
                    queryRequest.Dates, 
                    queryRequest.PageIndex,
                    queryRequest.PageSize,
                    queryRequest.NeedTotalCount)
            );
        }

        [HttpGet]
        [ActionName("GetDateChoices")]
        public string GetAllDateChoices(string year)
        {
            return JsonConvert.SerializeObject(DataService.GetDateChoices(year));
        }

        [HttpGet]
        [ActionName("GetHospitalTree")]
        public string GetHospitalTree(bool disableHospitals)
        {
            return JsonConvert.SerializeObject(DataService.GetHospitalTree(disableHospitals));
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
