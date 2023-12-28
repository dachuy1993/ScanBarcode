using MAUIApplication.ViewModels;

namespace MAUIApplication.Views;

public partial class HistoryScanDetailPage : ContentPage
{
	public HistoryScanDetailPage()
	{
		InitializeComponent();
		BindingContext = new HistoryScanDetailViewModel();
	}
}