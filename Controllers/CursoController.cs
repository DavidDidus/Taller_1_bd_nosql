using Microsoft.AspNetCore.Mvc;


[Route("[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly CursoService _cursoService;
    private readonly ComentarioCursoService _comentarioService;
    private readonly UnidadService _unidadService;
    private readonly ClaseService _claseService;

    public CursoController(CursoService cursoService, ComentarioCursoService comentarioService, UnidadService unidadService, ClaseService claseService)
    {
        _cursoService = cursoService;
        _comentarioService = comentarioService;
        _unidadService = unidadService;
        _claseService = claseService;
    }
   

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetCursos()
    {
        var cursos = await _cursoService.GetCursosAsync();
        return Ok(cursos);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCursoDetalle(string id)
    {
        var curso = await _cursoService.GetCursoDetalleAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        return Ok(curso);
    }

    
   
}
