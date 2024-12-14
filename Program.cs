using Taller1;
using DotNetEnv;
using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde el archivo .env
Env.Load();

// Configuración de MongoDB usando variables de entorno
var mongoDbSettings = new MongoDbSettings
{
    ConnectionString = Environment.GetEnvironmentVariable("MONGO_URI"),
    DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME")
};

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoDbSettings.ConnectionString;
    options.DatabaseName = mongoDbSettings.DatabaseName;
});

var neo4jUri = Environment.GetEnvironmentVariable("NEO4J_URI");
var neo4jUser = Environment.GetEnvironmentVariable("NEO4J_USERNAME");
var neo4jPassword = Environment.GetEnvironmentVariable("NEO4J_PASSWORD");

await using var driver = GraphDatabase.Driver(neo4jUri, AuthTokens.Basic(neo4jUser, neo4jPassword));
await driver.VerifyConnectivityAsync();

builder.Services.AddSingleton(driver);

// Servicios personalizados
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<RedisService>();

// Configuración de controladores
builder.Services.AddControllers();

// Registro de servicios
builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<UnidadService>();
builder.Services.AddScoped<ClaseService>();
builder.Services.AddScoped<ComentarioCursoService>();
builder.Services.AddScoped<ComentarioClaseService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioCursoService>();

// Swagger para documentación de la API (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Urls.Add("http://*:5012");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
