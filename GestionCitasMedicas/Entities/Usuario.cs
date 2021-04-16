using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCitasMedicas.Entities
{
    [Table("USUARIO")]
    public abstract class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long id { get; set; }
        [Required]
        [Column("NOMBRE")]
        public string nombre { get; set; }
        [Required]
        [Column("APELLIDOS")]
        public string apellidos { get; set; }
        [Required]
        [Column("USUARIO")]
        public string usuario { get; set; }
        [Required]
        [Column("CLAVE")]
        public string clave { get; set; }
    }
}