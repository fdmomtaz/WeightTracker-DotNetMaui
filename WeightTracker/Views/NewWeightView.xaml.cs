using WeightTracker.Models;

namespace WeightTracker.Views;

public partial class NewWeightView :  CommunityToolkit.Maui.Views.Popup
{
	public NewWeightView()
	{
		InitializeComponent();

		BindingContext = new WeightModelView() ;
	}

	private async void BackBtn_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(false);
	}
	
	private async void NewDataBtn_Clicked(object sender, EventArgs e)
	{
		WeightModelView? weight = null;

		try
		{
			weight = (WeightModelView) this.BindingContext;

			if (weight.Weight <= 0)
			{
				weight.FormError = "The weight should be a positive non-zero number";
				return;
			}

			if (weight.WeightDate == DateTime.MaxValue || weight.WeightDate == DateTime.MinValue)
			{
				weight.FormError =  "The date is required";
				return;
			}

			if (weight.WeightDate > DateTime.Now)
			{
				weight.FormError =  "Can't add a weight for a future date";
				return;
			}

			List<Weight> weights = await App.Database.GetWeights();
			if (weights.Any(x => x.Record.Date == weight.WeightDate.Date))
			{
				weight.FormError =  "Can't add two weights for a date";
				return;
			}

			// create new object
			Weight newWeight = new Weight
			{
				Value = weight.Weight,
				Record = weight.WeightDate,
				BodyFatPercent = weight.BodyFatPercent,
				BoneMassPercent = weight.BoneMassPercent,
				MuscleMassPercent = weight.MuscleMassPercent,
				WaterWeightPercent = weight.WaterWeightPercent
			};

			// add new data to the database
			int isSuccess = await App.Database.SaveWeightAsync(newWeight);
			if (isSuccess == 1)
			{
				await CloseAsync(true);
			}
			else
			{
				weight.FormError =  "Well This is Embarrassing!! Unfortunately we ran into an issues while saving the data";
			}
		}
		catch(Exception)
		{
			if (weight != null)
				weight.FormError = "Well This is Embarrassing!! Unfortunately we ran into an issues while saving the data";

		}
	}
}