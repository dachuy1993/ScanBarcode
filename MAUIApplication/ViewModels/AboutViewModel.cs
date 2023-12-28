using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MAUIApplication.Services;
using MAUIApplication.Views;
using System.Windows.Input;

namespace MAUIApplication.ViewModels
{
    public partial class AboutViewModel : BaseViewModel
    {
        public const string ViewName = "AboutPage";
        public AboutViewModel()
        {
            Title = "Quét mã QR";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.devexpress.com/maui/"));
        }




        public ICommand OpenWebCommand { get; }

        [RelayCommand]

        async Task GotoScanPage()
        {
            
            //await Navigation.NavigateToAsync<ScanViewModel>(true);

            await Shell.Current.GoToAsync($"ScanPage");
        }

        [RelayCommand]
        async Task GotoHistoryScanPage()
        {
            await Shell.Current.GoToAsync($"HistoryScanPage");
        }

        [RelayCommand]
        async Task GotoExportPage()
        {
            await Shell.Current.GoToAsync($"ExportPage");
        }
        [RelayCommand]
        async Task GotoImportPage()
        {
            await Shell.Current.GoToAsync($"ImportPage");
        }
    }
}