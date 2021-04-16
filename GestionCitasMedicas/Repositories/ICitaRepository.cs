using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface ICitaRepository
    {
        Task<Cita> GetCitaAsync(long id);
        Task<IEnumerable<Cita>> GetCitasAsync();
        Task<Cita> CreateCitaAsync(Cita cita);
        Task<Cita> UpdateCitaAsync(Cita cita);
        Task DeleteCitaAsync(long id);
    }
}
