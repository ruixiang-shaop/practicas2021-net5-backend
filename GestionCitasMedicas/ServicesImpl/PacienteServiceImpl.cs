using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class PacienteServiceImpl : IPacienteService
    {
        private readonly IPacienteRepository repository;
        public PacienteServiceImpl(IPacienteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await repository.DeletePacienteAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Paciente>> findAllAsync()
        {
            try {
                return (ICollection<Paciente>) await repository.GetPacientesAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Paciente> findByIdAsync(long id)
        {
            try {
                return await repository.GetPacienteAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Paciente> saveAsync(Paciente pac)
        {
            try {
                return await repository.CreatePacienteAsync(pac);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Paciente> updateAsync(Paciente pac)
        {
            try {
                var updatedPac = await findByIdAsync(pac.id);
                if (updatedPac == null)
                    return null;
                if (pac.usuario != null) updatedPac.usuario = pac.usuario;
                if (pac.clave != null) updatedPac.clave = pac.clave;
                if (pac.nombre != null) updatedPac.nombre = pac.nombre;
                if (pac.apellidos != null) updatedPac.apellidos = pac.apellidos;
                if (pac.nss != null) updatedPac.nss = pac.nss;
                if (pac.numTarjeta != null) updatedPac.numTarjeta = pac.numTarjeta;
                if (pac.telefono != null) updatedPac.telefono = pac.telefono;
                if (pac.direccion != null) updatedPac.direccion = pac.direccion;
                if (pac.medicos != null) updatedPac.medicos = pac.medicos;
                if (pac.citas != null) updatedPac.citas = pac.citas;
                return await repository.UpdatePacienteAsync(updatedPac);
            } catch (Exception) {
                return null;
            }
        }
    }
}