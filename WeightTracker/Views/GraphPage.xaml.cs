namespace WeightTracker.Views;

public partial class GraphPage : ContentPage
{
	public GraphPage()
	{
		InitializeComponent();

		BindingContext = new GraphModelView();
	}
}