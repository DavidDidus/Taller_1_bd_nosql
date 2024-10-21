namespace Taller1;

using MongoDB.Driver;
using Microsoft.Extensions.Options;

public interface ICursoService {
    Task<List<Curso>> GetAllCursosAsync();
    
    Task<Curso> GetCursoByIdAsync(string Id);
    Task CreateCursoAsync(Curso curso);
    Task UpdateCursoAsync(string id, Curso updatedCurso);
    Task DeleteCursoAsync(string id);
}

public class CursoService : ICursoService {
    private readonly IMongoCollection<Curso> _cursos;

    public CursoService(IOptions<MongoDbSettings> mongoDbSettings) {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _cursos = database.GetCollection<Curso>("Cursos");
    }

    // Obtener todos los cursos
    public async Task<List<Curso>> GetAllCursosAsync() {
        return await _cursos.Find(curso => true).ToListAsync();
    }

    // Obtener curso por Id
    public async Task<Curso> GetCursoByIdAsync(string id) {
        return await _cursos.Find(curso => curso.Id.ToString() == id).FirstOrDefaultAsync();
    }

    // Crear un nuevo curso
    public async Task CreateCursoAsync(Curso curso) {
        await _cursos.InsertOneAsync(curso);
    }

    // Actualizar curso existente
    public async Task UpdateCursoAsync(string id, Curso updatedCurso) {
        await _cursos.ReplaceOneAsync(curso => curso.Id.ToString() == id, updatedCurso);
    }

    // Eliminar un curso
    public async Task DeleteCursoAsync(string id) {
        await _cursos.DeleteOneAsync(curso => curso.Id.ToString() == id);
    }
}
