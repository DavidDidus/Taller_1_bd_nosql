using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly CursoService _cursoService;

    public CursoController(CursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetCursos()
    {
        var cursos = await _cursoService.GetCursosAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCursoById(string id)
    {
        var curso = await _cursoService.GetCursoByIdAsync(id);
        if (curso == null)
        {
            return NotFound();
        }
        return Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> CreateCurso(Curso curso)
    {
        await _cursoService.CreateCursoAsync(curso);
        return CreatedAtAction(nameof(GetCursoById), new { id = curso.Id }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCurso(string id, Curso curso)
    {
        var exists = await _cursoService.UpdateCursoAsync(id, curso);
        if (!exists)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(string id)
    {
        var deleted = await _cursoService.DeleteCursoAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
