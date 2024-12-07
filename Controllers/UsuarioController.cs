using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Taller1; 

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
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
}