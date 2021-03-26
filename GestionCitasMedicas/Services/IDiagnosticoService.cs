using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IDiagnosticoService
    {
        public HashSet<Diagnostico> findAll();
        public Diagnostico findById(long id);
        public Diagnostico save(Diagnostico diag);
        public bool delete(long id);
        public Diagnostico update(Diagnostico diag);
    }
}