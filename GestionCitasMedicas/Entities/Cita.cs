using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCitasMedicas.Entities
{
    [Table("CITA")]
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long id { get; set; }
        [Required]
        [Column("FECHA_HORA")]
        public DateTime fechaHora { get; set; }
        [Required]
        [Column("MOTIVO_CITA")]
        public string motivoCita { get; set; }
        [Required]
        [Column("FK_PACIENTE")]
        public long pacienteId { get; set; }
        public virtual Paciente paciente { get; set; }
        [Required]
        [Column("FK_MEDICO")]
        public long medicoId { get; set; }
        public virtual Medico medico { get; set; }
        public virtual Diagnostico diagnostico { get; set; }
    }
}