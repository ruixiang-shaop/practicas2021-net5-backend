using System.Collections.Generic;

namespace GestionCitasMedicas.Dtos.Medico
{
    public class MedicoDTO
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string numColegiado { get; set; }
        public ICollection<PacienteOnlyDTO> pacientes { get; set; }
        public ICollection<CitaDTO> citas { get; set; }
    }
}