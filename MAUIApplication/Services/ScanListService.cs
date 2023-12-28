
using System.Data.SqlClient;
using System.Data;
using MAUIApplication.Services;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIApplication.Models;
[assembly:Dependency(typeof(ScanListService))]
namespace MAUIApplication.Services
{
    public static class ScanListService
    {
        //static SQLiteAsyncConnection db;
        //public static string path_Ksystem20 = "Data Source=192.168.2.20;Initial Catalog=TAIXINERP;Persist Security Info=True;User ID=sa;Password= Ksystem@123";
        static async Task Init()
        {
            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "SqliteScanDBTest1.db");
        }

        public static class Constants
        {
            public const string DatabaseFilename = "SqliteScanDBTest1.db";

            public const SQLite.SQLiteOpenFlags Flags =
                // open the database in read/write mode
                SQLite.SQLiteOpenFlags.ReadWrite |
                // create the database if it doesn't exist
                SQLite.SQLiteOpenFlags.Create |
                // enable multi-threaded database access
                SQLite.SQLiteOpenFlags.SharedCache;

            public static string DatabasePath =>
                Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        }
        public static async Task AddScanList(string barcodeSS, string barcode, string barcodeSX, string timebarcode)
        {
            await Init();
            
            var tblScanList = new TblScanList
            {
                BarcodeSS = barcodeSS,
                Barcode = barcode,
               BarcodeSX = barcodeSX,
                //BarcodeID = barcodeID,
                TimeBarcode = timebarcode
            };

           
            //var id = await db.InsertAsync(tblScanList);

        }
        public static async Task RemoveScan(int id)
        {
            await Init();
            //await db.DeleteAsync(id);

        }
      

        //public async Task AddScanList(string barcodeSS, string barcode, string barcodeSX, string timebarcode)
        //{
        //    await Init();
        //    var tblScanList = new TblScanList
        //    {
        //        BarcodeSS = barcodeSS,
        //        Barcode = barcode,
        //        BarcodeSX = barcodeSX,
        //        //BarcodeID = barcodeID,
        //        TimeBarcode = timebarcode
        //    };

        //    var id = await db.InsertAsync(tblScanList);
        //}

        public static Task<IEnumerable<TblScanList>> GetScanLists()
        {
            throw new NotImplementedException();
        }

        public static Task<TblScanList> GetScanList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
