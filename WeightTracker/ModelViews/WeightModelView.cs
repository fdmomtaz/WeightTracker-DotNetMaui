using CommunityToolkit.Mvvm.ComponentModel;

namespace WeightTracker;

public partial class WeightModelView : ObservableObject
{
    [ObservableProperty]
    private DateTime weightDate = DateTime.Today;

    [ObservableProperty]
    private double weight;

    [ObservableProperty]
    private double? bodyFat;

    [ObservableProperty]
    private double? muscleMass;

    [ObservableProperty]
    private double? waterWeight;

    [ObservableProperty]
    private double? boneMass;

    [ObservableProperty]
    private string? formError;
}
