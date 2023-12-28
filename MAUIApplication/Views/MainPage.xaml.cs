using DevExpress.Maui.CollectionView;
using MAUIApplication.ViewModels;

namespace MAUIApplication.Views
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
            Routing.RegisterRoute(nameof(ScanPagesScan), typeof(ScanPagesScan));
            Routing.RegisterRoute(nameof(HistoryScanPage), typeof(HistoryScanPage));
            Routing.RegisterRoute(nameof(HistoryScanDetailPage), typeof(HistoryScanDetailPage));
            Routing.RegisterRoute(nameof(ExportPage), typeof(ExportPage));
            Routing.RegisterRoute(nameof(ImportPage), typeof(ImportPage));
        }

        async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }

        void OnCloseClicked(object sender, EventArgs e)
        {
            Current.FlyoutIsPresented = false;
        }
    }
}