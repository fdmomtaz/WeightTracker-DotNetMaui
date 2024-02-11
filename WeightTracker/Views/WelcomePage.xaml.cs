using WeightTracker;

namespace MauiApp1;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();

		BindingContext = new WelcomeModelView();
	}

	private void NextBtn_Clicked(object sender, EventArgs e)
	{
		((WelcomeModelView)BindingContext).Increment();
	}
}