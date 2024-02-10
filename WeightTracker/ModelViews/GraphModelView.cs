using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using WeightTracker.Models;
using Microcharts;
using SkiaSharp;

namespace WeightTracker;

public partial class GraphModelView : ObservableObject
{

    [ObservableProperty]
    private List<Weight> weightsHistory = new List<Weight>();

    [ObservableProperty]
    private string selectedInterval = "Week";

    [ObservableProperty]
    LineChart chartData = new LineChart { Entries = new List<ChartEntry>() {new ChartEntry(0)
            {
                Label = "-",
                ValueLabel = "0",
                Color = SKColor.Parse("#000000")
            }} };

    
    public GraphModelView() {
        Populate();
    }

    partial void OnSelectedIntervalChanged(string value) {
        Populate();
    }

    public async void Populate() {

        int intervalDays = -7;
        if (SelectedInterval.Equals("Month"))
            intervalDays = -30;
        else if (SelectedInterval.Equals("Year"))
            intervalDays = -365;

        // filter the data
        DateTime RecordLimit = DateTime.Today.AddDays(intervalDays);

        // get data
        WeightsHistory = (await App.Database.GetWeights())
                                .Where(x => x.Record >= RecordLimit)
                                .OrderByDescending(x => x.Record)
                                .ToList();

        // set the default values
        if (WeightsHistory.Count == 0) {
            WeightsHistory = new List<Weight>();

            ChartData = new LineChart { 
                Entries = new List<ChartEntry>() {
                    new ChartEntry(0) {
                        Label = "-",
                        ValueLabel = "0",
                        Color = SKColor.Parse("#000000")
                    }
                } 
            };

            return;
        }

        // get min weight
        double ScaleValue = WeightsHistory.OrderBy(x => x.Value).First().Value - 10;
        if (ScaleValue <= 0)
            ScaleValue = 0;

        // popualte the chart
        List<ChartEntry> chartEntries = new List<ChartEntry>();
        for(int i = WeightsHistory.Count -1 ; i >= 0; i--)
        {
            SKColor pointColor = SKColor.Parse("#77d065");

            if (i > 0 && WeightsHistory[i].Value > WeightsHistory[i - 1].Value)
                pointColor = SKColor.Parse("#FF0000");

            chartEntries.Add(new ChartEntry((float) (WeightsHistory[i].Value - ScaleValue))
            {
                ValueLabel = WeightsHistory[i].Value.ToString(),
                Label = WeightsHistory[i].Record.ToString("MM/dd/y"),
                Color = pointColor
            }) ;
        }

        ChartData = new LineChart() { Entries = chartEntries };
    }

}
