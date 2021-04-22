using System;
using System.ComponentModel.DataAnnotations;

namespace GestionCitasMedicas.Dtos.Cita
{
    public class CreateCitaDTO
    {
        [Required]
        public DateTime? fechaHora { get; set; }
        [Required]
        public string motivoCita { get; set; }
        [Required]
        public PacienteOnlyDTO paciente { get; set; }
        [Required]
        public MedicoOnlyDTO medico { get; set; }
    }
}