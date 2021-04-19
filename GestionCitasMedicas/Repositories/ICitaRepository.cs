using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface ICitaRepository
    {
        Task<Cita> GetCitaAsync(long id);
        Task<IEnumerable<Cita>> GetCitasAsync();
        Task<long> CreateCitaAsync(Cita cita);
        Task UpdateCitaAsync(Cita cita);
        Task DeleteCitaAsync(long id);
    }
}
