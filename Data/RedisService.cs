namespace Taller1
{
    using StackExchange.Redis;
    using System;

    public class RedisService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public RedisService()
        {
            _connection = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { Environment.GetEnvironmentVariable("REDIS_URI") },
                User = Environment.GetEnvironmentVariable("REDIS_USER"),
                Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD")
            });
            _db = _connection.GetDatabase();
        }

        // Obtener valor por clave
        public async Task<string> GetAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        // Guardar valor con clave
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _db.StringSetAsync(key, value, expiry);
        }

        // Eliminar clave
        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        // Verificar si existe una clave
        public async Task<bool> ExistsAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }
    }
    
}
