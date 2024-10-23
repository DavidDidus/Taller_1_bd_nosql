using MongoDB.Bson;
using MongoDB.Driver;
using Taller1;

public class ComentarioService
{
    private readonly IMongoCollection<Comentario> _comentarios;
    private readonly IMongoCollection<Unidad> _unidades;
    private readonly IMongoCollection<Clase> _clases;

    public ComentarioService(MongoDbContext context)
    {
        _comentarios = context.Comentarios;
        _unidades = context.Unidades;
        _clases = context.Clases;
    }

    public async Task<List<Comentario>> GetComentariosAsync()
    {
        return await _comentarios.Find(comentario => true).ToListAsync();
    }

    public async Task<Comentario> GetComentarioByIdAsync(string id)
    {
        return await _comentarios.Find(comentario => comentario.Id == id).FirstOrDefaultAsync();
    }
    public async Task<List<Comentario>> GetTop3ComentariosByCursoIdAsync(string cursoId)
    {
        // Obtener todas las unidades que pertenecen al curso
        var unidades = await _unidades.Find(unidad => unidad.CursoId == cursoId).ToListAsync();
        var unidadIds = unidades.Select(unidad => unidad.Id).ToList();

        // Obtener todas las clases que pertenecen a esas unidades
        var clases = await _clases.Find(clase => unidadIds.Contains(clase.UnidadId)).ToListAsync();
        var claseIds = clases.Select(clase => clase.Id).ToList();

        // Obtener los comentarios que pertenecen a esas clases
        var comentarios = await _comentarios.Find(comentario => claseIds.Contains(comentario.ClaseId))
                                            .SortByDescending(comentario => comentario.Valoracion)
                                            .Limit(3)
                                            .ToListAsync();

        return comentarios;
    }

    public async Task<Comentario> CreateComentarioAsync(Comentario comentario)
    {
        await _comentarios.InsertOneAsync(comentario);
        return comentario;
    }
    
    public async Task<List<Comentario>> GetComentariosByClaseIdAsync(string claseId)
    {
        return await _comentarios.Find(comentario => comentario.ClaseId == claseId).ToListAsync();
    }


  
}
