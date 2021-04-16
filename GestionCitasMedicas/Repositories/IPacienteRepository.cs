using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface IPacienteRepository
    {
        Task<Paciente> GetPacienteAsync(long id);
        Task<IEnumerable<Paciente>> GetPacientesAsync();
        Task<Paciente> CreatePacienteAsync(Paciente pac);
        Task<Paciente> UpdatePacienteAsync(Paciente pac);
        Task DeletePacienteAsync(long id);
    }
}
