using DevExpress.Maui.Editors;
using MAUIApplication.ViewModels;

namespace MAUIApplication.Views;

public partial class HistoryScanPage : ContentPage
{
    
    public HistoryScanPage()
	{
        InitializeComponent();
        BindingContext = new HistoryScanViewModel();
    }

}