using MongoDB.Driver;

using Taller1;

public class ComentarioCursoService{

    private readonly IMongoCollection<ComentarioCurso> _comentariosCurso;

    public ComentarioCursoService(MongoDbContext context)
    {
        _comentariosCurso = context.ComentariosCurso;
    }

    public async Task<List<ComentarioCurso>> GetComentariosCursoAsync()
    {
        return await _comentariosCurso.Find(comentarioCurso => true).ToListAsync();
    }

    public async Task<List<ComentarioCurso>> GetComentarioCursoByIdAsync(string id)
    {
        return await _comentariosCurso.Find(comentarioCurso => comentarioCurso.CursoId == id).ToListAsync();
    }

    public async Task<ComentarioCurso> CreateCursoAsycn(ComentarioCurso comentarioCurso)
    {
        await _comentariosCurso.InsertOneAsync(comentarioCurso);
        return comentarioCurso;
    }

    
}