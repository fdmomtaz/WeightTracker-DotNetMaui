using SQLite;

namespace WeightTracker.Models;

public class Weight
{
    [PrimaryKey, AutoIncrement]
    public int WeightId { get; set; }
    public DateTime Record { get; set; }
    public double Value { get; set; }
    public double? BodyFat { get; set; }
    public double? MuscleMass { get; set; }
    public double? WaterWeight { get; set; }
    public double? BoneMass { get; set; }
}