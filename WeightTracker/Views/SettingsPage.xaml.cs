using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using WeightTracker.Models;

namespace WeightTracker.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
	
	protected override void OnAppearing()
	{
		base.OnAppearing();
		
		// populate UI here instead of MVVM
        Enums.UnitSystem unitSystem = (Enums.UnitSystem) Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial);
		UnitText.ValueText = unitSystem.ToString();

		if (unitSystem == Enums.UnitSystem.Imperial) {
			HeightText.ValueText = string.Format("{0}' {1}\"", Preferences.Get("HeightFtInfo", 0), Preferences.Get("HeightInchInfo", 0));
		}
		else {
			HeightText.ValueText = string.Format("{0} CM", Preferences.Get("HeightCmInfo", string.Empty));
		}
		
        Enums.Gender gender = (Enums.Gender) Preferences.Get("Gender", (int)Enums.Gender.UnSelected);
		GenderText.ValueText = gender.ToString();
	}

	private async void DeleteBtn_Clicked(object sender, EventArgs e)
	{
		try 
		{
			// delete all the data
			await App.Database.DeleteAllWeightAsync();

			// delete all saved data
			Preferences.Remove("isAppInit");
			Preferences.Remove("UnitSystem");
			Preferences.Remove("HeightFtInfo");
			Preferences.Remove("HeightInchInfo");
			Preferences.Remove("HeightCmInfo");
			Preferences.Remove("Gender");

			var toast = Toast.Make("Your Data was successfully deleted", ToastDuration.Short, 14);
			await toast.Show();
		}
		catch(Exception) 
		{
			var toast = Toast.Make("Well This is Embarrassing!! Unfortunately we ran into an issues while deleting the weight recordings", ToastDuration.Short, 14);
			await toast.Show();
		}
	}

	private async void ExportBtn_Clicked(object sender, EventArgs e)
	{
		try
		{
			// get data
			List<Weight> weights = await App.Database.GetWeights();
			weights = weights.OrderByDescending(x => x.Record).ToList();

			// create csv file
			string csvFile = Path.Combine(FileSystem.CacheDirectory, "history.csv");
			using (StreamWriter file = new StreamWriter(csvFile))
			{
				file.WriteLine(string.Format("Date, Weight, Body Fat, Muscle Mass, Water Weight, Bone Mass"));

				foreach (Weight weight in weights)
				{
					file.WriteLine(string.Format("{0}, {1}lbs, {2}, {3}, {4}, {5}", weight.Record.ToString("yyyy-MM-dd"), weight.Value, weight.BodyFatPercent, weight.MuscleMassPercent, weight.WaterWeightPercent, weight.BoneMassPercent));
				}
			}

			// share the file
			await Share.RequestAsync(new ShareFileRequest
			{
				Title = "Weight Records Spreadsheet",
				File = new ShareFile(csvFile)
			});
		}
		catch (Exception)
		{
			var toast = Toast.Make("Well This is Embarrassing!! Unfortunately we ran into an issues while exporting data recordings", ToastDuration.Short, 14);
			await toast.Show();
		}
	}
}