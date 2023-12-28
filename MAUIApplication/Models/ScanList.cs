
using SQLite;

namespace MAUIApplication.Models
{
    public class TblScanList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BarcodeSS { get; set; }
        public string Barcode { get;set; }
        public string BarcodeSX { get; set; }
        public int BarcodeID { get; set; }   
        public string TimeBarcode { get; set; }

    }

    public class TblScanResultList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Num { get; set; }
        public int BarcodeID { get; set; }
    }
    public class TblScanInfo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string BarcodeID { get; set; }
        public string UserLog { get; set; }
        public string ShiftLog { get; set; }
        public string TimeLog { get; set; }
    }
    public class TblUser
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }  
    }

}
