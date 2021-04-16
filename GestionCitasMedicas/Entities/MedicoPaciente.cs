using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCitasMedicas.Entities
{
    [Table("MEDICO_PACIENTE")]   
    public class MedicoPaciente
    {
        [Required]
        [Column("FK_MEDICO")]
        public long medicoId { get; set; }
        public virtual Medico medico { get; set; }
        [Required]
        [Column("FK_PACIENTE")]
        public long pacienteId { get; set; }
        public virtual Paciente paciente { get; set; }
    }
}