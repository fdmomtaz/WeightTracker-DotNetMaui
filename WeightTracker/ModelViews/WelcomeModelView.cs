using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using WeightTracker.Models;

namespace WeightTracker;

public partial class WelcomeModelView : ObservableObject
{    
    private static string[] Colors = {"ee6002", "6200EE", "75e900", "FFDE03"};

    [ObservableProperty]
    private string title = Titles[0];

    private static string[] Titles = {"Hey There!", "Chose Your Gender", "Choose a Unit", "What's Your Height"};

    [ObservableProperty]
    private FontImageSource image = new FontImageSource() {Glyph= Images[0], FontFamily = "FontAwesomeSolid", Size = 150, Color = Color.FromArgb(Colors[0])};
    
    private static string[] Images = {"\uf0ae", "\ue548", "\uf545", "\uf5ae"};

    [ObservableProperty]
    private string pickerTitle = PickerTitles[0];

    private static string[] PickerTitles = {"", "Gender", "Unit of Measurement", "Height"};
    
    [ObservableProperty]
    private string description = "We just need a few small btis of information to get going :)";
    
    [ObservableProperty]
    private bool isPicker = false;

    [ObservableProperty]
    private List<string> pickerItems = new List<string>();

    [ObservableProperty]
    private string selectedItem = "";

    [ObservableProperty]
    private bool isInput = false;

    [ObservableProperty]
    private double height = 0;

    private int Index = 0;

    public WelcomeModelView() {
    }

    public void Increment() {
        Index++;

        if (Index == 4) {
            Save();
            return;
        }

        Title = Titles[Index];
        PickerTitle = PickerTitles[Index];
        Image = new FontImageSource() {Glyph= Images[Index], FontFamily = "FontAwesomeSolid", Size = 150, Color = Color.FromArgb(Colors[Index])};

        IsPicker = Index == 1 || Index == 2;
        IsInput = Index == 3;
    }

    public void Save() {
        // mark app as init
        Preferences.Set("is_app_init", DateTime.Now);
    }
}
