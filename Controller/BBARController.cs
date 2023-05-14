using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.Data.DB2.Core;
using System.Data;
using System.Configuration;
using Basic;
using SQLUI;
using MyUI;
using Oracle.ManagedDataAccess.Client;
using System.Text.Json;
using System.Text.Encodings;
using System.Text.Json.Serialization;
namespace DB2VM
{
    public enum enum_醫囑資料
    {
        GUID,
        PRI_KEY,
        藥局代碼,
        藥袋條碼,
        藥品碼,
        藥品名稱,
        病人姓名,
        病歷號,
        交易量,
        開方日期,
        產出時間,
        過帳時間,
        狀態,
    }

    [Route("dbvm/[controller]")]
    [ApiController]
    public class BBARController : ControllerBase
    {
      
        public enum enum_急診藥袋
        {
            本次領藥號,
            看診日期,
            病歷號,
            序號,
            頻率,
            途徑,
            總量,
            前次領藥號,
            本次醫令序號,
        }

        private class orderApi_OPD
        {
            [JsonPropertyName("MED_CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("MED_DESC")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("FEE_UNIT_DESC")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("TTL_QTY")]
            public int 總量 { get; set; }
            [JsonPropertyName("PAT_NAME")]
            public string 病人姓名 { get; set; }
            [JsonPropertyName("OPD_DATE")]
            public string 開方日期 { get; set; }
            [JsonPropertyName("BARCODE")]
            public string 藥袋條碼 { get; set; }
            [JsonPropertyName("PRI_KEY")]
            public string PRI_KEY { get; set; }


        }
        private class orderApi_ER
        {
            [JsonPropertyName("MED_CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("MED_DESC")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("FEE_UNIT_DESC")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("TTL_QTY")]
            public int 總量 { get; set; }
            [JsonPropertyName("PAT_NAME")]
            public string 病人姓名 { get; set; }
            [JsonPropertyName("OPD_DATE")]
            public string 開方日期 { get; set; }
            [JsonPropertyName("BARCODE")]
            public string 藥袋條碼 { get; set; }
            [JsonPropertyName("PRI_KEY")]
            public string PRI_KEY { get; set; }


        }
        private class orderApi_IPD
        {
            [JsonPropertyName("MED_CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("MED_DESC")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("FEE_UNIT_DESC")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("TTL_QTY")]
            public int 總量 { get; set; }
            [JsonPropertyName("PAT_NAME")]
            public string 病人姓名 { get; set; }
            [JsonPropertyName("EXEC_DATE")]
            public string 開方日期 { get; set; }
            [JsonPropertyName("BARCODE")]
            public string 藥袋條碼 { get; set; }
            [JsonPropertyName("PRI_KEY")]
            public string PRI_KEY { get; set; }


        }
        private class orderApi_CPD
        {
            [JsonPropertyName("MED_CODE")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("MED_DESC")]
            public string 藥品名稱 { get; set; }
            [JsonPropertyName("FEE_UNIT_DESC")]
            public string 包裝單位 { get; set; }
            [JsonPropertyName("TTL_QTY")]
            public int 總量 { get; set; }
            [JsonPropertyName("PAT_NAME")]
            public string 病人姓名 { get; set; }
            [JsonPropertyName("EXEC_DATE")]
            public string 開方日期 { get; set; }
            [JsonPropertyName("BARCODE")]
            public string 藥袋條碼 { get; set; }
            [JsonPropertyName("PRI_KEY")]
            public string PRI_KEY { get; set; }


        }
        static string MySQL_server = $"{ConfigurationManager.AppSettings["MySQL_server"]}";
        static string MySQL_database = $"{ConfigurationManager.AppSettings["MySQL_database"]}";
        static string MySQL_userid = $"{ConfigurationManager.AppSettings["MySQL_user"]}";
        static string MySQL_password = $"{ConfigurationManager.AppSettings["MySQL_password"]}";
        static string MySQL_port = $"{ConfigurationManager.AppSettings["MySQL_port"]}";

        public SQLControl sQLControl_UDSDBBCM = new SQLControl(MySQL_server, MySQL_database, "UDSDBBCM", MySQL_userid, MySQL_password, (uint)MySQL_port.StringToInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);
        public SQLControl sQLControl_醫囑資料 = new SQLControl(MySQL_server, MySQL_database, "order_list", MySQL_userid, MySQL_password, (uint)MySQL_port.StringToInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);

        [HttpGet]
        public string Get(string? BarCode,string? test)
        {
            if (BarCode.StringIsEmpty()) return "BarCode is null!";
            List<OrderClass> list_OrderClass = new List<OrderClass>();
            List<object[]> list_醫囑資料 = sQLControl_醫囑資料.GetRowsByDefult(null, (int)enum_醫囑資料.藥袋條碼, BarCode);
            for (int i = 0; i < list_醫囑資料.Count; i++)
            {
                OrderClass orderClass = new OrderClass();
                orderClass.PRI_KEY = list_醫囑資料[i][(int)enum_醫囑資料.PRI_KEY].ObjectToString();
                orderClass.藥局代碼 = list_醫囑資料[i][(int)enum_醫囑資料.藥局代碼].ObjectToString();
                orderClass.藥袋條碼 = list_醫囑資料[i][(int)enum_醫囑資料.藥袋條碼].ObjectToString();
                orderClass.交易量 = list_醫囑資料[i][(int)enum_醫囑資料.交易量].ObjectToString();
                orderClass.藥品碼 = list_醫囑資料[i][(int)enum_醫囑資料.藥品碼].ObjectToString();
                orderClass.藥品名稱 = list_醫囑資料[i][(int)enum_醫囑資料.藥品名稱].ObjectToString();
                orderClass.病人姓名 = list_醫囑資料[i][(int)enum_醫囑資料.病人姓名].ObjectToString();
                orderClass.病歷號 = list_醫囑資料[i][(int)enum_醫囑資料.病歷號].ObjectToString();
                orderClass.開方時間 = list_醫囑資料[i][(int)enum_醫囑資料.開方日期].ToDateTimeString();
                list_OrderClass.Add(orderClass);

            }

            return list_OrderClass.JsonSerializationt();
        }

        [Route("orderRefresh")]
        [HttpGet]
        public string Get_OrderRefresh()
        {
            string result = "";
            result += Function_Get_er();
            result += Function_Get_opd();
            result += Function_Get_ipd();
            result += Function_Get_cpd();

            return result;
        }

        private string Function_Get_er()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                DateTime dateTime_st = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0);
                DateTime dateTime_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-0).Day, 23, 59, 59);

                List<object[]> list_醫囑資料_ER = this.sQLControl_醫囑資料.GetRowsByBetween(null, (int)enum_醫囑資料.開方日期, dateTime_st.ToDateTimeString(), dateTime_end.ToDateTimeString());
                list_醫囑資料_ER = list_醫囑資料_ER.GetRows((int)enum_醫囑資料.藥局代碼, "ER");
                List<object[]> list_醫囑資料_ER_Add = new List<object[]>();
                string text = Basic.Net.WEBApiGet("http://10.65.1.229:5003/kwokzoco/er");

                Console.Write($"讀取醫囑資料(er)報表 , 耗時 : {myTimer.ToString()} ms\n");

                List<api_order_er> list_api_order_er = text.JsonDeserializet<List<api_order_er>>();
                List<my_order_er> list_my_order_ER = new List<my_order_er>();
                for (int i = 0; i < list_api_order_er.Count; i++)
                {
                    my_order_er my_order_er = new my_order_er();
                    my_order_er.Load(list_api_order_er[i]);
                    list_my_order_ER.Add(my_order_er);
                }

                Console.Write($"轉換醫囑資料(er)JsonDeserializet 共{list_api_order_er.Count}筆資料, 耗時 : {myTimer.ToString()} ms\n");
                Parallel.ForEach(list_my_order_ER, orderClasses_temp =>
                {
                    List<object[]> list_醫囑資料_ER_buf = new List<object[]>();
                    list_醫囑資料_ER_buf = list_醫囑資料_ER.GetRows((int)enum_醫囑資料.PRI_KEY, orderClasses_temp.PRI_KEY);
                    if (list_醫囑資料_ER_buf.Count == 0 )
                    {
                        DateTime dateTime = orderClasses_temp.開方日期.StringToDateTime();
                        if (dateTime.IsInDate(dateTime_st, dateTime_end))
                        {
                            object[] value_add = new object[new enum_醫囑資料().GetLength()];
                            value_add[(int)enum_醫囑資料.GUID] = Guid.NewGuid().ToString();
                            value_add[(int)enum_醫囑資料.PRI_KEY] = orderClasses_temp.PRI_KEY;
                            value_add[(int)enum_醫囑資料.藥局代碼] = "ER";
                            value_add[(int)enum_醫囑資料.藥袋條碼] = orderClasses_temp.藥袋條碼;
                            value_add[(int)enum_醫囑資料.藥品碼] = orderClasses_temp.藥品碼;
                            value_add[(int)enum_醫囑資料.藥品名稱] = orderClasses_temp.藥品名稱;
                            value_add[(int)enum_醫囑資料.病人姓名] = orderClasses_temp.病人姓名;
                            value_add[(int)enum_醫囑資料.病歷號] = orderClasses_temp.病歷號;
                            value_add[(int)enum_醫囑資料.交易量] = orderClasses_temp.交易量 * -1;
                            value_add[(int)enum_醫囑資料.開方日期] = orderClasses_temp.開方日期;
                            value_add[(int)enum_醫囑資料.產出時間] = DateTime.Now.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.過帳時間] = DateTime.MinValue.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.狀態] = "未過帳";
                            list_醫囑資料_ER_Add.LockAdd(value_add);
                        }
                    }
                });
                list_醫囑資料_ER_Add = list_醫囑資料_ER_Add.Distinct(new Distinct_order()).ToList();
                Console.Write($"檢查醫囑資料(er)完成,共{list_醫囑資料_ER_Add.Count}筆需更新 , 耗時 : {myTimer.ToString()} ms\n");
                if (list_醫囑資料_ER_Add.Count > 0) this.sQLControl_醫囑資料.AddRows(null, list_醫囑資料_ER_Add);
                Console.Write($"醫囑資料(er)寫入資料庫完成 , 耗時 : {myTimer.ToString()} ms\n");
                string num = list_醫囑資料_ER.Count.ToString();
                string add = list_醫囑資料_ER_Add.Count.ToString();
                return $"ER 資料共{list_my_order_ER.Count}筆,新增{list_醫囑資料_ER_Add.Count}筆 ,資料庫資料共{list_醫囑資料_ER.Count}筆\n";
            }
            catch
            {
                Console.Write($"醫囑資料(ER)更新失敗\n");
                return "醫囑資料(ER)更新失敗\n";
            }

        }
        private string Function_Get_opd()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                DateTime dateTime_st = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0);
                DateTime dateTime_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-0).Day, 23, 59, 59);

                List<object[]> list_醫囑資料_OPD = this.sQLControl_醫囑資料.GetRowsByBetween(null, (int)enum_醫囑資料.開方日期, dateTime_st.ToDateTimeString(), dateTime_end.ToDateTimeString());
                list_醫囑資料_OPD = list_醫囑資料_OPD.GetRows((int)enum_醫囑資料.藥局代碼, "OPD");
                List<object[]> list_醫囑資料_OPD_Add = new List<object[]>();
                string text = Basic.Net.WEBApiGet("http://10.65.1.229:5003/kwokzoco/opd");

                Console.Write($"讀取醫囑資料(OPD)報表 , 耗時 : {myTimer.ToString()} ms\n");

                List<api_order_opd> list_api_order_opd = text.JsonDeserializet<List<api_order_opd>>();
                List<my_order_opd> list_my_order_OPD = new List<my_order_opd>();
                for (int i = 0; i < list_api_order_opd.Count; i++)
                {
                    my_order_opd my_order_opd = new my_order_opd();
                    my_order_opd.Load(list_api_order_opd[i]);
                    list_my_order_OPD.Add(my_order_opd);
                }

                Console.Write($"轉換醫囑資料(OPD)JsonDeserializet 共{list_api_order_opd.Count}筆資料, 耗時 : {myTimer.ToString()} ms\n");
                Parallel.ForEach(list_my_order_OPD, orderClasses_temp =>
                {
                    List<object[]> list_醫囑資料_OPD_buf = new List<object[]>();
                    list_醫囑資料_OPD_buf = list_醫囑資料_OPD.GetRows((int)enum_醫囑資料.PRI_KEY, orderClasses_temp.PRI_KEY);
                    if (list_醫囑資料_OPD_buf.Count == 0)
                    {
                        DateTime dateTime = orderClasses_temp.開方日期.StringToDateTime();
                        if (dateTime.IsInDate(dateTime_st, dateTime_end))
                        {
                            object[] value_add = new object[new enum_醫囑資料().GetLength()];
                            value_add[(int)enum_醫囑資料.GUID] = Guid.NewGuid().ToString();
                            value_add[(int)enum_醫囑資料.PRI_KEY] = orderClasses_temp.PRI_KEY;
                            value_add[(int)enum_醫囑資料.藥局代碼] = "OPD";
                            value_add[(int)enum_醫囑資料.藥袋條碼] = orderClasses_temp.藥袋條碼;
                            value_add[(int)enum_醫囑資料.藥品碼] = orderClasses_temp.藥品碼;
                            value_add[(int)enum_醫囑資料.藥品名稱] = orderClasses_temp.藥品名稱;
                            value_add[(int)enum_醫囑資料.病人姓名] = orderClasses_temp.病人姓名;
                            value_add[(int)enum_醫囑資料.病歷號] = orderClasses_temp.病歷號;
                            value_add[(int)enum_醫囑資料.交易量] = orderClasses_temp.交易量 * -1;
                            value_add[(int)enum_醫囑資料.開方日期] = orderClasses_temp.開方日期;
                            value_add[(int)enum_醫囑資料.產出時間] = DateTime.Now.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.過帳時間] = DateTime.MinValue.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.狀態] = "未過帳";
                            list_醫囑資料_OPD_Add.LockAdd(value_add);
                        }
                    }
                });
                list_醫囑資料_OPD_Add = list_醫囑資料_OPD_Add.Distinct(new Distinct_order()).ToList();

                Console.Write($"檢查醫囑資料(OPD)完成,共{list_醫囑資料_OPD_Add.Count}筆需更新 , 耗時 : {myTimer.ToString()} ms\n");
                if (list_醫囑資料_OPD_Add.Count > 0) this.sQLControl_醫囑資料.AddRows(null, list_醫囑資料_OPD_Add);
                Console.Write($"醫囑資料(OPD)寫入資料庫完成 , 耗時 : {myTimer.ToString()} ms\n");
                return $"OPD 資料共{list_my_order_OPD.Count}筆,新增{list_醫囑資料_OPD_Add.Count}筆 ,資料庫資料共{list_醫囑資料_OPD.Count}筆\n";
            }
            catch
            {
                Console.Write($"醫囑資料(OPD)更新失敗\n");
                return "醫囑資料(OPD)更新失敗\n";
            }

        }
        private string Function_Get_ipd()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                DateTime dateTime_st = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0);
                DateTime dateTime_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-0).Day, 23, 59, 59);

                List<object[]> list_醫囑資料_IPD = this.sQLControl_醫囑資料.GetRowsByBetween(null, (int)enum_醫囑資料.開方日期, dateTime_st.ToDateTimeString(), dateTime_end.ToDateTimeString());
                list_醫囑資料_IPD = list_醫囑資料_IPD.GetRows((int)enum_醫囑資料.藥局代碼, "IPD");
                List<object[]> list_醫囑資料_IPD_Add = new List<object[]>();
            
                string text = Basic.Net.WEBApiGet("http://10.65.1.229:5003/kwokzoco/ipd");

                Console.Write($"讀取醫囑資料(ipd)報表 , 耗時 : {myTimer.ToString()} ms\n");

                List<api_order_ipd> list_api_order_ipd = text.JsonDeserializet<List<api_order_ipd>>();
                List<my_order_ipd> list_my_order_ipd = new List<my_order_ipd>();
                for (int i = 0; i < list_api_order_ipd.Count; i++)
                {
                    my_order_ipd my_order_ipd = new my_order_ipd();
                    my_order_ipd.Load(list_api_order_ipd[i]);
                    list_my_order_ipd.Add(my_order_ipd);
                }

                Console.Write($"轉換醫囑資料(ipd)JsonDeserializet 共{list_api_order_ipd.Count}筆資料, 耗時 : {myTimer.ToString()} ms\n");
                Parallel.ForEach(list_my_order_ipd, orderClasses_temp =>
                {
                    List<object[]> list_醫囑資料_IPD_buf = new List<object[]>();
                    list_醫囑資料_IPD_buf = list_醫囑資料_IPD.GetRows((int)enum_醫囑資料.PRI_KEY, orderClasses_temp.PRI_KEY);
                    if (list_醫囑資料_IPD_buf.Count == 0 )
                    {
                        DateTime dateTime = orderClasses_temp.開方日期.StringToDateTime();
                        if (dateTime.IsInDate(dateTime_st, dateTime_end))
                        {
                            object[] value_add = new object[new enum_醫囑資料().GetLength()];
                            value_add[(int)enum_醫囑資料.GUID] = Guid.NewGuid().ToString();
                            value_add[(int)enum_醫囑資料.PRI_KEY] = orderClasses_temp.PRI_KEY;
                            value_add[(int)enum_醫囑資料.藥局代碼] = "IPD";
                            value_add[(int)enum_醫囑資料.藥袋條碼] = orderClasses_temp.藥袋條碼;
                            value_add[(int)enum_醫囑資料.藥品碼] = orderClasses_temp.藥品碼;
                            value_add[(int)enum_醫囑資料.藥品名稱] = orderClasses_temp.藥品名稱;
                            value_add[(int)enum_醫囑資料.病人姓名] = orderClasses_temp.病人姓名;
                            value_add[(int)enum_醫囑資料.病歷號] = orderClasses_temp.病歷號;
                            value_add[(int)enum_醫囑資料.交易量] = orderClasses_temp.交易量 * -1;
                            value_add[(int)enum_醫囑資料.開方日期] = orderClasses_temp.開方日期;
                            value_add[(int)enum_醫囑資料.產出時間] = DateTime.Now.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.過帳時間] = DateTime.MinValue.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.狀態] = "未過帳";
                            list_醫囑資料_IPD_Add.LockAdd(value_add);
                        }
                    }
                });
                list_醫囑資料_IPD_Add = list_醫囑資料_IPD_Add.Distinct(new Distinct_order()).ToList();
                Console.Write($"檢查醫囑資料(ipd)完成,共{list_醫囑資料_IPD_Add.Count}筆需更新 , 耗時 : {myTimer.ToString()} ms\n");
                if (list_醫囑資料_IPD_Add.Count > 0) this.sQLControl_醫囑資料.AddRows(null, list_醫囑資料_IPD_Add);
                Console.Write($"醫囑資料(ipd)寫入資料庫完成 , 耗時 : {myTimer.ToString()} ms\n");
                return $"IPD 資料共{list_my_order_ipd.Count}筆,新增{list_醫囑資料_IPD_Add.Count}筆 ,資料庫資料共{list_醫囑資料_IPD.Count}筆\n";
            }
            catch
            {
                Console.Write($"醫囑資料(ipd)更新失敗\n");
                return "醫囑資料(ipd)更新失敗\n";
            }

        }
        private string Function_Get_cpd()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                DateTime dateTime_st = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0);
                DateTime dateTime_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-0).Day, 23, 59, 59);

                List<object[]> list_醫囑資料_CPD = this.sQLControl_醫囑資料.GetRowsByBetween(null, (int)enum_醫囑資料.開方日期, dateTime_st.ToDateTimeString(), dateTime_end.ToDateTimeString());
                list_醫囑資料_CPD = list_醫囑資料_CPD.GetRows((int)enum_醫囑資料.藥局代碼, "CPD");
                List<object[]> list_醫囑資料_CPD_Add = new List<object[]>();
                string text = Basic.Net.WEBApiGet("http://10.65.1.229:5003/kwokzoco/cpd");

                Console.Write($"讀取醫囑資料(cpd)報表 , 耗時 : {myTimer.ToString()} ms\n");

                List<api_order_cpd> list_api_order_cpd = text.JsonDeserializet<List<api_order_cpd>>();
                List<my_order_cpd> list_my_order_CPD = new List<my_order_cpd>();
                for (int i = 0; i < list_api_order_cpd.Count; i++)
                {
                    my_order_cpd my_order_cpd = new my_order_cpd();
                    my_order_cpd.Load(list_api_order_cpd[i]);
                    list_my_order_CPD.Add(my_order_cpd);
                }

                Console.Write($"轉換醫囑資料(cpd)JsonDeserializet 共{list_api_order_cpd.Count}筆資料, 耗時 : {myTimer.ToString()} ms\n");
                Parallel.ForEach(list_my_order_CPD, orderClasses_temp =>
                {
                    List<object[]> list_醫囑資料_CPD_buf = new List<object[]>();
                    list_醫囑資料_CPD_buf = list_醫囑資料_CPD.GetRows((int)enum_醫囑資料.PRI_KEY, orderClasses_temp.PRI_KEY);
                    if (list_醫囑資料_CPD_buf.Count == 0 )
                    {
                        DateTime dateTime = orderClasses_temp.開方日期.StringToDateTime();
                        if (dateTime.IsInDate(dateTime_st, dateTime_end))
                        {
                            object[] value_add = new object[new enum_醫囑資料().GetLength()];
                            value_add[(int)enum_醫囑資料.GUID] = Guid.NewGuid().ToString();
                            value_add[(int)enum_醫囑資料.PRI_KEY] = orderClasses_temp.PRI_KEY;
                            value_add[(int)enum_醫囑資料.藥局代碼] = "CPD";
                            value_add[(int)enum_醫囑資料.藥袋條碼] = orderClasses_temp.藥袋條碼;
                            value_add[(int)enum_醫囑資料.藥品碼] = orderClasses_temp.藥品碼;
                            value_add[(int)enum_醫囑資料.藥品名稱] = orderClasses_temp.藥品名稱;
                            value_add[(int)enum_醫囑資料.病人姓名] = orderClasses_temp.病人姓名;
                            value_add[(int)enum_醫囑資料.病歷號] = orderClasses_temp.病歷號;
                            value_add[(int)enum_醫囑資料.交易量] = orderClasses_temp.交易量 * -1;
                            value_add[(int)enum_醫囑資料.開方日期] = orderClasses_temp.開方日期;
                            value_add[(int)enum_醫囑資料.產出時間] = DateTime.Now.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.過帳時間] = DateTime.MinValue.ToDateTimeString_6();
                            value_add[(int)enum_醫囑資料.狀態] = "未過帳";
                            list_醫囑資料_CPD_Add.LockAdd(value_add);
                        }
                    }
                });
                list_醫囑資料_CPD_Add = list_醫囑資料_CPD_Add.Distinct(new Distinct_order()).ToList();
                Console.Write($"檢查醫囑資料(cpd)完成,共{list_醫囑資料_CPD_Add.Count}筆需更新 , 耗時 : {myTimer.ToString()} ms\n");
                if (list_醫囑資料_CPD_Add.Count > 0) this.sQLControl_醫囑資料.AddRows(null, list_醫囑資料_CPD_Add);
                Console.Write($"醫囑資料(cpd)寫入資料庫完成 , 耗時 : {myTimer.ToString()} ms\n");
                return $"CPD 資料共{list_my_order_CPD.Count}筆,新增{list_醫囑資料_CPD_Add.Count}筆 ,資料庫資料共{list_醫囑資料_CPD.Count}筆\n";
            }
            catch
            {
                Console.Write($"醫囑資料(cpd)更新失敗\n");
                return "醫囑資料(cpd)更新失敗\n";
            }

        }
        public class Distinct_order : IEqualityComparer<object[]>
        {
            public bool Equals(object[] x, object[] y)
            {
                return (x[(int)enum_醫囑資料.PRI_KEY].ObjectToString() == y[(int)enum_醫囑資料.PRI_KEY].ObjectToString());
            }

            public int GetHashCode(object[] obj)
            {
                return 1;
            }
        }
    }
}
