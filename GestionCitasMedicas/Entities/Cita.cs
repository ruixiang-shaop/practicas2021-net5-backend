using System;

namespace GestionCitasMedicas.Entities
{
    public class Cita
    {
        public long id { get; set; }
        public DateTime fechaHora { get; set; }
        public Paciente paciente { get; set; }
        public Medico medico { get; set; }
        public Diagnostico diagnostico { get; set; }
    }
}