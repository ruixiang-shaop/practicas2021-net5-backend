using System;

namespace GestionCitasMedicas.Dtos.Medico
{
    public class CitaDTO
    {
        public long id { get; set; }
        public DateTime fechaHora { get; set; }
        public string motivoCita { get; set; }
        public PacienteOnlyDTO paciente { get; set; }
        public DiagnosticoOnlyDTO diagnostico { get; set; }
    }
}