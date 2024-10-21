using Microsoft.AspNetCore.Mvc;
namespace Taller1;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController : ControllerBase {
    private readonly IComentarioService _comentarioService;

    public ComentariosController(IComentarioService comentarioService) {
        _comentarioService = comentarioService;
    }

    // GET: api/comentarios/cursos/{cursoId}
    [HttpGet("cursos/{cursoId:length(24)}")]
    public async Task<ActionResult<List<Comentario>>> GetComentariosByCurso(string cursoId) {
        var comentarios = await _comentarioService.GetTopComentariosByCursoIdAsync(cursoId);
        return Ok(comentarios);
    }

    // GET: api/comentarios/cursos/{cursoId}/ver-mas
    [HttpGet("cursos/{cursoId:length(24)}/ver-mas")]
    public async Task<ActionResult<List<Comentario>>> GetAllComentariosByCurso(string cursoId) {
        var comentarios = await _comentarioService.GetAllComentariosByCurso(cursoId);
        return Ok(comentarios);
    }

    // POST: api/comentarios (Añadir nuevo comentario a un curso)
    [HttpPost]
    public async Task<ActionResult> AddComentario([FromBody] Comentario nuevoComentario) {
        await _comentarioService.CreateComentarioAsync(nuevoComentario);
        return Ok(nuevoComentario);
    }
}
