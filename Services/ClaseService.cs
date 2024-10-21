namespace Taller1;

using MongoDB.Driver;
using Microsoft.Extensions.Options;



public interface IClaseService {
    Task<List<Clase>> GetClasesByUnidadIdAsync(string cursoId, string unidadNombre);
    
    Task<Clase> GetClaseByIdAsync(string cursoId, string claseId);
    Task AddClaseToUnidadAsync(string cursoId, string unidadNombre, Clase clase);
}

public class ClaseService : IClaseService {
    private readonly IMongoCollection<Curso> _cursos;

    public ClaseService(IOptions<MongoDbSettings> mongoDbSettings) {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _cursos = database.GetCollection<Curso>("Cursos");
    }

    // Obtener todas las clases de una unidad por Id de la unidad
    public async Task<List<Clase>> GetClasesByUnidadIdAsync(string cursoId, string unidadNombre) {
        var curso = await _cursos.Find(curso => curso.Id.ToString() == cursoId).FirstOrDefaultAsync();
        var unidad = curso?.Unidades?.Find(unidad => unidad.Nombre == unidadNombre);
        return unidad?.Clases ?? new List<Clase>();
    }

    // Obtener detalles de una clase por su Id
    public async Task<Clase> GetClaseByIdAsync(string cursoId, string claseId) {
        var curso = await _cursos.Find(curso => curso.Id.ToString() == cursoId).FirstOrDefaultAsync();
        foreach (var unidad in curso.Unidades) {
            var clase = unidad.Clases.Find(clase => clase.Nombre == claseId);
            if (clase != null) {
                return clase;
            }
        }
        return null;
    }

    // Añadir una clase a una unidad
    public async Task AddClaseToUnidadAsync(string cursoId, string unidadNombre, Clase clase) {
        var curso = await _cursos.Find(curso => curso.Id.ToString() == cursoId).FirstOrDefaultAsync();
        var unidad = curso?.Unidades?.Find(unidad => unidad.Nombre == unidadNombre);
        if (unidad != null) {
            unidad.Clases.Add(clase);
            await _cursos.ReplaceOneAsync(curso => curso.Id.ToString() == cursoId, curso);
        }
    }
}
