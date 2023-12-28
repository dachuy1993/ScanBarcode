using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApplication.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Shift { get; set; }
        public string ID { get; set; }
    }

    public class BarcodeScan
    {
        public int BarcodeID { get; set; }
        public int ResultID { get; set; }
        public string BarcodeSS { get; set; }
        public string Barcode { get; set; }
        public string BarcodeSX { get; set; }
        
        public DateTime TimeBarcode { get; set; }
        public Color Color { get; set; }
        public string Status { get; set; }
    }

 

    public class BarcodeScanResult
    {
        public string Barcode { get; set; }
        public int ResultID { get; set; }
        public int Num { get; set; }
        public string EmpId { get; set; }
        public string EmpNm { get; set; }
        public string Shift { get; set; }
        public DateTime TimeBarcode { get; set; }
        //public List<BarcodeScanDetailResult> BarcodeScanResultDetailList { get; set; }
    }

    public class BarcodeScanResultHist
    {
        public DateTime TimeBarcode { get; set; }
        public List<BarcodeScanDetailResult> BarcodeScanResultDetailList { get; set; }
    }

    public class BarcodeScanDetailResult
    {
        public string BarcodeD { get; set; }
        public int NumD { get; set; }
        public string EmpIdD { get; set; }
        public string EmpNmD { get; set; }
        public string ShiftD { get; set; }
    }


    public class TitleResult
    {
        public string TitleBarcode { get; set; }
        public string TitleNum { get; set; }
    }
    public class TitleScan
    {
        public string TitleBarcode { get; set; }
        public string BarcodeStan { get;set; }
        public string BarcodePro { get; set;}
    }
   
    public class TitleHis
    {
        public string ItemNo { get; set; }
        public string Num { get; set; }
        public string EmpNo { get; set; }
        public string EmpNm { get; set; }
        public string Shift { get; set; }
    }

    public class ResultHis
    {
        public string ItemNo { get; set; }
        public string Num { get; set; }
        public string EmpNo { get; set; }
        public string EmpNm { get; set; }
        public string Shift { get; set; }
    }

    public class LoginUser
    {
        public string User { get;set; }
        public string Pass { get; set; }
        public string EmpId { get; set; }
        public string EmpNm { get; set; }
    }

    public class LoginJson
    {
        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Pass")]
        public string Pass { get; set; }

        [JsonProperty("EmpId")]
        public string EmpId { get; set; }

        [JsonProperty("EmpNm")]
        public string EmpNm { get; set; }

    }

    public class BarcodeScanListJson
    {
        [JsonProperty("BarcodeID")]
        public int BarcodeID { get; set; }
        [JsonProperty("ResultID")]
        public int ResultID { get; set; }   
        [JsonProperty("BarcodeSS")]
        public string BarcodeSS { get; set; }
        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
        [JsonProperty("BarcodeSX")]
        public string BarcodeSX { get; set; }
        [JsonProperty("TimeBarcode")]

        public DateTime TimeBarcode { get; set; }
        [JsonProperty("Color")]
        public string Color { get; set; }
        public string Status { get; set; }
    }

    public class BarcodeScanResultJson
    {
        [JsonProperty("Barcode")]
        public string Barcode { get; set; }
        [JsonProperty("ResultID")]
        public int ResultID { get; set; }
        [JsonProperty("Num")]
        public int Num { get; set; }

        [JsonProperty("EmpId")]
        public string EmpId { get; set; }

        [JsonProperty("EmpNm")]
        public string EmpNm { get; set; }

        [JsonProperty("Shift")]
        public string Shift { get; set; }

        [JsonProperty("TimeBarcode")]
        public DateTime TimeBarcode { get; set; }
        
        

    }

    public class Shift
    {
        public string ShiftNm { get; set; }
    }

    public class ToFrDate
    {
        public DateTime ToDate { get; set; }
        public DateTime FrDate { get; set; }
    }

}
