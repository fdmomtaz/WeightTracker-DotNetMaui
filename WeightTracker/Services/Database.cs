using SQLite;
using WeightTracker.Models;

namespace WeightTracker.Services;

public class Database
{
    readonly SQLiteAsyncConnection _database;

    public Database(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Weight>().Wait();
    }

    public Task<List<Weight>> GetWeights()
    {
        return _database.Table<Weight>().ToListAsync();
    }

    public Task<int> SaveWeightAsync(Weight weight)
    {
        return _database.InsertAsync(weight);
    }

    public Task<int> DeleteAllWeightAsync()
    {
        return _database.DeleteAllAsync<Weight>();
    }

    public Task<int> DeleteWeightAsync(Weight weight)
    {
        return _database.DeleteAsync(weight);
    }
}
