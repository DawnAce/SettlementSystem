using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementSystem.Dao
{
    public class SugarDao
    {
        private SugarDao()
        {

        }
        public static string ConnectionString
        {
            get
            {
                string reval = "server=.;uid=sa;pwd=sasa;database=SqlSugarTest";
                return reval;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Server=52.231.54.69;Port=3306;Database=MyDatabase;Uid=Ace;Pwd=Chenxi19900604;pooling=true",
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            //Print sql
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}
