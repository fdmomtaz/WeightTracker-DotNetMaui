using System.Resources;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

    private static string[] PickerTitles = {"", "Gender", "Unit of Measurement", "Height (CM)"};
    
    [ObservableProperty]
    private string description = "We just need a few small btis of information to get going :)";
    
    [ObservableProperty]
    private bool isTitle = true;

    [ObservableProperty]
    private bool isPicker = false;

    [ObservableProperty]
    private List<string> pickerItems = new List<string>();

    public List<string>[] Items = {
        new List<string>(),
        new List<string>() {"Male", "Female"}, 
        new List<string>() {"Imperial", "Metric"}, 
        new List<string>() 
    };

    [ObservableProperty]
    private string selectedItem = "";

    [ObservableProperty]
    private bool isMetricHeight = false;
    
    [ObservableProperty]
    private bool isImperialHeight = false;

    [ObservableProperty]
    private double height = 0;
    
    [ObservableProperty]
    private string heightFt = "";
    
    [ObservableProperty]
    private string heightIn = "";

    private int Index = 0;

    public WelcomeModelView() {
    }

    public bool Increment() {
        // process the current step
        if (Index == 1) {
            if (string.IsNullOrWhiteSpace(SelectedItem)) {
                var toast = Toast.Make("Please select your gender", ToastDuration.Short, 14);
                toast.Show();

                return false;
            }

            Enums.Gender gender = Enum.Parse<Enums.Gender>(SelectedItem);
			Preferences.Set("Gender", (int) gender);
        }
        else if(Index == 2) {
            if (string.IsNullOrWhiteSpace(SelectedItem)) {
                var toast = Toast.Make("Please select a unit system", ToastDuration.Short, 14);
                toast.Show();

                return false;
            }

            Enums.UnitSystem unit = Enum.Parse<Enums.UnitSystem>(SelectedItem);
			Preferences.Set("UnitSystem", (int) unit);
        }
        else if(Index == 3) {
            Enums.UnitSystem unitSystem = (Enums.UnitSystem) Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial);

            if (unitSystem == Enums.UnitSystem.Imperial) {
                if (string.IsNullOrWhiteSpace(HeightFt) || string.IsNullOrWhiteSpace(HeightIn)) {
                    var toast = Toast.Make("Please select your height", ToastDuration.Short, 14);
                    toast.Show();

                    return false;
                }
                
                Preferences.Set("HeightFtInfo", int.Parse(HeightFt));
                Preferences.Set("HeightInchInfo", int.Parse(HeightIn));
            }
            else {
                if (Height == 0) {
                    var toast = Toast.Make("Please enter your height", ToastDuration.Short, 14);
                    toast.Show();

                    return false;
                }

                Preferences.Set("HeightCmInfo", Height);
            }
        }

        // increment index
        Index++;
        if (Index == 4) {
            return Save();
        }

        // update the UI
        Title = Titles[Index];
        PickerTitle = PickerTitles[Index];
        Image = new FontImageSource() {Glyph= Images[Index], FontFamily = "FontAwesomeSolid", Size = 150, Color = Color.FromArgb(Colors[Index])};
        PickerItems = Items[Index];
        SelectedItem = "";

        IsTitle = Index == 0;
        IsPicker = Index == 1 || Index == 2;
        IsImperialHeight = Index == 3 && ((Enums.UnitSystem) Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial) == Enums.UnitSystem.Imperial);
        IsMetricHeight = Index == 3 && ((Enums.UnitSystem) Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial) == Enums.UnitSystem.Metric);

        return false;
    }

    public bool Save() {
        // mark app as init
        Preferences.Set("isAppInit", DateTime.Now);

        return true;
    }
}
