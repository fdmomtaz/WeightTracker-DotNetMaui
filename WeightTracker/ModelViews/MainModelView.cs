using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using WeightTracker.Models;

namespace WeightTracker;

public partial class MainModelView : ObservableObject
{
    [ObservableProperty]
    private bool hasValue = false;
    
    [ObservableProperty]
    private bool hasHistory = false;
    
    [ObservableProperty]
    private Color topBackgroundColor = Color.FromArgb("9E3A6F");
    
    [ObservableProperty]
    private Color bottomBackgroundColor = Color.FromArgb("3B1159");

    [ObservableProperty]
    private string weightUnit = "lbs";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentWeightText))]
    private double currentWeight = 0;

    public string CurrentWeightText {get { return (HasValue) ? CurrentWeight.ToString() : "-"; } }
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentDateText))]
    private DateTime currentDate = DateTime.MinValue;
    
    public string CurrentDateText {get { return (HasValue) ? CurrentDate.ToString("MMMM dd, yyyy") : "-"; } }
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WeekProgressIcon))]
    private double weekProgress = 0;

    public FontImageSource WeekProgressIcon {
        get {
            FontImageSource icon = new FontImageSource() {Glyph= "\uf068", FontFamily = "FontAwesomeSolid", Size = 20, Color = Color.FromArgb("000000")};

            if (HasHistory)
                icon.Glyph = (WeekProgress > 0) ? "\uf0d8" : "\uf0d7";
            
            return icon;
        }
    }
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MonthProgressIcon))]
    private double monthProgress = 0;

    public FontImageSource MonthProgressIcon {
        get {
            FontImageSource icon = new FontImageSource() {Glyph= "\uf068", FontFamily = "FontAwesomeSolid", Size = 20, Color = Color.FromArgb("000000")};

            if (HasHistory)
                icon.Glyph = (MonthProgress > 0) ? "\uf0d8" : "\uf0d7";
            
            return icon;
        }
    }
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(YearProgressIcon))]
    private double yearProgress = 0;

    public FontImageSource YearProgressIcon {
        get {
            FontImageSource icon = new FontImageSource() {Glyph= "\uf068", FontFamily = "FontAwesomeSolid", Size = 20, Color = Color.FromArgb("000000")};

            if (HasHistory)
                icon.Glyph = (YearProgress > 0) ? "\uf0d8" : "\uf0d7";
            
            return icon;
        }
    }

    public MainModelView() 
    {
        Populate();
    }

    public async void Populate()
    {
        // populate page
        if (Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial) == (int) Enums.UnitSystem.Imperial)
            WeightUnit = "lbs";
        else
            WeightUnit = "KG";

        List<Weight> weights = await App.Database.GetWeights();
        if (weights.Count == 0)
        {
            HasValue = false;
            HasHistory = false;
            
            TopBackgroundColor = Color.FromArgb("9E3A6F");
            BottomBackgroundColor = Color.FromArgb("3B1159");

            return;
        }

        // get all the recorded weights
        HasValue = true;
        weights = weights.OrderByDescending(x => x.Record).ToList();
        Weight LastRecord = weights[0];

        // set the current date
        CurrentWeight = LastRecord.Value;
        CurrentDate = LastRecord.Record;

        // set the variable background
        if (weights.Count > 1 && Preferences.Get("DynamicBackground", true))
        {
            if (weights[0].Value > weights[1].Value) {
                TopBackgroundColor = Color.FromArgb("E50000");
                BottomBackgroundColor = Color.FromArgb("FFA500");
            }
            else {
                TopBackgroundColor = Color.FromArgb("9ACD32");
                BottomBackgroundColor = Color.FromArgb("006400");
            }
        }
        else {
            TopBackgroundColor = Color.FromArgb("9E3A6F");
            BottomBackgroundColor = Color.FromArgb("3B1159");
        }

        // check if there is weight history
        if (weights.Count == 1) {
            HasHistory = false;
            return;
        }

        HasHistory = true;

        // get the progress numbers
        DateTime lastRecord = weights[0].Record;
        DateTime weekDate = lastRecord.AddDays(-7);
        DateTime monthDate = lastRecord.AddMonths(-1);
        DateTime yearDate = lastRecord.AddYears(-1);

        List<Weight> yearWeights = weights.Where(x => x.Record >= yearDate).OrderByDescending(x => x.Record).ToList();
        List<Weight> monthWeights = weights.Where(x => x.Record >= monthDate).OrderByDescending(x => x.Record).ToList();
        List<Weight> weekWeights = weights.Where(x => x.Record >= weekDate).OrderByDescending(x => x.Record).ToList();

        WeekProgress = weekWeights[0].Value - weekWeights[weekWeights.Count - 1].Value;
        MonthProgress = monthWeights[0].Value - monthWeights[monthWeights.Count - 1].Value;
        YearProgress = yearWeights[0].Value - yearWeights[yearWeights.Count - 1].Value;
    }
}
