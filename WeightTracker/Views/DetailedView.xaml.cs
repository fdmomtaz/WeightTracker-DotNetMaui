using CommunityToolkit.Maui.Views;
using WeightTracker.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;


namespace WeightTracker.Views;

public partial class DetailedView : CommunityToolkit.Maui.Views.Popup
{
	Weight weight;

	public DetailedView(Weight weight)
	{
		InitializeComponent();

		this.weight = weight;
		this.BindingContext = new WeightModelView() {
			WeightDate = weight.Record,
			Weight = weight.Value,
			BodyFatPercent = weight.BodyFatPercent,
			MuscleMassPercent = weight.MuscleMassPercent,
			WaterWeightPercent = weight.WaterWeightPercent,
			BoneMassPercent = weight.BoneMassPercent
		};
	}
	
	private async void BackBtn_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(false);
	}
	
	private async void DeleteWeightBtn_Clicked(object sender, EventArgs e)
	{
		try 
		{
			await App.Database.DeleteWeightAsync(weight);

			await CloseAsync(true);
		}
		catch(Exception) 
		{
			var toast = Toast.Make("Well This is Embarrassing!! Unfortunately we ran into an issues while deleting the weight recordings", ToastDuration.Short, 14);
			await toast.Show();
		}
	}
}