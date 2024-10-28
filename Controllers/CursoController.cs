using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class CursoController(CursoService cursoService, ComentarioCursoService comentarioService, UnidadService unidadService, ClaseService claseService) : ControllerBase
{
    private readonly CursoService _cursoService = cursoService;
    private readonly ComentarioCursoService _comentarioService = comentarioService;
    private readonly UnidadService _unidadService = unidadService;
    private readonly ClaseService _claseService = claseService;

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetCursos()
    {
        var cursos = await _cursoService.GetCursosAsync();
        var cursoList = cursos.Select(curso => new {
            Nombre = curso.Nombre,
            Imagen = curso.ImagenPrincipal,
            descripcion = curso.DescripcionBreve,
            valoracion = curso.Valoracion
        }).ToList();
        
        return Ok(cursoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCursoContenido(string id)
    {
        var curso = await _cursoService.GetCursoContenidoAsyc(id);

        if (curso == null)
        {
            return NotFound();
        }

        return Ok(curso);
    }

    [HttpGet("{id}/detalle")]
    public async Task<ActionResult<Curso>> GetCursoDetalle(string id)
    {
        var curso = await _cursoService.GetCursoDetalleAsync(id);

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

        if (curso.Unidades != null)
        {
            foreach (var unidad in curso.Unidades)
            {
                unidad.CursoId = curso.Id;
                await _unidadService.CreateUnidadAsync(unidad);

                if (unidad.Clases != null)
                {
                    foreach (var clase in unidad.Clases)
                    {
                        clase.UnidadId = unidad.Id;
                        await _claseService.CreateClaseAsync(clase);
                    }
                }
            }
        }
        return CreatedAtAction(nameof(GetCursoById), new { id = curso.Id }, curso);
    }

    public async Task<ActionResult<Curso>> GetCursoById(string id)
    {
        var curso = await _cursoService.GetCursoByIdAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        return Ok(curso);
    }
}
