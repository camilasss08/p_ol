using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Backend.Data;
using Backend.Modelos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioController(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [Authorize(Roles = "Admin")] //obliga a presentar un JWT valido
        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuario
        // REGISTRO
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostUsuario(RegistroUsuarioRequest request)
        {
            // Verificamos que el correo no esté repetido
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Correo == request.Correo);

            if (existe)
            {
                return Conflict("El correo ya está registrado.");
            }

            // Creamos el usuario
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Correo = request.Correo,
                EsAdministrador = request.EsAdministrador,
                LocalidadResidencia = request.LocalidadResidencia,
                FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            // Hasheamos la contraseña
            var hasher = new PasswordHasher<Usuario>();

            usuario.ContrasenaHash = hasher.HashPassword(
                usuario,
                request.Contrasena
            );

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            // No devolvemos la contraseña
            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuario.Id },
                new
                {
                    usuario.Id,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Correo,
                    usuario.EsAdministrador,
                    usuario.LocalidadResidencia,
                    usuario.FechaRegistro
                }
            );
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(
            int id,
            Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExiste(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Usuario/login
        // LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Buscamos al usuario por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (usuario == null)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            // Verificamos la contraseña
            var hasher = new PasswordHasher<Usuario>();

            var resultado = hasher.VerifyHashedPassword(
                usuario,
                usuario.ContrasenaHash,
                request.Contrasena
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            // Obtenemos la configuración JWT
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = Encoding.UTF8.GetBytes(
                jwtSettings["Key"]!
            );

            // Información que guardaremos dentro del token
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Correo
                ),

                new Claim(
                    ClaimTypes.Role,
                    usuario.EsAdministrador
                        ? "Admin"
                        : "Usuario"
                )
            };

            // Firmamos el token
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            // Creamos el JWT
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            // Convertimos el token a texto
            var tokenString =
                new JwtSecurityTokenHandler()
                    .WriteToken(token);

            return Ok(new
            {
                token = tokenString
            });
        }

        private bool UsuarioExiste(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }


    // Datos que recibe el registro
    public class RegistroUsuarioRequest
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public bool EsAdministrador { get; set; } = false;

        public string? LocalidadResidencia { get; set; }
    }


    // Datos que recibe el login
    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;
    }
}