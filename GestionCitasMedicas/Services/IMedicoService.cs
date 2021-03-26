using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IMedicoService
    {
        public HashSet<Medico> findAll();
        public Medico findById(long id);
        public Medico getRandom();
        public Medico save(Medico medico);
        public bool delete(long id);
        public Medico update(Medico medico);
    }
}