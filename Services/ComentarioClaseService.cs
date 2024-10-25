using MongoDB.Driver;

using Taller1;

public class ComentarioClaseService{

    private readonly IMongoCollection<ComentarioClase> _comentariosClase;

    public ComentarioClaseService(MongoDbContext context)
    {
        _comentariosClase = context.ComentariosClase;
    }

    public async Task<List<ComentarioClase>> GetComentariosClaseAsync()
    {
        return await _comentariosClase.Find(comentarioClase => true).ToListAsync();
    }

    public async Task<ComentarioClase> GetComentarioClaseByIdAsync(string id)
    {
        return await _comentariosClase.Find(comentarioClase => comentarioClase.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ComentarioClase> CreateCursoAsycn(ComentarioClase comentarioClase)
    {
        await _comentariosClase.InsertOneAsync(comentarioClase);
        return comentarioClase;
    }
}