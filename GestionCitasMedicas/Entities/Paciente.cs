using System.Collections.Generic;

namespace GestionCitasMedicas.Entities
{
    public class Paciente : Usuario
    {
        public string nss { get; set; }
        public string numTarjeta { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public HashSet<Medico> medicos { get; set; }
        public HashSet<Cita> citas { get; set; }
        
        public bool addCita(Cita c)
        {
            return citas.Add(c);
        }
        public bool removeCita(Cita c)
        {
            return citas.Remove(c);
        }
        public bool addMedico(Medico m)
        {
            return medicos.Add(m);
        }
        public bool removeMedico(Medico m)
        {
            return medicos.Remove(m);
        }
    }
}