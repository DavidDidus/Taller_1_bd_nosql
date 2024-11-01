using MongoDB.Bson;
using MongoDB.Driver;
using Taller1;

public class UnidadService(MongoDbContext context)
{
    private readonly IMongoCollection<Unidad> _unidades = context.Unidades;
    private readonly IMongoCollection<Clase> _clases = context.Clases;

    public async Task<List<Unidad>> GetUnidadesAsync()
    {
        return await _unidades.Find(unidad => true).ToListAsync();
    }

    public async Task<Unidad> GetUnidadByIdAsync(string id)
    {
        var unidad = await _unidades.Find(unidad => unidad.Id == id).FirstOrDefaultAsync();

        if(unidad != null)
        {
            var clases = await _clases.Find(clase => clase.UnidadId == id).ToListAsync();
            unidad.Clases = clases;
        }
        else
        {
            throw new Exception("Unidad not found");
        }
        return unidad;
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

}
