using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Modelos
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }= string.Empty; //es campo obligatorio

        [Column("apellido")]
        public string Apellido { get; set; }= string.Empty;

        [Column("email")]
        public string Correo { get; set; } = string.Empty;
        [Column("contrasena")]
        public string ContrasenaHash { get; set; } = string.Empty;
        [Column("es_admin")]
        public bool EsAdministrador { get; set; } = false;
        
        [Column("localidad_residencia")]
        public string? LocalidadResidencia { get; set; }
        [Column("fecha_registro")]
        public DateOnly FechaRegistro { get; set; }= DateOnly.FromDateTime(DateTime.UtcNow); //fecha de registro del usuario apartir de la fecha actual
        
        
        //constructor
        public Usuario(string Nombre, string Apellido, string Correo, string ContrasenaHash, bool EsAdministrador , string LocalidadResidencia, DateOnly FechaRegistro)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Correo = Correo;
            this.ContrasenaHash = ContrasenaHash;
            this.EsAdministrador = EsAdministrador;
            this.LocalidadResidencia = LocalidadResidencia;
            this.FechaRegistro = FechaRegistro;
        }

        public Usuario()
        {
        }

        public ICollection<Incidencia> Incidencias { get; set; } = new List<Incidencia>(); //un usuario puede reportar muchas incidencias

        public ICollection<Comentarios> Comentarios { get; set; } = new List<Comentarios>(); //un usuario puede hacer muchos comentarios

    }
}
