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
        private readonly IMedicoRepository medRepository;
        private readonly IPacienteRepository pacRepository;
        public MedicoServiceImpl(IMedicoRepository medRepository, IPacienteRepository pacRepository)
        {
            this.medRepository = medRepository;
            this.pacRepository = pacRepository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await medRepository.DeleteMedicoAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Medico>> findAllAsync()
        {
            try {
                return (ICollection<Medico>) await medRepository.GetMedicosAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> findByIdAsync(long id)
        {
            try {
                return await medRepository.GetMedicoAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> getRandom()
        {
            try {
                return await medRepository.GetMedicoRandomAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Medico med)
        {
            try {
                return await medRepository.CreateMedicoAsync(med);
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
                        mp.paciente = await pacRepository.GetPacienteAsync(mp.pacienteId);
                    }
                }
                await medRepository.UpdateMedicoAsync(updatedMed);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}