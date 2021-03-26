using System.Collections.Generic;

namespace GestionCitasMedicas.Entities
{
    public class Medico : Usuario
    {
        public string numColegiado { get; set; }
        public HashSet<Paciente> pacientes { get; set; }
        public HashSet<Cita> citas { get; set; }
        public bool addCita(Cita c)
        {
            return citas.Add(c);
        }
        public bool removeCita(Cita c)
        {
            return citas.Remove(c);
        }
        public bool addPaciente(Paciente p)
        {
            return pacientes.Add(p);
        }
        public bool removePaciente(Paciente p)
        {
            return pacientes.Remove(p);
        }
    }
}