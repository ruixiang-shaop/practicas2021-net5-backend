using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface IDiagnosticoRepository
    {
        Task<Diagnostico> GetDiagnosticoAsync(long id);
        Task<IEnumerable<Diagnostico>> GetDiagnosticosAsync();
        Task<long> CreateDiagnosticoAsync(Diagnostico diag);
        Task UpdateDiagnosticoAsync(Diagnostico diag);
        Task DeleteDiagnosticoAsync(long id);
    }
}
