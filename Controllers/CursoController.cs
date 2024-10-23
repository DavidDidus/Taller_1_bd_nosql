using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly CursoService _cursoService;
    private readonly ComentarioService _comentarioService;
    private readonly UnidadService _unidadService;
    private readonly ClaseService _claseService;

    public CursoController(CursoService cursoService, ComentarioService comentarioService, UnidadService unidadService, ClaseService claseService)
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
    public async Task<ActionResult<Curso>> GetCursoById(string id)
    {
        var curso = await _cursoService.GetCursoByIdAsync(id);

        if (curso == null)
        {
            return NotFound();
        }
        var comentarios = await _comentarioService.GetTop3ComentariosByCursoIdAsync(id);
        var unidades = await _unidadService.GetUnidadesByCursoIdAsync(id);

        var detalleCurso = new {
            curso,
            unidades,
            comentarios,
        };

        return Ok(detalleCurso);
    }
    
    [HttpGet]
    [Route("{cursoId}/unidades/{unidadId}/clases/{claseId}")]
    public async Task<IActionResult> GetClaseDetalle(string cursoId, string unidadId, string claseId)
    {
        var clase = await _claseService.GetClaseByIdAsync(claseId);
        if (clase == null)
        {
            return NotFound();
        }

        var comentarios = await _comentarioService.GetComentariosByClaseIdAsync(claseId);

        var claseDetalle = new {
            clase,
            comentarios
        };

        return Ok(claseDetalle);
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> CreateCurso([FromBody] CursoDto cursoDto)
    {
        var curso = new Curso{
            Nombre = cursoDto.Nombre,
            DescripcionBreve = cursoDto.DescripcionBreve,
            ImagenPrincipal = cursoDto.ImagenPrincipal,
            ImagenBanner = cursoDto.ImagenBanner,
            Valoracion = 0,
            CantidadInscritos = 0

        };
        var creado = await _cursoService.CreateCursoAsync(curso);
        if (creado == null){
            return BadRequest("Error al crear el curso");
        }

        foreach (var unidadDto in cursoDto.Unidades)
        {
            var unidad = new Unidad
            {
                CursoId = creado.Id,
                Nombre = unidadDto.Nombre,
                NumeroOrden = unidadDto.numeroOrden,
                Clases = new List<string>() // Lista de IDs de clases
            };

            var unidadCreada = await _unidadService.CreateUnidadAsync(unidad);

            foreach (var claseDto in unidadDto.Clases)
            {
                var clase = new Clase
                {
                    UnidadId = unidadCreada.Id,
                    NumeroOrden = claseDto.NumeroOrden,
                    Nombre = claseDto.Nombre,
                    Descripcion = claseDto.Descripcion,
                    VideoUrl = claseDto.VideoUrl,
                    Adjuntos = claseDto.Adjuntos
                };

                var claseCreada = await _claseService.CreateClaseAsync(clase);
                unidad.Clases.Add(claseCreada.Id); // Agrega el ID de la clase a la lista
            }

            await _unidadService.UpdateUnidadAsync(unidadCreada.Id, unidad); // Actualiza la unidad con los IDs de las clases
        }
        
    return Ok(curso);
    }
}
