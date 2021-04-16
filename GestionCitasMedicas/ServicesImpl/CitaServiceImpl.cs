using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class CitaServiceImpl : ICitaService
    {
        private readonly ICitaRepository repository;
        public CitaServiceImpl(ICitaRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                var cita = await findByIdAsync(id);
                if (cita == null)
                    return true;
                cita.medico.removeCita(cita);
                cita.paciente.removeCita(cita);
                await repository.DeleteCitaAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Cita>> findAllAsync()
        {
            try {
                return (ICollection<Cita>) await repository.GetCitasAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Cita> findByIdAsync(long id)
        {
            try {
                return await repository.GetCitaAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Cita> saveAsync(Cita cita)
        {
            try {
                return await repository.CreateCitaAsync(cita);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Cita> updateAsync(Cita cita)
        {
            try {
                var updatedCita = await findByIdAsync(cita.id);
                if (updatedCita == null)
                    return null;
                updatedCita.fechaHora = cita.fechaHora;
                if (cita.motivoCita != null) updatedCita.motivoCita = cita.motivoCita;
                if (cita.paciente != null) updatedCita.paciente = cita.paciente;
                if (cita.medico != null) updatedCita.medico = cita.medico;
                if (cita.diagnostico != null) updatedCita.diagnostico = cita.diagnostico;
                return await repository.UpdateCitaAsync(updatedCita);
            } catch (Exception) {
                return null;
            }
        }
    }
}