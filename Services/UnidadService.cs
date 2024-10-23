using MongoDB.Bson;
using MongoDB.Driver;
using Taller1;

public class UnidadService
{
    private readonly IMongoCollection<Unidad> _unidades;

    public UnidadService(MongoDbContext context)
    {
        _unidades = context.Unidades;
    }

    public async Task<List<Unidad>> GetUnidadesAsync()
    {
        return await _unidades.Find(unidad => true).ToListAsync();
    }

    public async Task<Unidad> GetUnidadByIdAsync(string id)
    {
        
        return await _unidades.Find(unidad => unidad.Id == id).FirstOrDefaultAsync();
    }
    public async Task<List<Unidad>> GetUnidadesByCursoIdAsync(string id)
    {
        return await _unidades.Find(unidad => unidad.CursoId == id).ToListAsync();
    }

    public async Task<Unidad> CreateUnidadAsync(Unidad unidad)
    {   
        if (string.IsNullOrEmpty(unidad.Id))
        {
            unidad.Id = ObjectId.GenerateNewId().ToString();
        }
        await _unidades.InsertOneAsync(unidad);
        return unidad;
    }

    public async Task<bool> UpdateUnidadAsync(string id, Unidad unidadUpdate)
    {
        var result = await _unidades.ReplaceOneAsync(unidad => unidad.Id == id, unidadUpdate);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteUnidadAsync(string id)
    {
        var result = await _unidades.DeleteOneAsync(unidad => unidad.Id == id);
        return result.DeletedCount > 0;
    }
}
