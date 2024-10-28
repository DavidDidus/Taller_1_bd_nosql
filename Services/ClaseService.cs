using MongoDB.Driver;
using Taller1;

public class ClaseService(MongoDbContext context)
{
    private readonly IMongoCollection<Clase> _clases = context.Clases;

    public async Task<List<Clase>> GetClasesAsync()
    {
        return await _clases.Find(clase => true).ToListAsync();
    }

    public async Task<Clase> GetClaseByIdAsync(string id)
    {
        return await _clases.Find(clase => clase.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Clase> CreateClaseAsync(Clase clase)
    {
        await _clases.InsertOneAsync(clase);
        return clase;
    }

    public async Task<bool> UpdateClaseAsync(string id, Clase claseUpdate)
    {
        var result = await _clases.ReplaceOneAsync(clase => clase.Id == id, claseUpdate);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteClaseAsync(string id)
    {
        var result = await _clases.DeleteOneAsync(clase => clase.Id == id);
        return result.DeletedCount > 0;
    }
}
