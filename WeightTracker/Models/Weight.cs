using SQLite;

namespace WeightTracker.Models;

public class Weight
{
    [PrimaryKey, AutoIncrement]
    public int WeightId { get; set; }
    public DateTime Record { get; set; }
    public double Value { get; set; }
    public double? BodyFatPercent { get; set; }
    public double? MuscleMassPercent { get; set; }
    public double? WaterWeightPercent { get; set; }
    public double? BoneMassPercent { get; set; }
}