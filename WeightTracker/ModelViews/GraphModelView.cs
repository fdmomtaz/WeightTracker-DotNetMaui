using CommunityToolkit.Mvvm.ComponentModel;
using WeightTracker.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace WeightTracker;

public partial class GraphModelView : ObservableObject
{

    [ObservableProperty]
    private List<Weight> weights;

    [ObservableProperty]
    private string selectedInterval = "Month";

    
}
