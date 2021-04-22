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
        private readonly IDiagnosticoRepository diagRepo;
        private readonly ICitaRepository citaRepo;
        public DiagnosticoServiceImpl(IDiagnosticoRepository diagRepo, ICitaRepository citaRepo)
        {
            this.diagRepo = diagRepo;
            this.citaRepo = citaRepo;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await diagRepo.DeleteDiagnosticoAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Diagnostico>> findAllAsync()
        {
            try {
                return (ICollection<Diagnostico>) await diagRepo.GetDiagnosticosAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Diagnostico> findByIdAsync(long id)
        {
            try {
                return await diagRepo.GetDiagnosticoAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Diagnostico diag)
        {
            try {
                Cita c = await citaRepo.GetCitaAsync(diag.citaId);
                diag.cita = c;
                return await diagRepo.CreateDiagnosticoAsync(diag);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<bool> updateAsync(Diagnostico diag)
        {
            try {
                var updatedDiag = await findByIdAsync(diag.id);
                if (updatedDiag == null)
                    return false;
                if (diag.valoracionEspecialista != null) updatedDiag.valoracionEspecialista = diag.valoracionEspecialista;
                if (diag.enfermedad != null) updatedDiag.enfermedad = diag.enfermedad;
                if (diag.cita != null) updatedDiag.citaId = diag.citaId;
                await diagRepo.UpdateDiagnosticoAsync(updatedDiag);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}