
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

	private async void DataHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Weight selectedWeight = DataHistory.SelectedItem as Weight;
		if (selectedWeight == null)
			return;

		await this.ShowPopupAsync(new DetailedView(weight:selectedWeight));

		DataHistory.SelectedItem = null;
	}
}