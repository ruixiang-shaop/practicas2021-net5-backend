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
        private readonly ICitaRepository citaRepo;
        private readonly IDiagnosticoRepository diagRepo;
        private readonly IMedicoRepository medRepo;
        private readonly IPacienteRepository pacRepo;
        public CitaServiceImpl(ICitaRepository citaRepo, IDiagnosticoRepository diagRepo,
            IMedicoRepository medRepo, IPacienteRepository pacRepo)
        {
            this.citaRepo = citaRepo;
            this.diagRepo = diagRepo;
            this.medRepo = medRepo;
            this.pacRepo = pacRepo;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                var cita = await findByIdAsync(id);
                if (cita == null)
                    return true;
                if (cita.diagnostico != null)
                    await diagRepo.DeleteDiagnosticoAsync(cita.diagnostico.id);
                await citaRepo.DeleteCitaAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Cita>> findAllAsync()
        {
            try {
                return (ICollection<Cita>) await citaRepo.GetCitasAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Cita> findByIdAsync(long id)
        {
            try {
                return await citaRepo.GetCitaAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Cita cita)
        {
            try {
                Medico m = await medRepo.GetMedicoAsync(cita.medicoId);
                Paciente p = await pacRepo.GetPacienteAsync(cita.pacienteId);
                cita.medico = m;
                cita.paciente = p;
                return await citaRepo.CreateCitaAsync(cita);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<bool> updateAsync(Cita cita)
        {
            try {
                var updatedCita = await findByIdAsync(cita.id);
                if (updatedCita == null)
                    return false;
                if (cita.fechaHora != null) updatedCita.fechaHora = cita.fechaHora;
                if (cita.motivoCita != null) updatedCita.motivoCita = cita.motivoCita;
                if (cita.paciente != null)
                    updatedCita.pacienteId = cita.pacienteId;
                if (cita.medico != null)
                    updatedCita.medicoId = cita.medicoId;
                await citaRepo.UpdateCitaAsync(updatedCita);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}