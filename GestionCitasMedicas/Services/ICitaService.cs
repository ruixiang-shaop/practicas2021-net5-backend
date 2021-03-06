using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface ICitaService
    {
        public Task<ICollection<Cita>> findAllAsync();
        public Task<Cita> findByIdAsync(long id);
        public Task<long?> saveAsync(Cita cita);
        public Task<bool> deleteAsync(long id);
        public Task<bool> updateAsync(Cita cita);
    }
}