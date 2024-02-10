using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MauiApp1;

namespace WeightTracker.Views;

public partial class MainPage : ContentPage
{
	
	public MainPage()
    {
		InitializeComponent();

		BindingContext = new MainModelView();
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		
		((MainModelView)BindingContext).Populate();
	
		// sent to welcome page
		if (!Preferences.ContainsKey("is_app_init"))
			await Navigation.PushAsync(new WelcomePage());
	}

	async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs args)
	{
		if (((MainModelView)BindingContext).HasValue && ((MainModelView)BindingContext).LastRecord != null) {
			var shouldRefresh = await this.ShowPopupAsync(new DetailedView(((MainModelView)BindingContext).LastRecord));
		
			if (shouldRefresh != null && (bool)shouldRefresh) {
				((MainModelView)BindingContext).Populate();
			}
		}
	}

	private async void NewBtn_Clicked(object sender, EventArgs e)
	{
        var shouldRefresh = await this.ShowPopupAsync(new NewWeightView());

		if (shouldRefresh != null && (bool)shouldRefresh) {
			((MainModelView)BindingContext).Populate();
		}
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