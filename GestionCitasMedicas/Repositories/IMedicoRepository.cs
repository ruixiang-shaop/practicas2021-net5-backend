using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface IMedicoRepository
    {
        Task<Medico> GetMedicoAsync(long id);
        Task<IEnumerable<Medico>> GetMedicosAsync();
        Task<Medico> GetMedicoRandomAsync();
        Task<long> CreateMedicoAsync(Medico diag);
        Task UpdateMedicoAsync(Medico diag);
        Task DeleteMedicoAsync(long id);
    }
}
