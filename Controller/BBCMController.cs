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
using Oracle.ManagedDataAccess.Client;
using System.Text.Json;
using System.Text.Encodings;
using System.Text.Json.Serialization;
namespace DB2VM.Controller
{
    public enum enum_雲端藥檔
    {
        GUID,
        藥品碼,
        料號,
        中文名稱,
        藥品名稱,
        藥品學名,
        健保碼,
        包裝單位,
        包裝數量,
        最小包裝單位,
        最小包裝數量,
        藥品條碼1,
        藥品條碼2,
        警訊藥品,
        管制級別,
        類別,
    }
    public class medApi
    {
        [JsonPropertyName("MED_CODE")]
        public string 藥品碼 { get; set; }
        [JsonPropertyName("MED_DESC")]
        public string 藥品名稱 { get; set; }
        [JsonPropertyName("ALISE_DESC")]
        public string 藥品學名 { get; set; }
        [JsonPropertyName("INS_MED_CODE")]
        public string 料號 { get; set; }
        [JsonPropertyName("BARCODE")]
        public string 藥品條碼1 { get; set; }
        [JsonPropertyName("FEE_UNIT_DESC")]
        public string 包裝單位 { get; set; }
    }
    [Route("dbvm/[controller]")]
    [ApiController]
    public class BBCMController : ControllerBase
    {
        private SQLControl sQLControl_UDSDBBCM = new SQLControl(MySQL_server, MySQL_database, "medicine_page_cloud", MySQL_userid, MySQL_password, (uint)MySQL_port.StringToInt32(), MySql.Data.MySqlClient.MySqlSslMode.None);

        static string MySQL_server = $"{ConfigurationManager.AppSettings["MySQL_server"]}";
        static string MySQL_database = $"{ConfigurationManager.AppSettings["MySQL_database"]}";
        static string MySQL_userid = $"{ConfigurationManager.AppSettings["MySQL_user"]}";
        static string MySQL_password = $"{ConfigurationManager.AppSettings["MySQL_password"]}";
        static string MySQL_port = $"{ConfigurationManager.AppSettings["MySQL_port"]}";
        [HttpGet]
        public string Get(string? Code)
        {
            string jsonString = Basic.Net.WEBApiGet("http://10.65.1.229:5003/kwokzoco/med");
            List<medApi> list_medApi = jsonString.JsonDeserializet<List<medApi>>();
            List<object[]> list_v_hisdrugdia = new List<object[]>();
            if (Code.StringIsEmpty() ==false)
            {
                list_medApi = (from value in list_medApi
                               where value.藥品碼 == Code
                               select value).ToList();
            }
            for(int i = 0; i < list_medApi.Count; i++)
            {
                object[] value = new object[new enum_雲端藥檔().GetLength()];
                if (list_medApi[i].藥品碼 == null) continue;
                if (list_medApi[i].料號 == null) list_medApi[i].料號 = "";
                if (list_medApi[i].藥品名稱 == null) list_medApi[i].藥品名稱 = "";
                if (list_medApi[i].藥品學名 == null) list_medApi[i].藥品學名 = "";
                if (list_medApi[i].包裝單位 == null) list_medApi[i].包裝單位 = "";
                value[(int)enum_雲端藥檔.藥品碼] = list_medApi[i].藥品碼.Trim();
                value[(int)enum_雲端藥檔.料號] = list_medApi[i].料號.Trim();
                value[(int)enum_雲端藥檔.藥品名稱] = list_medApi[i].藥品名稱.Trim();
                value[(int)enum_雲端藥檔.藥品學名] = list_medApi[i].藥品學名.Trim();
                value[(int)enum_雲端藥檔.包裝單位] = list_medApi[i].包裝單位.Trim();
                value[(int)enum_雲端藥檔.包裝數量] = "1";
                value[(int)enum_雲端藥檔.最小包裝數量] = "1";
                //value[(int)enum_雲端藥檔.藥品條碼1] = reader["DIA_SKDIACODE"].ToString().Trim();
                //value[(int)enum_雲端藥檔.藥品條碼2] = reader["DIA_SKDIACODE"].ToString().Trim();
                value[(int)enum_雲端藥檔.警訊藥品] = "False";
                value[(int)enum_雲端藥檔.管制級別] = "N";
                value[(int)enum_雲端藥檔.類別] = "";
                list_v_hisdrugdia.Add(value);
            }

            List<object[]> list_BBCM = sQLControl_UDSDBBCM.GetAllRows(null);
            List<object[]> list_BBCM_buf = new List<object[]>();
            List<object[]> list_BBCM_Add = new List<object[]>();
            List<object[]> list_BBCM_Replace = new List<object[]>();
            for (int i = 0; i < list_v_hisdrugdia.Count; i++)
            {
                list_BBCM_buf = list_BBCM.GetRows((int)enum_雲端藥檔.藥品碼, list_v_hisdrugdia[i][(int)enum_雲端藥檔.藥品碼].ObjectToString());
                if (list_BBCM_buf.Count == 0)
                {
                    list_v_hisdrugdia[i][(int)enum_雲端藥檔.GUID] = Guid.NewGuid().ToString();
                    list_BBCM_Add.Add(list_v_hisdrugdia[i]);
                }
                else
                {
                    list_v_hisdrugdia[i][(int)enum_雲端藥檔.GUID] = list_BBCM_buf[0][(int)enum_雲端藥檔.GUID].ObjectToString();
                    if (!list_v_hisdrugdia[i].IsEqual(list_BBCM_buf[0], (int)enum_雲端藥檔.藥品條碼1, (int)enum_雲端藥檔.藥品條碼2))
                    {
                        list_v_hisdrugdia[i][(int)enum_雲端藥檔.藥品條碼1] = list_BBCM_buf[0][(int)enum_雲端藥檔.藥品條碼1];
                        list_v_hisdrugdia[i][(int)enum_雲端藥檔.藥品條碼2] = list_BBCM_buf[0][(int)enum_雲端藥檔.藥品條碼2];
                        list_BBCM_Replace.Add(list_v_hisdrugdia[i]);
                    }


                }
            }
            if (list_BBCM_Add.Count > 0) sQLControl_UDSDBBCM.AddRows(null, list_BBCM_Add);
            if (list_BBCM_Replace.Count > 0) sQLControl_UDSDBBCM.UpdateByDefulteExtra(null, list_BBCM_Replace);


            return list_medApi.JsonSerializationt();
           
        }
    }
}
