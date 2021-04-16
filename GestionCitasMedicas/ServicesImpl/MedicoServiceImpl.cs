using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class MedicoServiceImpl : IMedicoService
    {
        private readonly IMedicoRepository repository;
        public MedicoServiceImpl(IMedicoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await repository.DeleteMedicoAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Medico>> findAllAsync()
        {
            try {
                return (ICollection<Medico>) await repository.GetMedicosAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> findByIdAsync(long id)
        {
            try {
                return await repository.GetMedicoAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> getRandom()
        {
            try {
                return await repository.GetMedicoRandomAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> saveAsync(Medico med)
        {
            try {
                return await repository.CreateMedicoAsync(med);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Medico> updateAsync(Medico med)
        {
            try {
                var updatedMed = await findByIdAsync(med.id);
                if (updatedMed == null)
                    return null;
                if (med.usuario != null) updatedMed.usuario = med.usuario;
                if (med.clave != null) updatedMed.clave = med.clave;
                if (med.nombre != null) updatedMed.nombre = med.nombre;
                if (med.apellidos != null) updatedMed.apellidos = med.apellidos;
                if (med.numColegiado != null) updatedMed.numColegiado = med.numColegiado;
                if (med.pacientes != null) updatedMed.pacientes = med.pacientes;
                if (med.citas != null) updatedMed.citas = med.citas;
                return await repository.UpdateMedicoAsync(updatedMed);
            } catch (Exception) {
                return null;
            }
        }
    }
}