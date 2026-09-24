using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Modelos
{
    [Table("comentario")]
    public class Comentarios
    {
        [Key]
        [Column("id_comentario")]
        public int Id { get; set; }
        [Column("texto")]
        public string Texto { get; set; } = string.Empty; //es campo obligatorio

        [Column("fecha_creacion")]
        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); //fecha de creacion del comentario apartir de la fecha actual

        [Column("id_usuario")]
        public int UsuarioId { get; set; } //FK, el num puro
        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }  //obj completo

        [Column("id_incidencia")]
        public int IncidenciaId { get; set; } //FK, el num puro
        [ForeignKey(nameof(IncidenciaId))]
        public Incidencia? Incidencia { get; set; }  //obj completo


    }
}
