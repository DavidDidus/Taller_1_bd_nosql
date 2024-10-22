using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ClaseController : ControllerBase
{
    private readonly ClaseService _claseService;

    public ClaseController(ClaseService claseService)
    {
        _claseService = claseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Clase>>> GetClases()
    {
        var clases = await _claseService.GetClasesAsync();
        return Ok(clases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Clase>> GetClaseById(string id)
    {
        var clase = await _claseService.GetClaseByIdAsync(id);
        if (clase == null)
        {
            return NotFound();
        }
        return Ok(clase);
    }

    [HttpPost]
    public async Task<ActionResult<Clase>> CreateClase(Clase clase)
    {
        await _claseService.CreateClaseAsync(clase);
        return CreatedAtAction(nameof(GetClaseById), new { id = clase.Id }, clase);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClase(string id, Clase clase)
    {
        var exists = await _claseService.UpdateClaseAsync(id, clase);
        if (!exists)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClase(string id)
    {
        var deleted = await _claseService.DeleteClaseAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
