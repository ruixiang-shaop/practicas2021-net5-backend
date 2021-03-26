using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IPacienteService
    {
        public HashSet<Paciente> findAll();
        public Paciente findById(long id);
        public Paciente save(Paciente paciente);
        public bool delete(long id);
        public Paciente update(Paciente paciente);
    }
}