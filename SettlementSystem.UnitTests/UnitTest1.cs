using NUnit.Framework;
using SettlementSystem.Models;
using SettlementSystem.Service;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var db = new DataService().GetInstance();
            var list = db.SqlQueryable<TestEntity>("select * from Test").ToList();
            Assert.AreEqual(1, list.Count);
        }
    }
}