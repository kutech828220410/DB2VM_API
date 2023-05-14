using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings;
using System.Text.Json.Serialization;
using Basic;
namespace DB2VM
{

    public class my_order_cpd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private int tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string eXEC_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private string bARCODE = "";

        public string 藥品碼 { get => mED_CODE; set => mED_CODE = value; }
        public string 藥品名稱 { get => mED_DESC; set => mED_DESC = value; }
        public string 包裝單位 { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public int 交易量 { get => tTL_QTY; set => tTL_QTY = value; }
        public string 病歷號 { get => cHR_NO; set => cHR_NO = value; }
        public string 病人姓名 { get => pAT_NAME; set => pAT_NAME = value; }
        public string 開方日期 { get => eXEC_DATE; set => eXEC_DATE = value; }
        public string 醫師姓名 { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => bARCODE; set => bARCODE = value; }

        public void Load(api_order_cpd api_order_cpd)
        {
            try
            {
                this.藥品碼 = api_order_cpd.MED_CODE.Trim();
                this.藥品名稱 = api_order_cpd.MED_DESC.Trim();
                this.包裝單位 = api_order_cpd.FEE_UNIT_DESC.Trim();
                if (api_order_cpd.TTL_QTY is JsonElement)
                {
                    api_order_cpd.TTL_QTY = (int)((JsonElement)api_order_cpd.TTL_QTY).GetDouble();
                }
                this.交易量 = api_order_cpd.TTL_QTY.ObjectToString().StringToInt32(); ;

                this.病歷號 = api_order_cpd.CHR_NO.Trim();
                this.病人姓名 = api_order_cpd.PAT_NAME.Trim();
                this.開方日期 = api_order_cpd.EXEC_DATE.Trim();
                DateTime dt = DateTime.ParseExact(this.開方日期, "yyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddYears(1911);
                this.開方日期 = dt.ToDateString();
                this.醫師姓名 = api_order_cpd.DOC_NAME.Trim();
                this.PRI_KEY = api_order_cpd.PRI_KEY.Trim();
                if (api_order_cpd.BARCODE is JsonElement)
                {
                    if (((JsonElement)api_order_cpd.BARCODE).ValueKind == JsonValueKind.String) api_order_cpd.BARCODE = ((JsonElement)api_order_cpd.BARCODE).GetString();
                    else if (((JsonElement)api_order_cpd.BARCODE).ValueKind == JsonValueKind.Number) api_order_cpd.BARCODE = ((long)((JsonElement)api_order_cpd.BARCODE).GetDouble()).ToString();
                }
                this.藥袋條碼 = api_order_cpd.BARCODE.ObjectToString().Trim();
            }
            catch
            {

            }

        }
    }
    public class api_order_cpd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private object tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string eXEC_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private object bARCODE = "";

        public string MED_CODE { get => mED_CODE; set => mED_CODE = value; }
        public string MED_DESC { get => mED_DESC; set => mED_DESC = value; }
        public string FEE_UNIT_DESC { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public object TTL_QTY { get => tTL_QTY; set => tTL_QTY = value; }
        public string CHR_NO { get => cHR_NO; set => cHR_NO = value; }
        public string PAT_NAME { get => pAT_NAME; set => pAT_NAME = value; }
        public string EXEC_DATE { get => eXEC_DATE; set => eXEC_DATE = value; }
        public string DOC_NAME { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public object BARCODE { get => bARCODE; set => bARCODE = value; }
    }

    public class my_order_ipd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private int tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string eXEC_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private string bARCODE = "";

        public string 藥品碼 { get => mED_CODE; set => mED_CODE = value; }
        public string 藥品名稱 { get => mED_DESC; set => mED_DESC = value; }
        public string 包裝單位 { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public int 交易量 { get => tTL_QTY; set => tTL_QTY = value; }
        public string 病歷號 { get => cHR_NO; set => cHR_NO = value; }
        public string 病人姓名 { get => pAT_NAME; set => pAT_NAME = value; }
        public string 開方日期 { get => eXEC_DATE; set => eXEC_DATE = value; }
        public string 醫師姓名 { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => bARCODE; set => bARCODE = value; }

        public void Load(api_order_ipd api_order_ipd)
        {
            try
            {
                this.藥品碼 = api_order_ipd.MED_CODE.Trim();
                this.藥品名稱 = api_order_ipd.MED_DESC.Trim();
                this.包裝單位 = api_order_ipd.FEE_UNIT_DESC.Trim();
                if (api_order_ipd.TTL_QTY is JsonElement)
                {
                    api_order_ipd.TTL_QTY = (int)((JsonElement)api_order_ipd.TTL_QTY).GetDouble();
                }
                this.交易量 = api_order_ipd.TTL_QTY.ObjectToString().StringToInt32(); ;

                this.病歷號 = api_order_ipd.CHR_NO.Trim();
                this.病人姓名 = api_order_ipd.PAT_NAME.Trim();
                this.開方日期 = api_order_ipd.EXEC_DATE.Trim();
                DateTime dt = DateTime.ParseExact(this.開方日期, "yyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddYears(1911);
                this.開方日期 = dt.ToDateString();
                this.醫師姓名 = api_order_ipd.DOC_NAME.Trim();
                this.PRI_KEY = api_order_ipd.PRI_KEY.Trim();
                if (api_order_ipd.BARCODE is JsonElement)
                {
                    if (((JsonElement)api_order_ipd.BARCODE).ValueKind == JsonValueKind.String) api_order_ipd.BARCODE = ((JsonElement)api_order_ipd.BARCODE).GetString();
                    else if (((JsonElement)api_order_ipd.BARCODE).ValueKind == JsonValueKind.Number) api_order_ipd.BARCODE = ((long)((JsonElement)api_order_ipd.BARCODE).GetDouble()).ToString();
                }
                this.藥袋條碼 = api_order_ipd.BARCODE.ObjectToString().Trim();
            }
            catch
            {

            }

        }
    }
    public class api_order_ipd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private object tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string eXEC_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private object bARCODE = "";

        public string MED_CODE { get => mED_CODE; set => mED_CODE = value; }
        public string MED_DESC { get => mED_DESC; set => mED_DESC = value; }
        public string FEE_UNIT_DESC { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public object TTL_QTY { get => tTL_QTY; set => tTL_QTY = value; }
        public string CHR_NO { get => cHR_NO; set => cHR_NO = value; }
        public string PAT_NAME { get => pAT_NAME; set => pAT_NAME = value; }
        public string EXEC_DATE { get => eXEC_DATE; set => eXEC_DATE = value; }
        public string DOC_NAME { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public object BARCODE { get => bARCODE; set => bARCODE = value; }
    }

    public class my_order_opd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private int tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string oPD_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private string bARCODE = "";

        public string 藥品碼 { get => mED_CODE; set => mED_CODE = value; }
        public string 藥品名稱 { get => mED_DESC; set => mED_DESC = value; }
        public string 包裝單位 { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public int 交易量 { get => tTL_QTY; set => tTL_QTY = value; }
        public string 病歷號 { get => cHR_NO; set => cHR_NO = value; }
        public string 病人姓名 { get => pAT_NAME; set => pAT_NAME = value; }
        public string 開方日期 { get => oPD_DATE; set => oPD_DATE = value; }
        public string 醫師姓名 { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => bARCODE; set => bARCODE = value; }

        public void Load(api_order_opd api_order_opd)
        {
            try
            {
                this.藥品碼 = api_order_opd.MED_CODE.Trim();
                this.藥品名稱 = api_order_opd.MED_DESC.Trim();
                this.包裝單位 = api_order_opd.FEE_UNIT_DESC.Trim();
                if (api_order_opd.TTL_QTY is JsonElement)
                {
                    api_order_opd.TTL_QTY = (int)((JsonElement)api_order_opd.TTL_QTY).GetDouble();
                }
                this.交易量 = api_order_opd.TTL_QTY.ObjectToString().StringToInt32(); ;

                this.病歷號 = api_order_opd.CHR_NO.Trim();
                this.病人姓名 = api_order_opd.PAT_NAME.Trim();
                this.開方日期 = api_order_opd.OPD_DATE.Trim();
                DateTime dt = DateTime.ParseExact(this.開方日期, "yyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddYears(1911);
                this.開方日期 = dt.ToDateString();
                this.醫師姓名 = api_order_opd.DOC_NAME.Trim();
                this.PRI_KEY = api_order_opd.PRI_KEY.Trim();
                if (api_order_opd.BARCODE is JsonElement)
                {
                    if (((JsonElement)api_order_opd.BARCODE).ValueKind == JsonValueKind.String) api_order_opd.BARCODE = ((JsonElement)api_order_opd.BARCODE).GetString();
                    else if (((JsonElement)api_order_opd.BARCODE).ValueKind == JsonValueKind.Number) api_order_opd.BARCODE = ((long)((JsonElement)api_order_opd.BARCODE).GetDouble()).ToString();
                }
                this.藥袋條碼 = api_order_opd.BARCODE.ObjectToString().Trim();
            }
            catch
            {

            }

        }
    }
    public class api_order_opd
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private object tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string oPD_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private object bARCODE = "";

        public string MED_CODE { get => mED_CODE; set => mED_CODE = value; }
        public string MED_DESC { get => mED_DESC; set => mED_DESC = value; }
        public string FEE_UNIT_DESC { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public object TTL_QTY { get => tTL_QTY; set => tTL_QTY = value; }
        public string CHR_NO { get => cHR_NO; set => cHR_NO = value; }
        public string PAT_NAME { get => pAT_NAME; set => pAT_NAME = value; }
        public string OPD_DATE { get => oPD_DATE; set => oPD_DATE = value; }
        public string DOC_NAME { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public object BARCODE { get => bARCODE; set => bARCODE = value; }
    }

    public class my_order_er
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private int tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string oPD_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private string bARCODE = "";

        public string 藥品碼 { get => mED_CODE; set => mED_CODE = value; }
        public string 藥品名稱 { get => mED_DESC; set => mED_DESC = value; }
        public string 包裝單位 { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public int 交易量 { get => tTL_QTY; set => tTL_QTY = value; }
        public string 病歷號 { get => cHR_NO; set => cHR_NO = value; }
        public string 病人姓名 { get => pAT_NAME; set => pAT_NAME = value; }
        public string 開方日期 { get => oPD_DATE; set => oPD_DATE = value; }
        public string 醫師姓名 { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => bARCODE; set => bARCODE = value; }

        public void Load(api_order_er api_order_er)
        {
            try
            {
                this.藥品碼 = api_order_er.MED_CODE.Trim();
                this.藥品名稱 = api_order_er.MED_DESC.Trim();
                this.包裝單位 = api_order_er.FEE_UNIT_DESC.Trim();
                if (api_order_er.TTL_QTY is JsonElement)
                {
                    api_order_er.TTL_QTY = (int)((JsonElement)api_order_er.TTL_QTY).GetDouble();
                }
                this.交易量 = api_order_er.TTL_QTY.ObjectToString().StringToInt32(); ;

                this.病歷號 = api_order_er.CHR_NO.Trim();
                this.病人姓名 = api_order_er.PAT_NAME.Trim();
                this.開方日期 = api_order_er.OPD_DATE.Trim();
                DateTime dt = DateTime.ParseExact(this.開方日期, "yyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddYears(1911);
                this.開方日期 = dt.ToDateString();
                this.醫師姓名 = api_order_er.DOC_NAME.Trim();
                this.PRI_KEY = api_order_er.PRI_KEY.Trim();
                if (api_order_er.BARCODE is JsonElement)
                {
                    if (((JsonElement)api_order_er.BARCODE).ValueKind == JsonValueKind.String) api_order_er.BARCODE = ((JsonElement)api_order_er.BARCODE).GetString();
                    else if (((JsonElement)api_order_er.BARCODE).ValueKind == JsonValueKind.Number) api_order_er.BARCODE = ((long)((JsonElement)api_order_er.BARCODE).GetDouble()).ToString();
                }
                this.藥袋條碼 = api_order_er.BARCODE.ObjectToString().Trim();
            }
            catch
            {

            }

        }
    }
    public class api_order_er
    {
        private string mED_CODE = "";
        private string mED_DESC = "";
        private string fEE_UNIT_DESC = "";
        private object tTL_QTY = 0;
        private string cHR_NO = "";
        private string pAT_NAME = "";
        private string oPD_DATE = "";
        private string dOC_NAME = "";
        private string pRI_KEY = "";
        private object bARCODE = "";

        public string MED_CODE { get => mED_CODE; set => mED_CODE = value; }
        public string MED_DESC { get => mED_DESC; set => mED_DESC = value; }
        public string FEE_UNIT_DESC { get => fEE_UNIT_DESC; set => fEE_UNIT_DESC = value; }
        public object TTL_QTY { get => tTL_QTY; set => tTL_QTY = value; }
        public string CHR_NO { get => cHR_NO; set => cHR_NO = value; }
        public string PAT_NAME { get => pAT_NAME; set => pAT_NAME = value; }
        public string OPD_DATE { get => oPD_DATE; set => oPD_DATE = value; }
        public string DOC_NAME { get => dOC_NAME; set => dOC_NAME = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public object BARCODE { get => bARCODE; set => bARCODE = value; }
    }
    [Serializable]
    public class OrderClass
    {
        private string _藥局代碼 = "";
        private string _藥品碼 = "";
        private string _藥品名稱 = "";
        private string _包裝單位 = "";
        private string _交易量 = "";
        private string _病歷號 = "";
        private string _開方時間 = "";
        private string pRI_KEY = "";
        private string _藥袋條碼 = "";
        private string _劑量 = "";
        private string _頻次 = "";
        private string _途徑 = "";
        private string _天數 = "";
        private string _處方序號 = "";
        private string _病人姓名 = "";

        public string 藥局代碼 { get => _藥局代碼; set => _藥局代碼 = value; }
        public string 藥品碼 { get => _藥品碼; set => _藥品碼 = value; }
        public string 藥品名稱 { get => _藥品名稱; set => _藥品名稱 = value; }
        public string 包裝單位 { get => _包裝單位; set => _包裝單位 = value; }
        public string 交易量 { get => _交易量; set => _交易量 = value; }
        public string 病歷號 { get => _病歷號; set => _病歷號 = value; }
        public string 開方時間 { get => _開方時間; set => _開方時間 = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => _藥袋條碼; set => _藥袋條碼 = value; }
        public string 劑量 { get => _劑量; set => _劑量 = value; }
        public string 頻次 { get => _頻次; set => _頻次 = value; }
        public string 途徑 { get => _途徑; set => _途徑 = value; }
        public string 天數 { get => _天數; set => _天數 = value; }
        public string 處方序號 { get => _處方序號; set => _處方序號 = value; }
        public string 病人姓名 { get => _病人姓名; set => _病人姓名 = value; }
    }
}
