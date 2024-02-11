using WeightTracker;

namespace MauiApp1;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();

		BindingContext = new WelcomeModelView();
	}

	private async void NextBtn_Clicked(object sender, EventArgs e)
	{
		if (((WelcomeModelView)BindingContext).Increment())
			await Navigation.PopAsync();
	}
}