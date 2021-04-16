using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IPacienteService
    {
        public Task<ICollection<Paciente>> findAllAsync();
        public Task<Paciente> findByIdAsync(long id);
        public Task<Paciente> saveAsync(Paciente pac);
        public Task<bool> deleteAsync(long id);
        public Task<Paciente> updateAsync(Paciente pac);
    }
}