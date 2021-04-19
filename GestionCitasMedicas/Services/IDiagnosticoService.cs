using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IDiagnosticoService
    {
        public Task<ICollection<Diagnostico>> findAllAsync();
        public Task<Diagnostico> findByIdAsync(long id);
        public Task<long?> saveAsync(Diagnostico diag);
        public Task<bool> deleteAsync(long id);
        public Task<bool> updateAsync(Diagnostico diag);
    }
}