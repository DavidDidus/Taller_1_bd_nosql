namespace Taller1;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbContext {
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings) {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        _database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
    }

    public IMongoCollection<Curso> Cursos => _database.GetCollection<Curso>("Cursos");
    public IMongoCollection<Clase> Clases => _database.GetCollection<Clase>("Clases");
    public IMongoCollection<ComentarioCurso> ComentariosCurso => _database.GetCollection<ComentarioCurso>("ComentarioCurso");
    public IMongoCollection<Unidad> Unidades => _database.GetCollection<Unidad>("unidades");
    public IMongoCollection<ComentarioClase> ComentariosClase => _database.GetCollection<ComentarioClase>("ComentarioClase");

    
}
