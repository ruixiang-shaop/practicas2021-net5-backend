using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class MedicoServiceImpl : IMedicoService
    {
        private readonly IMedicoRepository medRepo;
        private readonly IPacienteRepository pacRepo;
        private readonly ICitaRepository citaRepo;
        private readonly IDiagnosticoRepository diagRepo;
        public MedicoServiceImpl(IMedicoRepository medRepo, IPacienteRepository pacRepo,
            ICitaRepository citaRepo, IDiagnosticoRepository diagRepo)
        {
            this.medRepo = medRepo;
            this.pacRepo = pacRepo;
            this.citaRepo = citaRepo;
            this.diagRepo = diagRepo;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                Medico med = await medRepo.GetMedicoAsync(id);
                med.pacientes = new HashSet<MedicoPaciente>();
                await this.updateAsync(med);

                foreach (Cita c in med.citas) {
                    await diagRepo.DeleteDiagnosticoAsync(c.diagnostico.id);
                    await citaRepo.DeleteCitaAsync(c.id);
                }
                med.citas = new HashSet<Cita>();
                
                await medRepo.DeleteMedicoAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Medico>> findAllAsync()
        {
            try {
                return (ICollection<Medico>) await medRepo.GetMedicosAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> findByIdAsync(long id)
        {
            try {
                return await medRepo.GetMedicoAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> getRandom()
        {
            try {
                return await medRepo.GetMedicoRandomAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Medico med)
        {
            try {
                return await medRepo.CreateMedicoAsync(med);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<bool> updateAsync(Medico med)
        {
            try {
                var updatedMed = await findByIdAsync(med.id);
                if (updatedMed == null)
                    return false;
                if (med.usuario != null) updatedMed.usuario = med.usuario;
                if (med.clave != null) updatedMed.clave = med.clave;
                if (med.nombre != null) updatedMed.nombre = med.nombre;
                if (med.apellidos != null) updatedMed.apellidos = med.apellidos;
                if (med.numColegiado != null) updatedMed.numColegiado = med.numColegiado;
                if (med.pacientes != null && !med.pacientes.Equals(updatedMed.pacientes)) {
                    updatedMed.pacientes = med.pacientes;
                    foreach (var mp in updatedMed.pacientes) {
                        mp.medico = updatedMed;
                        mp.paciente = await pacRepo.GetPacienteAsync(mp.pacienteId);
                    }
                }
                await medRepo.UpdateMedicoAsync(updatedMed);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}