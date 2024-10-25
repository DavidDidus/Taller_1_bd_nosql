using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("[controller]")]
[ApiController]
public class ComentarioClaseController : ControllerBase {

    private readonly ComentarioClaseService _comentarioClaseService;

    public ComentarioClaseController(ComentarioClaseService comentarioClaseService)
    {
        _comentarioClaseService = comentarioClaseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ComentarioClase>>> GetComentariosClase()
    {
        return await _comentarioClaseService.GetComentariosClaseAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComentarioClase>> GetComentarioClaseById(string id)
    {
        ComentarioClase comentarioClase = await _comentarioClaseService.GetComentarioClaseByIdAsync(id);
        if (comentarioClase == null)
        {
            return NotFound();
        }
        return Ok(comentarioClase);
    }

    [HttpPost]
    public async Task<ActionResult<ComentarioClase>> CreateComentarioClase(ComentarioClase comentarioClase)
    {
        ComentarioClase comentarioClaseCreated = await _comentarioClaseService.CreateCursoAsycn(comentarioClase);
        return CreatedAtAction(nameof(GetComentarioClaseById), new { id = comentarioClaseCreated.Id }, comentarioClaseCreated);
    }
}