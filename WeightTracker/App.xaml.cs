using WeightTracker.Services;

namespace WeightTracker;

public partial class App : Application
{
	static Database? database;

	public static Database Database
	{
		get
		{
			// create new database instance
			if (database == null)
			{
				database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "weights.db3"));
			}

			return database;
		}
	}

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
