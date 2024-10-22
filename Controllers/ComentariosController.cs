using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly ComentarioService _comentarioService;

    public ComentarioController(ComentarioService comentarioService)
    {
        _comentarioService = comentarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Comentario>>> GetComentarios()
    {
        var comentarios = await _comentarioService.GetComentariosAsync();
        return Ok(comentarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comentario>> GetComentarioById(string id)
    {
        var comentario = await _comentarioService.GetComentarioByIdAsync(id);
        if (comentario == null)
        {
            return NotFound();
        }
        return Ok(comentario);
    }

    [HttpPost]
    public async Task<ActionResult<Comentario>> CreateComentario(Comentario comentario)
    {
        await _comentarioService.CreateComentarioAsync(comentario);
        return CreatedAtAction(nameof(GetComentarioById), new { id = comentario.Id }, comentario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComentario(string id, Comentario comentario)
    {
        var exists = await _comentarioService.UpdateComentarioAsync(id, comentario);
        if (!exists)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComentario(string id)
    {
        var deleted = await _comentarioService.DeleteComentarioAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
