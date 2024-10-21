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
    // Agrega más colecciones según lo necesario
}
