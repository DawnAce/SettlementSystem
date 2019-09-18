using NPOI.HPSF;
using NUnit.Framework;
using RestSharp;
using SettlementSystem.Dao;
using SettlementSystem.Models;
using SettlementSystem.Service;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConnector()
        {
            var db = SugarDao.GetInstance();
            var list = db.Queryable<TestEntity>().ToList();
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void TestPagenation()
        {
            var db = SugarDao.GetInstance();
            var totalCount = 0;
            var list = db.Queryable<Yyxx>()
                .Where(x => x.Id.StartsWith("201909"))
                .OrderBy(x => x.Jzrq)
                .ToPageList(1000, 10, ref totalCount);  //如果是第一次查询的话，需要带上ref totalCount，否则不需要
            Assert.IsTrue(totalCount > 0);
        }

        [Test]
        public void TestGetDateChoices()
        {
            var result = DataService.GetDateChoices("2019");
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetHospitalTree()
        {
            var result = DataService.GetHospitalTree();
            Assert.AreEqual(46, ((IList)((JsonObject)result.Data)["children"]).Count);

            var str = "(";
            foreach(var array in (IList)((JsonObject)result.Data)["default"])
            {
                str += "'" + ((string[])array)[2] + "',";
            }
            Assert.IsNotNull(str);
        }

    }
}