#if ANDROID
using Android.Content.PM;
#endif
using MAUIApplication.ViewModels;
namespace MAUIApplication.Views;

public partial class ImportPage : ContentPage
{
	public ImportPage()
	{
		InitializeComponent();
		BindingContext = new ImportViewModel();
        

    }

    // Khóa xoay ngang màn hình với Page Import
    protected override void OnAppearing()
    {
#if ANDROID
        ActivityStateManager.Default.GetCurrentActivity().RequestedOrientation = ScreenOrientation.SensorLandscape;
        base.OnAppearing();
#endif
    }

    // Khôi phục chế độ xoay màn hình theo cài đặt của người dùng khi thoát Page Import
    protected override void OnDisappearing()
    {
#if ANDROID
        ActivityStateManager.Default.GetCurrentActivity().RequestedOrientation = ScreenOrientation.User;
        base.OnDisappearing();
#endif
    }
}