namespace Taller1
{
    using StackExchange.Redis;
    using System;

    public class RedisService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;
        private readonly IServer _server;

        public RedisService()
        {
            _connection = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { Environment.GetEnvironmentVariable("REDIS_URI") ?? throw new ArgumentNullException("REDIS_URI environment variable is not set") },
                User = Environment.GetEnvironmentVariable("REDIS_USER"),
                Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD")
            });
            _db = _connection.GetDatabase();
            var redisUri = Environment.GetEnvironmentVariable("REDIS_URI") ?? throw new ArgumentNullException("REDIS_URI environment variable is not set");
            _server = _connection.GetServer(redisUri);
        }

        // Obtener valor por clave
        public async Task<string?> GetAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }
        // Obtener todas las claves que coincidan con un patrón
        public async Task<IEnumerable<string>> GetAsyncKeys(string pattern)
        {
            var keys = new List<string>();
            await foreach (var key in _server.KeysAsync(pattern: pattern))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    keys.Add(key.ToString());
                }
            }
            return keys;
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
