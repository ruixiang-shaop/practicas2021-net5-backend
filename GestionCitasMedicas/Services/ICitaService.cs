using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface ICitaService
    {
        public HashSet<Cita> findAll();
        public Cita findById(long id);
        public Cita save(Cita cita);
        public bool delete(long id);
        public Cita update(Cita cita);
    }
}