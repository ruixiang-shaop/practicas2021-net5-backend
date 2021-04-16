using System.Collections.Generic;

namespace GestionCitasMedicas.Dtos.Paciente
{
    public class PacienteDTO
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string nss { get; set; }
        public string numTarjeta { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public ICollection<MedicoOnlyDTO> medicos { get; set; }
        public ICollection<CitaDTO> citas { get; set; }
    }
}