using MongoDB.Bson;
using MongoDB.Driver;
using Taller1;

public class ComentarioService
{
    private readonly IMongoCollection<Comentario> _comentarios;

    public ComentarioService(MongoDbContext context)
    {
        _comentarios = context.Comentarios;
    }

    public async Task<List<Comentario>> GetComentariosAsync()
    {
        return await _comentarios.Find(comentario => true).ToListAsync();
    }

    public async Task<Comentario> GetComentarioByIdAsync(string id)
    {
        return await _comentarios.Find(comentario => comentario.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Comentario> CreateComentarioAsync(Comentario comentario)
    {
        await _comentarios.InsertOneAsync(comentario);
        return comentario;
    }

    public async Task<bool> UpdateComentarioAsync(string id, Comentario comentarioUpdate)
    {
        var result = await _comentarios.ReplaceOneAsync(comentario => comentario.Id == id, comentarioUpdate);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteComentarioAsync(string id)
    {
        var result = await _comentarios.DeleteOneAsync(comentario => comentario.Id == id);
        return result.DeletedCount > 0;
    }
}
