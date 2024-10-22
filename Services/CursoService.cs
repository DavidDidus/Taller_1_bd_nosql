using MongoDB.Driver;

using Taller1;

public class CursoService
{
    private readonly IMongoCollection<Curso> _cursos;

    public CursoService(MongoDbContext context)
    {
        _cursos = context.Cursos;
    }

    public async Task<List<Curso>> GetCursosAsync()
    {
        return await _cursos.Find(id => true).ToListAsync();
    }

    public async Task<Curso> GetCursoByIdAsync(string id)
    {
        return await _cursos.Find(curso => curso.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Curso> CreateCursoAsync(Curso curso)
    {
        await _cursos.InsertOneAsync(curso);
        return curso;
    }

    public async Task<bool> UpdateCursoAsync(string id, Curso cursoUpdate)
    {
        var result = await _cursos.ReplaceOneAsync(curso => curso.Id == id, cursoUpdate);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteCursoAsync(string id)
    {
        var result = await _cursos.DeleteOneAsync(curso => curso.Id == id);
        return result.DeletedCount > 0;
    }
}
