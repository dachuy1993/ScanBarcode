using MAUIApplication.ViewModels;
using MAUIApplication.Models;
using System.Globalization;
using ZXing.Net.Maui;
using DevExpress.Maui.Editors;
using Microsoft.Maui.Controls;
using MAUIApplication.Data;

namespace MAUIApplication.Views;

//[XamlCompilation(XamlCompilationOptions.Compile)]
//[QueryProperty(nameof(QrCodeResult), "qrCodeResult")]
public partial class ScanPage : ContentPage
{
    public static string SelectShift;
    public static string CheckCb;
    public ScanPage()
	{
		InitializeComponent();
		this.BindingContext = new ScanViewModel();


        List<string> options = new List<string> { "Ca A", "Ca B" };
        CbShift.ItemsSource = options;
        //lbEmpId.BindingContext = new ScanViewModel();
        //lbEmpNm.BindingContext = new ScanViewModel();
        //lbEmpId.Text = ScanViewModel.EmpIdLogin;
        //lbEmpNm.Text = ScanViewModel.EmpNmLogin;

    }

    private void CbShift_SelectionChanged(object sender, EventArgs e)
    {
        if (CbShift.SelectedItem.ToString() == null)
        {
            return;
        }
        SelectShift = CbShift.SelectedItem.ToString();
        
        if (SelectShift == null)
        {
            CheckCb = "0";
        }
        else
        {
            CheckCb = "1";
        }
    }
}