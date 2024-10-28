using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class ComentarioCursoController(ComentarioCursoService comentarioCursoService) : ControllerBase {

    private readonly ComentarioCursoService _comentarioCursoService = comentarioCursoService;

    [HttpGet]
    public async Task<ActionResult<List<ComentarioCurso>>> GetComentariosCurso()
    {
        return await _comentarioCursoService.GetComentariosCursoAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComentarioCurso>> GetComentarioCursoById(string id)
    {
        var comentarioCurso = await _comentarioCursoService.GetComentarioCursoByIdAsync(id);
        if (comentarioCurso == null)
        {
            return NotFound();
        }
        return Ok(comentarioCurso);
    }

    [HttpPost]
    public async Task<ActionResult<ComentarioCurso>> CreateComentarioCurso(ComentarioCurso comentarioCurso)
    {
        ComentarioCurso comentarioCursoCreated = await _comentarioCursoService.CreateCursoAsycn(comentarioCurso);
        return CreatedAtAction(nameof(GetComentarioCursoById), new { id = comentarioCursoCreated.Id }, comentarioCursoCreated);
    }
}
