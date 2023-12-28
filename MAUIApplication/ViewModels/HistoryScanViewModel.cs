using CommunityToolkit.Mvvm.Input;
using MAUIApplication.Models;
using MAUIApplication.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MAUIApplication.Views;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DevExpress.Maui.Core.Internal;

namespace MAUIApplication.ViewModels
{
    public partial class HistoryScanViewModel: BaseViewModel
    {
        public static DateTime DateId { get; set; }
        public static int MaxId { get;set; }
        public ObservableCollection<TitleHis> ListviewBarcodeTitleHis { get;} = new();
        public ObservableCollection<ResultHis> ListviewResultHis { get;} = new();
        public ObservableCollection<BarcodeScanResult> QRCodeCheckResults { get; } = new();
        public ObservableCollection<BarcodeScanDetailResult> BarcodeScanResultDetailList { get; } = new();
        public ObservableCollection<BarcodeScanResult> BarcodeScanResultHis { get;} = new();
        private readonly IAPIService _apiservice = new APIService();

        ObservableCollection<BarcodeScanResult> barcodeScanResultList;
        public ObservableCollection<BarcodeScanResult> BarcodeScanResultList
        {
            get => barcodeScanResultList;
            private set
            {
                barcodeScanResultList = value;
                OnPropertyChanged(nameof(BarcodeScanResultList));
            }
        }

        ObservableCollection<BarcodeScanResult> allResults;
        public ObservableCollection<BarcodeScanResult> AllResults
        {
            get => allResults;
            private set
            {
                allResults = value;
                OnPropertyChanged(nameof(AllResults));
            }
        }

        DateTime dateFrom;
        public DateTime DateFrom
        {
            get => dateFrom;
            set => SetProperty(ref dateFrom, value);
        }

        DateTime dateTo;
        public DateTime DateTo
        {
            get => dateTo;
            set => SetProperty(ref dateTo, value);
        }
        public HistoryScanViewModel() 
        {
            dateFrom = DateTime.Now.Date;
            dateTo = DateTime.Now.Date;
            TitleCollection();
            AllResults = new();
            BarcodeScanResultList = new();
        }
        [RelayCommand]
        public async Task SearchResult()
        {
            try
            {
                string uri = "ScanResults/GetForDate";

                ToFrDate toFrDate = new ToFrDate();
                toFrDate.ToDate = dateTo.AddDays(1);
                toFrDate.FrDate = dateFrom;
                
                //List<BarcodeScanResult> barcodeScanResultList = new List<BarcodeScanResult>();

                var response = await _apiservice.Connect("GET", $"{uri}?dateFrom={toFrDate.FrDate}&dateTo={toFrDate.ToDate}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<BarcodeScanResultJson> barcodeScanResultJson = await response.Content.ReadFromJsonAsync<List<BarcodeScanResultJson>>();
                    if(AllResults.Count > 0)
                        AllResults.Clear();
                    if(BarcodeScanResultList.Count > 0)
                        BarcodeScanResultList.Clear();
                    for (int i = 0; i < barcodeScanResultJson.Count; i++)
                    {
                        BarcodeScanResult barcodeScanResult1 = new BarcodeScanResult();
                        barcodeScanResult1.Barcode = barcodeScanResultJson[i].Barcode;
                        barcodeScanResult1.ResultID = barcodeScanResultJson[i].ResultID;
                        barcodeScanResult1.Num = barcodeScanResultJson[i].Num;
                        barcodeScanResult1.EmpId = barcodeScanResultJson[i].EmpId;
                        barcodeScanResult1.EmpNm = barcodeScanResultJson[i].EmpNm;
                        barcodeScanResult1.Shift = barcodeScanResultJson[i].Shift;
                        barcodeScanResult1.TimeBarcode = barcodeScanResultJson[i].TimeBarcode;

                        AllResults.Add(barcodeScanResult1);
                        
                    }
                    
                    for (int i = 0; i < barcodeScanResultJson.Count; i++)
                    {
                        BarcodeScanDetailResult barcodeScanResult2 = new BarcodeScanDetailResult();
                        barcodeScanResult2.BarcodeD = barcodeScanResultJson[i].Barcode;
                        
                        barcodeScanResult2.NumD = barcodeScanResultJson[i].Num;
                        barcodeScanResult2.EmpIdD = barcodeScanResultJson[i].EmpId;
                        barcodeScanResult2.EmpNmD = barcodeScanResultJson[i].EmpNm;
                        barcodeScanResult2.ShiftD = barcodeScanResultJson[i].Shift;
                        BarcodeScanResultDetailList.Add(barcodeScanResult2);


                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", response.StatusCode.ToString(), "OK");
                }
                

                    
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            LoadData();
        }

        [RelayCommand]
        void LoadData()
        {
            var listDateAll = AllResults.Select(x => x.TimeBarcode).Distinct().ToList();
            var listDate = BarcodeScanResultList.Select(x=> x.TimeBarcode).Distinct().ToList();

            for(int i = 0; i < listDateAll.Count; i++)
            {
                var listScanDate = AllResults.Where(x => x.TimeBarcode == listDateAll[i]).ToObservableCollection();
                foreach(var item in listScanDate)
                {
                    BarcodeScanResultList.Add(item);
                }
            }    
            
        }

        public sealed class CurrentDateValueProvider
        {
            public DateTime CurrentDateTime { get { return DateTime.Now; } }
        }

        public void TitleCollection()
        {
            TitleHis titleHis = new TitleHis();
            titleHis.ItemNo = "Mã sản phẩm";
            titleHis.Num = "Số lượng";
            titleHis.EmpNo = "Mã NV";
            titleHis.EmpNm = "Tên NV";
            titleHis.Shift = "Ca làm việc";
            ListviewBarcodeTitleHis.Add(titleHis);
        }

        [RelayCommand]
        public async Task QRCodeCheckResultDetails(DateTime DateScan)
        {
            DateId = DateScan;
            MaxId = AllResults.Distinct().Where(s =>s.TimeBarcode == DateId).Select(s => s.ResultID).First();
            await Shell.Current.GoToAsync($"HistoryScanDetailPage");
        }

    }
}
