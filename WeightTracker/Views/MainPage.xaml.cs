using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace WeightTracker.Views;

public partial class MainPage : ContentPage
{
	
	public MainPage()
    {
		InitializeComponent();

		BindingContext = new MainModelView();
	}

	async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs args)
	{
		if (((MainModelView)BindingContext).HasValue)
	        await this.ShowPopupAsync(new DetailedView());
	}

	private async void NewBtn_Clicked(object sender, EventArgs e)
	{
        await this.ShowPopupAsync(new NewWeightView());
	}

	private async void GraphBtn_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new GraphPage());
	}

	private async void SettingsBtn_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new SettingsPage());
	}
}