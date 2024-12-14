using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class ComentarioCursoController : ControllerBase
{
    private readonly ComentarioCursoService _comentarioCursoService;

    public ComentarioCursoController(ComentarioCursoService comentarioCursoService)
    {
        _comentarioCursoService = comentarioCursoService;
    }

    [HttpPost]
    public async Task<IActionResult> CrearComentarioCurso([FromBody] ComentarioCursoRequest request)
    {
        await _comentarioCursoService.CrearComentarioCursoAsync(request.CursoId, request.Autor, request.Titulo, request.Detalle, request.Valoracion, request.Me_gusta, request.No_me_gusta);
        return Ok("Comentario de curso creado exitosamente");
    }

    [HttpGet("{cursoId}")]
    public async Task<IActionResult> ObtenerComentariosCurso(string cursoId)
    {
        var comentarios = await _comentarioCursoService.ObtenerComentariosCursoAsync(cursoId);
        return Ok(comentarios);
    }

    [HttpGet("comentario/{id}")]
    public async Task<IActionResult> ObtenerComentarioCursoPorId(string id)
    {
        var comentario = await _comentarioCursoService.ObtenerComentarioCursoPorIdAsync(id);
        if (comentario == null)
        {
            return NotFound();
        }
        return Ok(comentario);
    }
}