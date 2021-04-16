using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCitasMedicas.Entities
{
    [Table("DIAGNOSTICO")]
    public class Diagnostico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long id { get; set; }
        [Required]
        [Column("VALORACION_ESPECIALISTA")]
        public string valoracionEspecialista { get; set; }
        [Required]
        [Column("ENFERMEDAD")]
        public string enfermedad { get; set; }
        [Required]
        [Column("FK_CITA")]
        public long citaId;
        public virtual Cita cita { get; set; }
    }
}