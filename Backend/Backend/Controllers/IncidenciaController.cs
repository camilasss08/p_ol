
using Backend.Data;
using Backend.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IncidenciaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Incidencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetIncidencias()
        {
            var incidencias = await _context.Incidencias
                .Include(i => i.Usuario)
                .Include(i => i.Categoria)
                .ToListAsync();

            return Ok(incidencias.Select(i => new
            {
                i.Id,
                i.Titulo,
                i.Detalles,
                i.Direccion,
                i.Latitud,
                i.Longitud,
                i.Estado,
                i.FechaCreacion,
                i.FechaActualizacion,
                UsuarioId = i.UsuarioId,
                CategoriaId = i.CategoriaId
            }));
        }

        // GET: api/Incidencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetIncidencia(int id)
        {
            var incidencia = await _context.Incidencias
                .Include(i => i.Usuario)
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (incidencia == null)
                return NotFound();

            return Ok(new
            {
                incidencia.Id,
                incidencia.Titulo,
                incidencia.Detalles,
                incidencia.Direccion,
                incidencia.Latitud,
                incidencia.Longitud,
                incidencia.Estado,
                incidencia.FechaCreacion,
                incidencia.FechaActualizacion,
                UsuarioId = incidencia.UsuarioId,
                CategoriaId = incidencia.CategoriaId
            });
        }

        // POST: api/Incidencia
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostIncidencia(Incidencia incidencia)
        {
            incidencia.Id = 0;
            incidencia.FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            incidencia.FechaActualizacion = DateOnly.FromDateTime(DateTime.UtcNow);
            incidencia.Estado = EstadoIncidencia.Pendiente;

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetIncidencia),
                new { id = incidencia.Id },
                incidencia
            );
        }

        // PUT: api/Incidencia/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidencia(
            int id,
            Incidencia incidencia)
        {
            if (id != incidencia.Id)
                return BadRequest();

            incidencia.FechaActualizacion =
                DateOnly.FromDateTime(DateTime.UtcNow);

            _context.Entry(incidencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenciaExiste(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Incidencia/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidencia(int id)
        {
            var incidencia = await _context.Incidencias.FindAsync(id);

            if (incidencia == null)
                return NotFound();

            _context.Incidencias.Remove(incidencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidenciaExiste(int id)
        {
            return _context.Incidencias.Any(e => e.Id == id);
        }
    }
}
