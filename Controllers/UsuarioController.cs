using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Taller1; 

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly UsuarioCursoService _usuarioCursoService;

    public UsuarioController(UsuarioService usuarioService, UsuarioCursoService usuarioCursoService)
    {
        _usuarioService = usuarioService;
        _usuarioCursoService = usuarioCursoService;
    }
   
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioDTOLogin request)
    {
        try{
            await _usuarioService.LoginAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Login exitoso");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario request)
    {
        try{
            await _usuarioService.CrearUsuarioAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Registro exitoso");
    }

    [HttpPost("ingresar-curso")]
    public async Task<IActionResult> IngresarCurso([FromBody] UsuarioCurso request)
    {
        try
        {
            await _usuarioCursoService.CrearUsuarioCursoAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Usuario ingresado al curso exitosamente");
    }

    [HttpGet("cursos/{idUsuario}")]
    public async Task<IActionResult> GetCursosUsuario(string idUsuario)
    {
        try
        {
            var cursos = await _usuarioCursoService.ObtenerCursosUsuarioAsync(idUsuario);
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("actualizar-estado")]
    public async Task<IActionResult> ActualizarEstadoCurso([FromBody] UsuarioCurso request)
    {
        try
        {
            var usuarioCurso = await _usuarioCursoService.GetUsuarioCursoAsync(request.IdUsuario, request.IdCurso);
            if (usuarioCurso == null)
            {
                return NotFound("UsuarioCurso no encontrado");
            }

            usuarioCurso.Progreso = request.Progreso;
            usuarioCurso.Estado = request.Estado;
            usuarioCurso.Completado = request.Completado;

            await _usuarioCursoService.UpdateUsuarioCursoAsync(usuarioCurso);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok("Estado del curso actualizado exitosamente");
    }
}
