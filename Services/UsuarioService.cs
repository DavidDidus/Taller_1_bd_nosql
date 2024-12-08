using StackExchange.Redis;
using System.Text.Json;
using Taller1;

public class UsuarioService
{
    private readonly RedisService _redisService;

    public UsuarioService(RedisService redisService)
    {
        _redisService = redisService;
    }

    
    public async Task CrearUsuarioAsync(Usuario usuario)
    {
        var usuarioKey = $"usuario:{usuario.Email}";
        var usuarioData = await _redisService.GetAsync(usuarioKey);

        if (usuarioData != null)
        {
            throw new Exception($"Usuario con email {usuario.Email} ya existe.");
        }

        var usuarioJson = JsonSerializer.Serialize(usuario);
        await _redisService.SetAsync(usuarioKey, usuarioJson);
        
    }


    public async Task<Usuario?> ObtenerUsuarioPorKeyAsync(string key)
    {
        var usuarioJson = await _redisService.GetAsync(key);
        if (string.IsNullOrEmpty(usuarioJson.ToString()))
        {
            return null;
        }

        return JsonSerializer.Deserialize<Usuario>(usuarioJson!);
    }

    public async Task LoginAsync(UsuarioDTOLogin usuario)
    {
        var usuarioKey= $"usuario:{usuario.Email}";
        var usuarioData = await _redisService.GetAsync(usuarioKey);

        if (usuarioData == null)
        {
            throw new Exception($"Usuario con email {usuario.Email} no encontrado.");
        }

        var usuarioDataJson = JsonSerializer.Deserialize<Usuario>(usuarioData!);
        if (usuarioDataJson.Password != usuario.Password)
        {
            throw new Exception("Contraseña incorrecta.");
        }
    }
    

    
}