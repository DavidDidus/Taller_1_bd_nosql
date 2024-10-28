using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UnidadController(UnidadService unidadService) : ControllerBase
{
    private readonly UnidadService _unidadService = unidadService;

    [HttpGet]
    public async Task<ActionResult<List<Unidad>>> GetUnidades()
    {
        List<Unidad> unidades = await _unidadService.GetUnidadesAsync();
        return Ok(unidades);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Unidad>> GetUnidadById(string id)
    {
        var unidad = await _unidadService.GetUnidadByIdAsync(id);
        if (unidad == null)
        {
            return NotFound();
        }
        return Ok(unidad);
    }

    [HttpPost]
    public async Task<ActionResult<Unidad>> CreateUnidad(Unidad unidad)
    {
        await _unidadService.CreateUnidadAsync(unidad);
        return CreatedAtAction(nameof(GetUnidadById), new { id = unidad.Id }, unidad);
    }    
}
