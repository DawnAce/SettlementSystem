using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Models
{
    public class MyResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        public static MyResponse Success()
        {
            return new MyResponse { Code = "200" };
        }

        public static MyResponse Success(object data)
        {
            return new MyResponse { Code = "200", Data = data };
        }

        public static MyResponse Fail(string message, string code = "500")
        {
            return new MyResponse { Code = code, Message = message };
        }
    }
}
