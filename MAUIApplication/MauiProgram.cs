using DevExpress.Maui;
using MAUIApplication.ViewModels;
using MAUIApplication.Views;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using ZXing.Net.Maui.Controls;

namespace MAUIApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseDevExpress(useLocalization: true)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans_Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                    fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("univia-pro-regular.ttf", "Univia-Pro");
                    fonts.AddFont("univia-pro-medium.ttf", "Univia-Pro Medium");
                })
                .ConfigureEffects((effects) =>
                {
                    effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
                });
            DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();
            DevExpress.Maui.DataGrid.Initializer.Init();
            DevExpress.Maui.Scheduler.Initializer.Init();

            builder.Services.AddTransient<ScanViewModel>();
            builder.Services.AddTransient<ScanPage>();

            builder.Services.AddTransient<ScanScanViewModel>();
            builder.Services.AddTransient<ScanPagesScan>();

            builder.Services.AddTransient<HistoryScanViewModel>();
            builder.Services.AddTransient<HistoryScanPage>();

            builder.Services.AddTransient<HistoryScanDetailViewModel>();
            builder.Services.AddTransient<HistoryScanDetailPage>();


            builder.Services.AddTransient<ExportViewModel>();
            builder.Services.AddTransient<ExportPage>();

            builder.Services.AddTransient<ImportViewModel>();
            builder.Services.AddTransient<ImportPage>();

            return builder.Build();
        }
    }
}