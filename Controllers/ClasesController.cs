using Microsoft.AspNetCore.Mvc;
namespace Taller1;

[ApiController]
[Route("api/[controller]")]
public class ClasesController : ControllerBase {
    private readonly IClaseService _claseService;

    public ClasesController(IClaseService claseService) {
        _claseService = claseService;
    }

    // GET: api/clases/unidad/{unidadId}
    [HttpGet("unidad/{unidadId:length(24)}")]
    public async Task<ActionResult<List<Clase>>> GetClasesByUnidad(string unidadId,string unidadNombre) {
        var clases = await _claseService.GetClasesByUnidadIdAsync(unidadId,unidadNombre);
        return Ok(clases);
    }

    // GET: api/clases/{id}
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Clase>> GetClaseById(string id,string claseId) {
        var clase = await _claseService.GetClaseByIdAsync(id,claseId);

        if (clase == null) {
            return NotFound();
        }

        return Ok(clase);
    }
}
