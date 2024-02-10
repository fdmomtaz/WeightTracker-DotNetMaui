using CommunityToolkit.Mvvm.ComponentModel;
using WeightTracker.Models;

namespace WeightTracker;

public partial class WeightModelView : ObservableObject
{
    [ObservableProperty]
    private DateTime weightDate = DateTime.Today;

    [ObservableProperty]
    private double weight;

    public double? BMI {
        get {
            Enums.UnitSystem unitSystem = (Enums.UnitSystem) Preferences.Get("UnitSystem", (int)Enums.UnitSystem.Imperial);

            if (unitSystem == Enums.UnitSystem.Imperial && Preferences.ContainsKey("HeightFtInfo") && Preferences.ContainsKey("HeightInchInfo"))
            {
                double heightInch = Preferences.Get("HeightFtInfo", 0) * 12 + Preferences.Get("HeightInchInfo", 0) - 1;
                return Math.Round(((Weight / (heightInch * heightInch)) * 703), 2);
            }

            if (unitSystem == Enums.UnitSystem.Metric && Preferences.ContainsKey("HeightCmInfo") && Preferences.Get("HeightCmInfo", string.Empty) != string.Empty)
            {
                double heightMetric;
                double.TryParse(Preferences.Get("HeightCmInfo", string.Empty), out heightMetric);
                heightMetric = heightMetric / 100;

                return Math.Round(Weight / (heightMetric * heightMetric), 2);
            }

            return null;
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BodyFat))]
    private double? bodyFatPercent;

    public double? BodyFat {get {return (!BodyFatPercent.HasValue) ? null : Math.Round((BodyFatPercent.Value * this.Weight) / 100, 2) ;}}

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MuscleMass))]
    private double? muscleMassPercent;
    
    public double? MuscleMass {get {return (!MuscleMassPercent.HasValue) ? null : Math.Round((MuscleMassPercent.Value * this.Weight) / 100, 2) ;}}

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WaterWeight))]
    private double? waterWeightPercent;
    
    public double? WaterWeight {get {return (!WaterWeightPercent.HasValue) ? null : Math.Round((WaterWeightPercent.Value * this.Weight) / 100, 2) ;}}

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BoneMass))]
    private double? boneMassPercent;
    
    public double? BoneMass {get {return (!BoneMassPercent.HasValue) ? null : Math.Round((BoneMassPercent.Value * this.Weight) / 100, 2) ;}}

    [ObservableProperty]
    private string? formError;
}
