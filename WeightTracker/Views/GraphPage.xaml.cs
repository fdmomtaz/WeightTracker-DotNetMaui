
using Microcharts;
using SkiaSharp;
using WeightTracker.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Alerts;

namespace WeightTracker.Views;

public partial class GraphPage : ContentPage
{
	public GraphPage()
	{
		InitializeComponent();

		BindingContext = new GraphModelView();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
				
		((GraphModelView)BindingContext).Populate();
	}

	private async void DataHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		try
		{
			Weight selectedWeight = (Weight) ((CollectionView)sender).SelectedItem;
			if (selectedWeight == null)
				return;

			var shouldRefresh = await this.ShowPopupAsync(new DetailedView(weight:selectedWeight));

			if(shouldRefresh != null && (bool)shouldRefresh) {		
				((GraphModelView)BindingContext).Populate();
			}

			DataHistory.SelectedItem = null;
		}
		catch(Exception)
		{
			var toast = Toast.Make("Well This is Embarrassing!! Unfortunately we ran into an issue showing the details of the record", ToastDuration.Short, 14);
			await toast.Show();
		}
	}
}