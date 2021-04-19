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
        private readonly IPacienteRepository pacRepository;
        private readonly IMedicoRepository medRepository;
        public PacienteServiceImpl(IPacienteRepository pacRepository, IMedicoRepository medRepository)
        {
            this.pacRepository = pacRepository;
            this.medRepository = medRepository;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {
                await pacRepository.DeletePacienteAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Paciente>> findAllAsync()
        {
            try {
                return (ICollection<Paciente>) await pacRepository.GetPacientesAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Paciente> findByIdAsync(long id)
        {
            try {
                return await pacRepository.GetPacienteAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Paciente pac)
        {
            try {
                return await pacRepository.CreatePacienteAsync(pac);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<bool> updateAsync(Paciente pac)
        {
            try {
                var updatedPac = await findByIdAsync(pac.id);
                if (updatedPac == null)
                    return false;
                if (pac.usuario != null) updatedPac.usuario = pac.usuario;
                if (pac.clave != null) updatedPac.clave = pac.clave;
                if (pac.nombre != null) updatedPac.nombre = pac.nombre;
                if (pac.apellidos != null) updatedPac.apellidos = pac.apellidos;
                if (pac.nss != null) updatedPac.nss = pac.nss;
                if (pac.numTarjeta != null) updatedPac.numTarjeta = pac.numTarjeta;
                if (pac.telefono != null) updatedPac.telefono = pac.telefono;
                if (pac.direccion != null) updatedPac.direccion = pac.direccion;
                if (pac.medicos != null && !pac.medicos.Equals(updatedPac.medicos)) {
                    updatedPac.medicos = pac.medicos;
                    foreach (var mp in updatedPac.medicos) {
                        mp.medico = await medRepository.GetMedicoAsync(mp.medicoId);
                        mp.paciente = updatedPac;
                    }
                }
                await pacRepository.UpdatePacienteAsync(updatedPac);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}