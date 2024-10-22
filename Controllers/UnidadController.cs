using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UnidadController : ControllerBase
{
    private readonly UnidadService _unidadService;

    public UnidadController(UnidadService unidadService)
    {
        _unidadService = unidadService;
    }

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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUnidad(string id, Unidad unidad)
    {
        var exists = await _unidadService.UpdateUnidadAsync(id, unidad);
        if (!exists)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnidad(string id)
    {
        var deleted = await _unidadService.DeleteUnidadAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
