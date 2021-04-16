using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class DiagnosticoServiceImpl : IDiagnosticoService
    {
        private readonly IDiagnosticoRepository repository;
        public DiagnosticoServiceImpl(IDiagnosticoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await repository.DeleteDiagnosticoAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Diagnostico>> findAllAsync()
        {
            try {
                return (ICollection<Diagnostico>) await repository.GetDiagnosticosAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Diagnostico> findByIdAsync(long id)
        {
            try {
                return await repository.GetDiagnosticoAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Diagnostico> saveAsync(Diagnostico diag)
        {
            try {
                diag.cita.diagnostico = diag;
                return await repository.CreateDiagnosticoAsync(diag);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Diagnostico> updateAsync(Diagnostico diag)
        {
            try {
                var updatedDiag = await findByIdAsync(diag.id);
                if (updatedDiag == null)
                    return null;
                if (diag.valoracionEspecialista != null) updatedDiag.valoracionEspecialista = diag.valoracionEspecialista;
                if (diag.enfermedad != null) updatedDiag.enfermedad = diag.enfermedad;
                if (diag.cita != null) updatedDiag.cita = diag.cita;
                return await repository.UpdateDiagnosticoAsync(updatedDiag);
            } catch (Exception) {
                return null;
            }
        }
    }
}