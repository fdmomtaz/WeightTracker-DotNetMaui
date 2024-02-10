
using Microcharts;
using SkiaSharp;
using WeightTracker.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

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
		Weight selectedWeight = (Weight) ((CollectionView)sender).SelectedItem;
		if (selectedWeight == null)
			return;

		var shouldRefresh = await this.ShowPopupAsync(new DetailedView(weight:selectedWeight));

		if(shouldRefresh != null && (bool)shouldRefresh) {		
			((GraphModelView)BindingContext).Populate();
		}

		DataHistory.SelectedItem = null;
	}
}