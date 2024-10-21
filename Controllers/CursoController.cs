namespace Taller1;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase {
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService) {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCursos() {
        var cursos = await _cursoService.GetAllCursosAsync();
        return Ok(cursos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCurso(CreateCursoDto cursoDto) {
        if (!ObjectId.TryParse(cursoDto.Id, out _)) {
            cursoDto.Id = ObjectId.GenerateNewId().ToString();
        }
        var curso = new Curso {
            Id = cursoDto.Id,
            Nombre = cursoDto.Nombre,
            DescripcionCorta = cursoDto.DescripcionBreve,
            Imagen = cursoDto.Imagen,

        };
        await _cursoService.CreateCursoAsync(curso);
        return CreatedAtAction(nameof(GetCursos), new { id = cursoDto.Id }, cursoDto);
    }
}
