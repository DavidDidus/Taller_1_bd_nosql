using System.Text.Json;

namespace Taller1
{
    public class UsuarioCursoService
    {
        private readonly RedisService _redisService;
        private readonly CursoService _cursoService;

        public UsuarioCursoService(RedisService redisService, CursoService cursoService)
        {
            _redisService = redisService;
            _cursoService = cursoService;
        }

        public async Task CrearUsuarioCursoAsync(UsuarioCurso usuarioCurso)
        {
            var usuarioKey = $"usuario:{usuarioCurso.IdUsuario}";
            var usuarioData = await _redisService.GetAsync(usuarioKey);

            if(usuarioData == null)
            {
                throw new Exception($"Usuario con id {usuarioCurso.IdUsuario} no existe.");
            }

            var usuarioCursoKey = $"usuarioCurso:{usuarioCurso.IdUsuario}:{usuarioCurso.IdCurso}";
            var usuarioCursoData = await _redisService.GetAsync(usuarioCursoKey);

            if (usuarioCursoData != null)
            {
                throw new Exception($"UsuarioCurso con idUsuario {usuarioCurso.IdUsuario} y idCurso {usuarioCurso.IdCurso} ya existe.");
            }

            usuarioCurso.Completado = false;
            usuarioCurso.Progreso = 0;
            usuarioCurso.Estado = "INICIADO";
            usuarioCurso.FechaInicio = DateTime.Now;

            var usuarioCursoJson = JsonSerializer.Serialize(usuarioCurso);
            await _cursoService.PatchCantidadInscritos(usuarioCurso.IdCurso);
            await _redisService.SetAsync(usuarioCursoKey, usuarioCursoJson);
        }

        public async Task<UsuarioCurso?> ObtenerUsuarioCursoPorKeyAsync(string key)
        {
            var usuarioCursoJson = await _redisService.GetAsync(key);
            if (usuarioCursoJson == null || string.IsNullOrEmpty(usuarioCursoJson.ToString()))
            {
                return null;
            }

            return JsonSerializer.Deserialize<UsuarioCurso>(usuarioCursoJson!);
        }

        public async Task<List<UsuarioCurso>> ObtenerCursosUsuarioAsync(string idUsuario)
        {
            var usuarioCursoKeys = await _redisService.GetAsyncKeys($"usuarioCurso:{idUsuario}:*");
            var usuarioCursos = new List<UsuarioCurso>();

            foreach (var key in usuarioCursoKeys)
            {
                var usuarioCurso = await ObtenerUsuarioCursoPorKeyAsync(key);
                if (usuarioCurso != null)
                {
                    usuarioCursos.Add(usuarioCurso);
                }
            }

            return usuarioCursos;
        }
        public async Task<UsuarioCurso?> GetUsuarioCursoAsync(string idUsuario, string idCurso)
        {
            var key = $"usuarioCurso:{idUsuario}:{idCurso}";
            var value = await _redisService.GetAsync(key);
            return value == null ? null : JsonSerializer.Deserialize<UsuarioCurso>(value);
        }

        public async Task<bool> UpdateUsuarioCursoAsync(UsuarioCurso usuarioCursoUpdate)
        {
            var key = $"usuarioCurso:{usuarioCursoUpdate.IdUsuario}:{usuarioCursoUpdate.IdCurso}";
            var value = JsonSerializer.Serialize(usuarioCursoUpdate);
            await _redisService.SetAsync(key, value);
            return true;
        }
    }
}