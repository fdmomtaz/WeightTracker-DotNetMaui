using CommunityToolkit.Maui.Views;

namespace WeightTracker.Views;

public partial class DetailedView : CommunityToolkit.Maui.Views.Popup
{
	public DetailedView()
	{
		InitializeComponent();
	}
	
	private async void BackBtn_Clicked(object sender, EventArgs e)
	{
		await CloseAsync();
	}
}