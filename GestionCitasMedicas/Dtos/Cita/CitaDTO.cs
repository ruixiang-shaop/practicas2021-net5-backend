using System;

namespace GestionCitasMedicas.Dtos.Cita
{
    public class CitaDTO
    {
        public long id { get; set; }
        public DateTime? fechaHora { get; set; }
        public string motivoCita { get; set; }
        public PacienteOnlyDTO paciente { get; set; }
        public MedicoOnlyDTO medico { get; set; }
        public DiagnosticoOnlyDTO diagnostico { get; set; }
    }
}