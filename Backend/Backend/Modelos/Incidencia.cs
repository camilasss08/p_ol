using Backend.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Modelos
{
    [Table("incidencia")]
    public class Incidencia
    {
        [Key]
        [Column("id_incidencia")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El título es obligatorio.")]
        [Column("titulo")]
       public string Titulo { get; set; } = string.Empty; //es campo obligatorio
        [Required(ErrorMessage ="Los detalles son obligatorios")]
        [Column("detalles")]
        public string Detalles { get; set; } = string.Empty; //es campo obligatorio
        [Required(ErrorMessage ="La direccion es obligatoria")]
        [Column("direccion")]
        public string Direccion{ get; set; }= string.Empty; //es campo obligatorio
        [Column("latitud")]
        public double Latitud { get; set; } //es campo obligatorio
        [Column("longitud")]
        public double Longitud { get; set; } //es campo obligatorio
        [Column("estado")]
        public EstadoIncidencia Estado { get; set; } = EstadoIncidencia.Pendiente;

      

        [Column("fecha_creacion")]
        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); //fecha de creacion de la incidencia apartir de la fecha actual
        [Column("fecha_actualizacion")] 
        public DateOnly? FechaActualizacion { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); //fecha de actualizacion de la incidencia apartir de la fecha actual
        [Column("id_usuario")]
        public int UsuarioId { get; set; } //FK, el num puro
        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }  //obj completo
        [Column("id_categoria")]
        public int CategoriaId { get; set; } //id FK , el num puro
        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; } //obj completo
        [Column("id_comentario")]
        public ICollection<Comentarios> Comentarios { get; set; } = new List<Comentarios>(); //una incidencia puede tener muchos comentarios
    }
}
