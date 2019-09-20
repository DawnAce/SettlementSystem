using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SettlementSystem.Dao;
using SettlementSystem.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Net.Http;

namespace SettlementSystem.Service
{
    public class FileService
    {
        /// <summary>
        /// 存储的数据间隔
        /// </summary>
        private static int SAVEINTERVAL = 1000;
        private static Dictionary<string, string> organizationMap;

        /// <summary>
        /// 需要解析的表所在的位置
        /// </summary>
        private static int SHEETINDEX = 1;

        public static MyResponse UploadFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                return new MyResponse { Code = "500", Message = "File is empty" };
            }

            try
            {
                ParseFileAndStore(file);
                return new MyResponse { Code = "200" };
            }
            catch (Exception e)
            {
                return new MyResponse { Code = "500", Message = e.Message };
            }
        }

        private static void ParseFileAndStore(IFormFile file)
        {
            var sFileExtension = Path.GetExtension(file.FileName).ToLower();
            ISheet sheet;
            var fullPath = Path.Combine("", file.FileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Position = 0;
                if (sFileExtension == ".xls")
                {
                    sheet = new HSSFWorkbook(stream).GetSheetAt(SHEETINDEX);
                }
                else
                {
                    sheet = new XSSFWorkbook(stream).GetSheetAt(SHEETINDEX);
                }
            }

            var dao = SugarDao.GetInstance();
            GetMap(dao);
            var operationList = new List<YyxxPO>();
            for (int i = sheet.FirstRowNum + 3; i <= sheet.LastRowNum; ++i)
            {
                var row = sheet.GetRow(i);
                var yyxx = ParseRow(row);
                if (String.IsNullOrEmpty(yyxx.Id))
                    Console.WriteLine(yyxx);
                operationList.Add(yyxx);
                if (operationList.Count == SAVEINTERVAL)
                {
                    dao.Saveable<YyxxPO>(operationList).ExecuteReturnEntity();
                    operationList.Clear();
                }
            }
            if (operationList.Count != 0)
                dao.Saveable<YyxxPO>(operationList).ExecuteReturnEntity();

            //delete file
            new FileInfo(fullPath).Delete();
        }

        /// <summary>
        /// 分院名称和科室名称都没动，仅靠这两个就可以确定唯一性
        /// </summary>
        /// <param name="db"></param>
        private static void GetMap(SqlSugarClient db)
        {
            var organizations = db.Queryable<OrganizationPO>().OrderBy(x => x.Id).ToList();
            var fyMap = new Dictionary<string, string>();
            organizationMap = new Dictionary<string, string>();
            foreach(var org in organizations)
            {
                if (org.Type == 1) continue;
                else if(org.Type == 2)
                {
                    fyMap[org.Id] = org.Name;
                }
                else
                {
                    organizationMap[fyMap[org.Id.Substring(0,6)] + org.Name] = org.Id;
                }
            }
        }

        private static YyxxPO ParseRow(IRow row)
        {
            var result = new YyxxPO();
            
            var cellCount = row.LastCellNum;
            var map = GetMap();
            for(int i=0; i<cellCount; ++i)
            {
                object value = row.GetCell(i)?.ToString();
                var fildName = Char.ToUpper(map[i][0]) + map[i].Substring(1);
                if (fildName.Equals("Ghf") || fildName.Equals("yhjg"))
                {
                    value = String.IsNullOrEmpty((string)value) ? "0" : value;
                    value = Int32.Parse((string)value);
                }
                else if (fildName.Equals("Sfdz") || fildName.Equals("Sfjf") 
                    || fildName.Equals("Sfjt") || fildName.Equals("Sfjz"))
                {
                    value = row.GetCell(i).ToString().Equals("是");
                }
                SetModelValue(fildName, value, result);
            }
            result.Ksczid = organizationMap[result.Fymc.Trim() + result.Ks.Trim()];
            result.Id = result.Jzrq.Replace("-", "").Substring(0, 6) + result.Id;
            return result;
        }

        /// <summary>
        /// 设置类中的属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SetModelValue(string FieldName, object Value, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object v = Convert.ChangeType(Value, Ts.GetProperty(FieldName).PropertyType);
                if (v.GetType() == typeof(string))
                    v = ((string)v).Trim();
                Ts.GetProperty(FieldName).SetValue(obj, v, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Dictionary<int, string> GetMap()
        {
            return new Dictionary<int, string>
            {
                {0, "Id"},
                {1, "Fymc"},
                {2, "Zymc"},
                {3, "Ghr"},
                {4, "Zjh"},
                {5, "Sjh"},
                {6, "Sbm"},
                {7, "Jzrq"},
                {8, "Sd"},
                {9, "Ks"},
                {10, "Ys"},
                {11, "Zc"},
                {12, "Ghf"},
                {13, "Yhqmc"},
                {14, "Yhjg"},
                {15, "Ycsj"},
                {16, "Yyxxbz"},
                {17, "Ddzt"},
                {18, "Zfzt"},
                {19, "Qxyy"},
                {20, "Zxgg"},
                {21, "Zxzc"},
                {22, "Yyqd"},
                {23, "Jsqd"},
                {24, "Dsfdd"},
                {25, "Sfdz"},
                {26, "Sfjf"},
                {27, "Wxfyy"},
                {28, "Dzrxm"},
                {29, "Blh"},
                {30, "Dzrqsj"},
                {31, "Yyzxxm"},
                {32, "Zzzlxm"},
                {33, "Sfjt"},
                {34, "Sfjz"},
                {35, "Xfxq"},
                {36, "Qt"},
                {37, "Bz"},
            };
        }

        /// <summary>
        /// 以后可能有用，目前没用，如果表格经常变，但是表头不变的话，可能需要用表头名进行对应
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private static string[] ParseHeader(ISheet sheet)
        {
            var firstHeadRow = sheet.GetRow(1);
            var cellCount = sheet.GetRow(0).LastCellNum;
            var propertiesArray = new string[cellCount + 1];
            for (int i = 0; i < cellCount; ++i)
            {
                propertiesArray[i] = firstHeadRow.GetCell(i).ToString();
            }

            var secondHeadRow = sheet.GetRow(2);
            for (int i = 0; i < cellCount; ++i)
            {
                if (String.IsNullOrEmpty(propertiesArray[i])
                    || String.IsNullOrEmpty(propertiesArray[i + 1]))
                {
                    propertiesArray[i] = secondHeadRow.GetCell(i)?.ToString();
                }
            }

            return propertiesArray;
        }
    }
}
