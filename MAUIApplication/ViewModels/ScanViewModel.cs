
using CommunityToolkit.Mvvm.ComponentModel;
using MAUIApplication.ViewModels;
using CommunityToolkit.Mvvm.Input;
using HandlebarsDotNet.Collections;
using MAUIApplication.Data;
using MAUIApplication.Models;
using MAUIApplication.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZXing.Net.Maui;
using ZXing.QrCode.Internal;
using DevExpress.Maui.Editors;
using MAUIApplication.Views;
using DevExpress.Maui.Core.Internal;
using System.Net.Http.Json;

namespace MAUIApplication.ViewModels
{
    [QueryProperty(nameof(QrCodeResult), "qrCodeResult")]
    public partial class ScanViewModel: BaseViewModel
    {
        public string BarcodeCheck = "";
        public string Check = "0";
        public int MaxId;
        public const string ViewName = "ScanPage";
        public ObservableList<UserInfo> UserInfo { get; } = new();
        public ObservableCollection<BarcodeScan> Listviewbarcode { get; } = new();
        public ObservableCollection<BarcodeScanResult> ListviewbarcodeResult { get; } = new();
        public ObservableCollection<TitleResult> ListviewBarcodeTitleResult { get; } = new();
        public ObservableCollection<TitleScan> ListviewBarcodeTitle { get; } = new();
        
        public ObservableCollection<Shift> shifts { get; } = new();
        //public ObservableCollection<LoginUser> LoginUserList { get; } = new();
        public ObservableList<LoginUser> LoginUserList { get; } = new();
        public AsyncRelayCommand AddCommand { get; }

        [ObservableProperty]
        string txtBarcode;
        [ObservableProperty]
        string txtBarcodePro;
        [ObservableProperty]
        string txtBarcodeStan;
        [ObservableProperty]
        string barcode;
        [ObservableProperty]
        string num;
        [ObservableProperty]
        public string empIdLogin;
        [ObservableProperty]
        public string empNmLogin;
        [ObservableProperty]
        string statusContent;
        [ObservableProperty]
        public Color statusColor;
        string proContent;
        public string ProContent
        {
            get => proContent;
            set => SetProperty(ref proContent, value);
        }

        List<BarcodeScan> BarcodeList = new List<BarcodeScan>();
        List<BarcodeScanResult> BarcodeResult = new List<BarcodeScanResult>();
        List<string> CbShiftList = new List<string>();
        public LoginUser LoginUser = new LoginUser();

        private readonly IAPIService _apiservice = new APIService();
        public ScanViewModel()
        {
            Title = ViewName;
            AddCommand = new AsyncRelayCommand(Add);
            TitleCollection();
            EmpIdLogin = LoginViewModel.EmpIdLogin;
            EmpNmLogin = LoginViewModel.EmpNmLogin;
            if (proContent == null)
            {
                proContent = "1";
            }
            
        }

