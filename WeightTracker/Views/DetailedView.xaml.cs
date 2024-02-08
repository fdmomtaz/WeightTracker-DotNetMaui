using CommunityToolkit.Maui.Views;
using WeightTracker.Models;

namespace WeightTracker.Views;

public partial class DetailedView : CommunityToolkit.Maui.Views.Popup
{
	public DetailedView(int? weightId = null, Weight? weight = null)
	{
		InitializeComponent();

		if (weightId.HasValue) {
//			Weight weight = await App.Database.GetWeight(weightId.Value);
			
			
		}
	}
	
	private async void BackBtn_Clicked(object sender, EventArgs e)
	{
		await CloseAsync();
	}
}