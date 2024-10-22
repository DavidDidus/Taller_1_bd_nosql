namespace Taller1;

using MongoDB.Driver;
using Microsoft.Extensions.Options;

public interface IComentarioService {
    Task<List<Comentario>> GetTopComentariosByCursoIdAsync(string cursoId);
    Task<List<Comentario>> GetAllComentariosByCursoIdAsync(string cursoId);
    Task CreateComentarioAsync(Comentario comentario);
    Task DeleteComentarioAsync(string id);
    Task<Comentario> GetAllComentariosByCurso(string cursoId);
}


public class ComentarioService : IComentarioService {
    private readonly IMongoCollection<Comentario> _comentarios;

    public ComentarioService(IOptions<MongoDbSettings> mongoDbSettings) {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _comentarios = database.GetCollection<Comentario>("Comentarios");
    }

    // Obtener los 3 comentarios más valorados de un curso
    public async Task<List<Comentario>> GetTopComentariosByCursoIdAsync(string cursoId) {
        return await _comentarios
            .Find(comentario => comentario.CursoId == cursoId)
            .SortByDescending(comentario => comentario.Valoracion)
            .Limit(3)
            .ToListAsync();
    }

    // Obtener todos los comentarios de un curso
    public async Task<List<Comentario>> GetAllComentariosByCursoIdAsync(string cursoId) {
        return await _comentarios
            .Find(comentario => comentario.CursoId == cursoId)
            .SortByDescending(comentario => comentario.Valoracion)
            .ToListAsync();
    }

    // Añadir un nuevo comentario a un curso
    public async Task CreateComentarioAsync(Comentario comentario) {
        await _comentarios.InsertOneAsync(comentario);
    }

    // Eliminar un comentario
    public async Task DeleteComentarioAsync(string id) {
        await _comentarios.DeleteOneAsync(comentario => comentario.Id == id);
    }
    public async Task<Comentario> GetAllComentariosByCurso(string cursoId) {
        return await _comentarios.Find(comentario => comentario.CursoId == cursoId).FirstOrDefaultAsync();
    }
}
