
using Backend.Data;
using Backend.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComentarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comentario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetComentarios()
        {
            var comentarios = await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Incidencia)
                .ToListAsync();

            return Ok(comentarios.Select(c => new
            {
                c.Id,
                c.Texto,
                c.FechaCreacion,
                UsuarioId = c.UsuarioId,
                IncidenciaId = c.IncidenciaId
            }));
        }

        // GET: api/Comentario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Incidencia)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
                return NotFound();

            return Ok(new
            {
                comentario.Id,
                comentario.Texto,
                comentario.FechaCreacion,
                UsuarioId = comentario.UsuarioId,
                IncidenciaId = comentario.IncidenciaId
            });
        }

        // POST: api/Comentario
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostComentario(Comentarios comentario)
        {
            comentario.Id = 0;
            comentario.FechaCreacion =
                DateOnly.FromDateTime(DateTime.UtcNow);

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetComentario),
                new { id = comentario.Id },
                comentario
            );
        }

        // PUT: api/Comentario/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(
            int id,
            Comentarios comentario)
        {
            if (id != comentario.Id)
                return BadRequest();

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExiste(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Comentario/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
                return NotFound();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioExiste(int id)
        {
            return _context.Comentarios.Any(c => c.Id == id);
        }
    }
}
