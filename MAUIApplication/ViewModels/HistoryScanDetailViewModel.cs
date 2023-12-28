using CommunityToolkit.Mvvm.ComponentModel;
using MAUIApplication.Data;
using MAUIApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MAUIApplication.ViewModels
{
    public partial class HistoryScanDetailViewModel:BaseViewModel
    {
        ObservableCollection<BarcodeScan> barcodeScanListDetail;
        
        private readonly IAPIService _apiservice = new APIService();
        public ObservableCollection<BarcodeScan> BarcodeScanListDetail
        {
            get => barcodeScanListDetail;
            private set
            {
                {
                    barcodeScanListDetail = value;
                    OnPropertyChanged(nameof(BarcodeScanListDetail));
                }
            }
        }
        [ObservableProperty]
        public DateTime datetime1;
        public int MaxID;
        [ObservableProperty]
        public string dateT;
        public HistoryScanDetailViewModel() 
        {
            BarcodeScanListDetail = new();
            GetData();
        }

        public async Task GetData()
        {
            try
            {
                string uri = "ScanBarcodeList/GetId";
                //datetime1 = HistoryScanViewModel.DateId;
                MaxID = HistoryScanViewModel.MaxId;

                var response = await _apiservice.Connect("GET", $"{uri}?id={MaxID}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<BarcodeScanListJson> barcodeScanResultJson = await response.Content.ReadFromJsonAsync<List<BarcodeScanListJson>>();
                    //if (BarcodeScanList.Count > 0)
                    //    BarcodeScanList.Clear();
                    for (int i = 0; i < barcodeScanResultJson.Count; i++)
                    {
                        BarcodeScan barcodeScanResult1 = new BarcodeScan();
                        barcodeScanResult1.BarcodeID = barcodeScanResultJson[i].BarcodeID;
                        barcodeScanResult1.BarcodeSS = barcodeScanResultJson[i].BarcodeSS;
                        barcodeScanResult1.Barcode = barcodeScanResultJson[i].Barcode;
                        barcodeScanResult1.BarcodeSX = barcodeScanResultJson[i].BarcodeSX;
                        barcodeScanResult1.TimeBarcode = barcodeScanResultJson[i].TimeBarcode;
                        barcodeScanResult1.Status = barcodeScanResultJson[i].Status;
                        Datetime1 = barcodeScanResultJson[i].TimeBarcode;
                        if (barcodeScanResultJson[i].Status == "OK")
                        {
                            barcodeScanResult1.Color = Color.FromArgb("#0dff00");//xanh
                        } 
                        else if (barcodeScanResultJson[i].Status == "DOUBLE")
                        {
                            barcodeScanResult1.Color = Color.FromArgb("FF3300");//vang
                        }    
                        else
                        {
                            barcodeScanResult1.Color = Color.FromArgb("#FF3300");//do
                        }    
                        BarcodeScanListDetail.Add(barcodeScanResult1);

                    }
                    DateT = Datetime1.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
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
        }

        //async Task QRCodeCheckResultDetails()
        //{
        //    await Shell.Current.GoToAsync($"HistoryScanDetailPage");
        //}
        
    }
}
