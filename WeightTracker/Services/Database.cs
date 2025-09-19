using SQLite;
using WeightTracker.Models;

namespace WeightTracker.Services;

public class Database
{
    private static string dbPath => Path.Combine(FileSystem.AppDataDirectory, "weights.db3");
    private SQLiteAsyncConnection? _database;

    public async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        if (_database == null)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Weight>();
        }

        return _database;
    }
    
    public async Task<Weight> GetWeight(int WeightId)
    {
        var db = await GetDatabaseAsync();
        return await db.Table<Weight>().Where(x => x.WeightId == WeightId).FirstOrDefaultAsync();
    }

    public async Task<List<Weight>> GetWeights()
    {
        var db = await GetDatabaseAsync();
        return await db.Table<Weight>().ToListAsync();
    }

    public async Task<int> SaveWeightAsync(Weight weight)
    {
        var db = await GetDatabaseAsync();
        return await db.InsertAsync(weight);
    }

    public async Task<int> DeleteAllWeightAsync()
    {
        var db = await GetDatabaseAsync();
        return await db.DeleteAllAsync<Weight>();
    }

    public async Task<int> DeleteWeightAsync(Weight weight)
    {
        var db = await GetDatabaseAsync();
        return await db.DeleteAsync(weight);
    }
}
