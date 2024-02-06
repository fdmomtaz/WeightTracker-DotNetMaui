using WeightTracker.Models;

namespace WeightTracker.Views;

public partial class NewWeightView :  CommunityToolkit.Maui.Views.Popup
{
	public NewWeightView()
	{
		InitializeComponent();

		BindingContext = new WeightModelView();
	}

	private async void BackBtn_Clicked(object sender, EventArgs e)
	{
		await CloseAsync();
	}
	
	private async void NewDataBtn_Clicked(object sender, EventArgs e)
	{
		try
		{
			/*
			if (string.IsNullOrWhiteSpace(Weight.Text))
			{
				//await this.App.DisplayAlert("Can't Do That!", "The weight can't be empty", "OK");
				return;
			}

			if (RecordDate.Date == DateTime.MaxValue || RecordDate.Date == DateTime.MinValue)
			{
				//await this.App.DisplayAlert("Can't Do That!", "The date is required", "OK");
				return;
			}

			if (RecordDate.Date > DateTime.Now)
			{
				// await this.App.DisplayAlert("Can't Do That!", "Can't add a weight for a future date", "OK");
				return;
			}

			List<Weight> weights = await App.Database.GetWeights();
			if (weights.Any(x => x.Record.Date == RecordDate.Date))
			{
				// await  this.App.DisplayAlert("Can't Do That!", "Can't add two weights for a date", "OK");
				return;
			}
*/

			// create new object
			WeightModelView weight = (WeightModelView) this.BindingContext;
			Weight newWeight = new Weight
			{
				Value = weight.Weight,
				Record = weight.WeightDate,
				BodyFat = weight.BodyFat,
				BoneMass = weight.BoneMass,
				MuscleMass = weight.MuscleMass,
				WaterWeight = weight.WaterWeight
			};

			// add new data to the database
			int isSuccess = await App.Database.SaveWeightAsync(newWeight);
			if (isSuccess == 1)
			{
				await CloseAsync();
			}
			else
			{
				//await this.App.DisplayAlert("Well This is Embarrassing!!", "Unfortunately we ran into an issues while saving the data", "OK");
			}
		}
		catch(Exception Ex)
		{
			//await this.App.DisplayAlert("Well This is Embarrassing!!", "Unfortunately we ran into an issues while saving the data", "OK");
		}
	}
}