        public void GetUserLogin()
        {
            try
            {
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task Add()
        {
            try
            {
                HttpClient client = new HttpClient();

                string uriGetmax = "ScanResults/GetMaxId";
                var response = await _apiservice.Connect("GET", uriGetmax);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    int barcodeScanResultJson = await response.Content.ReadFromJsonAsync<int>();
                    MaxId = barcodeScanResultJson;
                }
                
                    string uri = "ScanBarcodeList/Create";
                DateTime timeSave = DateTime.Now;
                
                foreach (var Listviewbarcode1 in Listviewbarcode)
                {
                    BarcodeScan barcodeScanAdd = new BarcodeScan();
                    barcodeScanAdd.BarcodeSS = Listviewbarcode1.BarcodeSS;
                    barcodeScanAdd.ResultID = MaxId;
                    barcodeScanAdd.Barcode = Listviewbarcode1.Barcode;
                    barcodeScanAdd.BarcodeSX = Listviewbarcode1.BarcodeSX;
                    barcodeScanAdd.TimeBarcode = timeSave;
                    barcodeScanAdd.Color = Listviewbarcode1.Color;
                    barcodeScanAdd.Status = Listviewbarcode1.Status;
                    await _apiservice.Connect("POST",uri, barcodeScanAdd);
                }
                string uriResult = "ScanResults/Create";
                foreach (var ListviewbarcodeResult1 in ListviewbarcodeResult)
                {
                    BarcodeScanResult barcodeResultAdd = new BarcodeScanResult();
                    barcodeResultAdd.Barcode = ListviewbarcodeResult1.Barcode;
                    barcodeResultAdd.ResultID = MaxId;
                    barcodeResultAdd.Num = ListviewbarcodeResult1.Num;
                    barcodeResultAdd.EmpId = EmpIdLogin;
                    barcodeResultAdd.EmpNm = EmpNmLogin;
                    barcodeResultAdd.Shift = ScanPage.SelectShift;
                    barcodeResultAdd.TimeBarcode = timeSave;
                    await _apiservice.Connect("POST",uriResult, barcodeResultAdd);
                }

                

                await Shell.Current.DisplayAlert("Thông báo", "Đã lưu dữ liệu thành công", "OK");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }

        

        [RelayCommand]
        async Task GotoScan()
        {
            if (ScanPage.CheckCb == "0" || ScanPage.CheckCb == null)
            {
                await Shell.Current.DisplayAlert("Thông báo","Bạn cần chọn ca làm việc trước","OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"ScanPagesScan");
            }    
            

              
            // await Navigation.NavigateToAsync<ScanScanViewModel>(true);
        }

        [RelayCommand]
        async  Task GotoScanPro()
        {
            if (ProContent == "1")
            {
                bool CheckOT = await Shell.Current.DisplayAlert("Thông báo", "Bạn muốn đổi sang quét 2 lần không?", "OK", "Cancel");
                if (CheckOT)
                {
                    ProContent = "2";
                    TxtBarcode = "";
                    TxtBarcodePro = "";
                    TxtBarcodeStan = "";
                }

                
            }
            else
            {
                bool CheckOT = await Shell.Current.DisplayAlert("Thông báo", "Bạn muốn đổi sang quét 1 lần không?", "OK", "Cancel");
                if (CheckOT)
                {
                    ProContent = "1";
                    TxtBarcode = "";
                    TxtBarcodePro = "";
                    TxtBarcodeStan = "";
                }
            }

        }
        [RelayCommand]
        async Task GotoScanStand()
        {
            bool CheckOT = await Shell.Current.DisplayAlert("Thông báo", "Mã barcode này không còn đúng nữa, bạn muốn thay đổi?", "OK", "Cancel");
            if (CheckOT)
            {
                TxtBarcodeStan = "";
                TxtBarcode = "";
                TxtBarcodePro = "";
            }    
            
        }





        public void TitleCollection()
        {
            TitleScan itemTitle = new TitleScan();
            TitleResult itemTitleResult = new TitleResult();
            LoginUser loginUser = new LoginUser();
            itemTitle.TitleBarcode = "QRCode";
            itemTitle.BarcodeStan = "Mã SP(SS)";
            itemTitle.BarcodePro = "Mã SP(SX)";
            ListviewBarcodeTitle.Add(itemTitle);
            itemTitleResult.TitleBarcode = "Mã SP";
            itemTitleResult.TitleNum = "Số lượng";
            ListviewBarcodeTitleResult.Add(itemTitleResult);
            loginUser.EmpId = LoginViewModel.EmpIdLogin;
            loginUser.EmpNm = LoginViewModel.EmpNmLogin;
            LoginUserList.Add(loginUser);

        }


        public string QrCodeResult
        {
            set
            {
                BarcodeScan item = new BarcodeScan();
                BarcodeScanResult itemResult = new BarcodeScanResult();
                var CheckTontai = 0;
                Check = "0";
                //ListviewbarcodeResult.Clear();
                string valueCode = value.Substring(0, 11);

                if (ProContent == "1")  //Xet trường hợp chọn quét 1 lần hay 2 lần
                {
                    if (TxtBarcodeStan == null || TxtBarcodeStan == "")
                    {
                        TxtBarcodeStan = valueCode;
                        BarcodeCheck = value;

                    }
                    else if (Listviewbarcode.Count == 0 && TxtBarcodeStan == valueCode)
                    {
                        item.Color = Color.FromArgb("#666666"); //FF3300
                        item.BarcodeSX = "";
                        item.Barcode = value;
                        item.BarcodeSS = valueCode;
                        item.TimeBarcode = DateTime.Now;
                        item.Status = "OK";
                        StatusContent = "OK";
                        StatusColor = Color.FromArgb("0dff00");
                        Listviewbarcode.Add(item);
                        itemResult.Barcode = valueCode;
                        itemResult.Num = 1;
                        BarcodeResult.Add(itemResult);
                        ListviewbarcodeResult.Clear();
                        foreach (var BarcodeResult1 in BarcodeResult)
                        {
                            
                            ListviewbarcodeResult.Add(BarcodeResult1);
                        }
                    }
                    else
                    {
                    
                        item.Barcode = value;
                        item.BarcodeSS = valueCode;
                        foreach (var itemlist in BarcodeList)
                        {
                            if (itemlist.Barcode == value)
                            {
                                Check = "1";
                            }
                        
                        }    
                        if (TxtBarcodeStan == valueCode && BarcodeCheck != value && Check == "0")
                        {
                            item.Color = Color.FromArgb("#666666"); //FF3300
                            item.Status = "OK";
                            //ColorStatus = Color.FromArgb("#00FF00");
                            StatusContent = "OK";
                            StatusColor = Color.FromArgb("0dff00");
                        }
                        else
                        {
                            item.Color = Color.FromArgb("FF3300"); //FF3300
                            if (TxtBarcodeStan == valueCode)
                            {
                                StatusColor = Color.FromArgb("#eaff00");
                                StatusContent = "DOUBLE";
                                item.Status = "DOUBLE";
                            }
                            else
                            {
                                StatusColor = Color.FromArgb("#FF0000");
                                StatusContent = "NG";
                                item.Status = "NG";
                            }
                        
                        }
                        item.BarcodeSX = "";
                        item.TimeBarcode = DateTime.Now;
                        TxtBarcode = valueCode;
                        BarcodeList.Add(item);

                        Listviewbarcode.Add(item);// = BarcodeList.ToList();

                    
                        if (BarcodeResult.Count == 0)
                        {
                            itemResult.Barcode = valueCode;
                            itemResult.Num = 1;
                        }
                        else
                        {
                            for (int i = 0; i < BarcodeResult.Count; i++)
                            {
                                if (BarcodeResult[i].Barcode == valueCode)
                                {
                                    CheckTontai = 1;
                                    itemResult.Num = BarcodeResult[i].Num + 1;
                                    BarcodeResult.Remove(BarcodeResult[i]);
                                    itemResult.Barcode = valueCode;
                                
                                    break;
                                }
                            }
                            if (CheckTontai == 0)
                            {
                                itemResult.Barcode = valueCode;
                                itemResult.Num = 1;
                            }
                        }
                        BarcodeResult.Add(itemResult);

                        if (ListviewbarcodeResult.Count != 0)
                            ListviewbarcodeResult.Clear();
                        foreach(var BarcodeResult1 in BarcodeResult)
                        {
                            ListviewbarcodeResult.Clear();
                            ListviewbarcodeResult.Add(BarcodeResult1);
                        }
                    }

                }
                else//Scan 2 lần
                {
                    if(TxtBarcodeStan == null || TxtBarcodeStan == "")
                    {
                        if(TxtBarcodePro == null || TxtBarcodePro =="")
                        {
                            TxtBarcodePro = valueCode;
                        }
                        else
                        {
                            if(TxtBarcodePro == valueCode)
                            {
                                TxtBarcodeStan = valueCode;
                                TxtBarcodePro = "";
                                TxtBarcode = "";
                            }
                            else
                            {
                                TxtBarcode = valueCode;
                                Shell.Current.DisplayAlert("Cảnh báo!", "Mã sản phẩm của phòng sản xuất và Samsung khác nhau", "Kiểm tra lại");
                                item.Barcode = value;
                                item.BarcodeSX = TxtBarcodePro;
                                item.BarcodeSS = TxtBarcodeStan;
                                item.Color = Color.FromArgb("FFFF00");
                                BarcodeResult.Add(itemResult);
                                foreach (var BarcodeResult1 in BarcodeResult)
                                {
                                    ListviewbarcodeResult.Clear();
                                    ListviewbarcodeResult.Add(BarcodeResult1);
                                }
                                TxtBarcode = "";
                                TxtBarcodePro = "";
                            }
                        }    
                    }
                    else
                    {
                        if(TxtBarcodePro == "")
                        {
                            TxtBarcodePro = valueCode;
                        }    
                        else
                        {
                            if(TxtBarcodePro != valueCode)
                            {
                                TxtBarcode = valueCode;
                                Shell.Current.DisplayAlert("Cảnh báo!", "Mã sản phẩm của phòng sản xuất và Samsung khác nhau", "Kiểm tra lại");
                                item.Barcode = value;
                                item.BarcodeSX = TxtBarcodePro;
                                item.BarcodeSS = TxtBarcodeStan;
                                item.Color = Color.FromArgb("FF0000");
                                Listviewbarcode.Add(item);

                                for (int i = 0; i < BarcodeResult.Count; i++)
                                {
                                    if (BarcodeResult[i].Barcode == valueCode)
                                    {
                                        CheckTontai = 1;
                                        itemResult.Num = BarcodeResult[i].Num + 1;
                                        BarcodeResult.Remove(BarcodeResult[i]);
                                        itemResult.Barcode = valueCode;

                                        break;
                                    }
                                }
                                if (CheckTontai == 0)
                                {
                                    itemResult.Barcode = valueCode;
                                    itemResult.Num = 1;
                                }

                                BarcodeResult.Add(itemResult);
                                ListviewbarcodeResult.Clear();
                                foreach (var BarcodeResult1 in BarcodeResult)
                                {
                                    
                                    ListviewbarcodeResult.Add(BarcodeResult1);
                                }
                                TxtBarcode = "";
                                TxtBarcodePro = "";
                            }
                            else if (TxtBarcodePro == valueCode && TxtBarcodePro != TxtBarcodeStan) 
                            {
                                TxtBarcode = valueCode;
                                Shell.Current.DisplayAlert("Cảnh báo!", "Mã sản phẩm "+ TxtBarcodePro + " bị lẫn trong pallet mã "+ TxtBarcodeStan + "", "Ok");
                                item.Barcode = value;
                                item.BarcodeSX = TxtBarcodePro;
                                item.BarcodeSS = TxtBarcodeStan;
                                item.Color = Color.FromArgb("FF0000");// màu đỏ
                                Listviewbarcode.Add(item);

                                for (int i = 0; i < BarcodeResult.Count; i++)
                                {
                                    if (BarcodeResult[i].Barcode == valueCode)
                                    {
                                        CheckTontai = 1;
                                        itemResult.Num = BarcodeResult[i].Num + 1;
                                        BarcodeResult.Remove(BarcodeResult[i]);
                                        itemResult.Barcode = valueCode;

                                        break;
                                    }
                                }
                                if (CheckTontai == 0)
                                {
                                    itemResult.Barcode = valueCode;
                                    itemResult.Num = 1;
                                }

                                BarcodeResult.Add(itemResult);
                                ListviewbarcodeResult.Clear();
                                foreach (var BarcodeResult1 in BarcodeResult)
                                {
                                    
                                    ListviewbarcodeResult.Add(BarcodeResult1);
                                }
                                TxtBarcode = "";
                                TxtBarcodePro = "";
                            }
                            else  //truyền giá trị vào list
                            {
                                if (Listviewbarcode.Count == 0)
                                {
                                    item.Color = Color.FromArgb("#666666"); // màu đen
                                    item.BarcodeSX = TxtBarcodePro;
                                    item.Barcode = value;
                                    item.BarcodeSS = TxtBarcodeStan;
                                    item.TimeBarcode = DateTime.Now;
                                    item.Status = "OK";
                                    StatusContent = "OK";
                                    StatusColor = Color.FromArgb("0dff00"); //màu xanh
                                    Listviewbarcode.Add(item);
                                    itemResult.Barcode = valueCode;
                                    itemResult.Num = 1;
                                    BarcodeResult.Add(itemResult);
                                    ListviewbarcodeResult.Clear();
                                    foreach (var BarcodeResult1 in BarcodeResult)
                                    {
                                        
                                        ListviewbarcodeResult.Add(BarcodeResult1);
                                    }
                                    TxtBarcodePro = "";
                                }
                                else
                                {

                                    item.Barcode = value;
                                    item.BarcodeSS = TxtBarcodeStan;
                                    foreach (var itemlist in Listviewbarcode)//kiểm tra xem có trùng barcode đã quét hay không
                                    {
                                        if (itemlist.Barcode == value)
                                        {
                                            Check = "1";
                                        }

                                    }
                                    if (TxtBarcodeStan == valueCode && BarcodeCheck != value && Check == "0")
                                    {
                                        item.Color = Color.FromArgb("#666666"); //FF3300
                                                                                //ColorStatus = Color.FromArgb("#00FF00");
                                        StatusContent = "OK";
                                        item.Status = "OK";
                                        StatusColor = Color.FromArgb("0dff00");
                                    }
                                    else
                                    {
                                        item.Color = Color.FromArgb("FF3300"); //FF3300
                                        if (TxtBarcodeStan == valueCode)
                                        {
                                            Shell.Current.DisplayAlert("Cảnh báo!", "Mã barcode đã được quét, vui lòng kiểm tra lại", "Kiểm tra lại");
                                            StatusColor = Color.FromArgb("#eaff00");
                                            item.Color = Color.FromArgb("#CCBF02"); //màu vàng
                                            StatusContent = "DOUBLE";
                                            item.Status = "DOUBLE";
                                        }
                                        else
                                        {
                                            StatusColor = Color.FromArgb("#FF0000");
                                            item.Color = Color.FromArgb("#FF0000");
                                            StatusContent = "NG";
                                            item.Status = "NG";
                                        }

                                    }
                                    item.BarcodeSX = TxtBarcodePro;
                                    item.TimeBarcode = DateTime.Now;
                                    TxtBarcode = valueCode;
                                    BarcodeList.Add(item);

                                    Listviewbarcode.Add(item);// = BarcodeList.ToList();


                                    if (BarcodeResult.Count == 0)
                                    {
                                        itemResult.Barcode = valueCode;
                                        itemResult.Num = 1;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < BarcodeResult.Count; i++)
                                        {
                                            if (BarcodeResult[i].Barcode == valueCode)
                                            {
                                                CheckTontai = 1;
                                                itemResult.Num = BarcodeResult[i].Num + 1;
                                                BarcodeResult.Remove(BarcodeResult[i]);
                                                itemResult.Barcode = valueCode;

                                                break;
                                            }
                                        }
                                        if (CheckTontai == 0)
                                        {
                                            itemResult.Barcode = valueCode;
                                            itemResult.Num = 1;
                                        }
                                    }
                                    BarcodeResult.Add(itemResult);

                                    if (ListviewbarcodeResult.Count != 0)
                                        ListviewbarcodeResult.Clear();
                                    foreach (var BarcodeResult1 in BarcodeResult)
                                    {
                                       
                                        ListviewbarcodeResult.Add(BarcodeResult1);
                                    }
                                    TxtBarcodePro = "";
                                    TxtBarcode = "";
                                }
                            }    
                        }    
                    }    
                }    
            }

        }
    }
}
