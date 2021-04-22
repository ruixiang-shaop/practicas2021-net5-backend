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
        private readonly IPacienteRepository pacRepo;
        private readonly IMedicoRepository medRepo;
        private readonly ICitaRepository citaRepo;
        private readonly IDiagnosticoRepository diagRepo;
        public PacienteServiceImpl(IPacienteRepository pacRepo, IMedicoRepository medRepo,
            ICitaRepository citaRepo, IDiagnosticoRepository diagRepo)
        {
            this.pacRepo = pacRepo;
            this.medRepo = medRepo;
            this.citaRepo = citaRepo;
            this.diagRepo = diagRepo;
        }
        public async Task<bool> deleteAsync(long id)
        {
            try {

                Paciente pac = await pacRepo.GetPacienteAsync(id);
                pac.medicos = new HashSet<MedicoPaciente>();
                await this.updateAsync(pac);

                foreach (Cita c in pac.citas) {
                    await diagRepo.DeleteDiagnosticoAsync(c.diagnostico.id);
                    await citaRepo.DeleteCitaAsync(c.id);
                }
                pac.citas = new HashSet<Cita>();
                
                await pacRepo.DeletePacienteAsync(id);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ICollection<Paciente>> findAllAsync()
        {
            try {
                return (ICollection<Paciente>) await pacRepo.GetPacientesAsync();
            } catch (Exception) {
                return null;
            }
        }

        public async Task<Paciente> findByIdAsync(long id)
        {
            try {
                return await pacRepo.GetPacienteAsync(id);
            } catch (Exception) {
                return null;
            }
        }

        public async Task<long?> saveAsync(Paciente pac)
        {
            try {
                return await pacRepo.CreatePacienteAsync(pac);
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
                        mp.medico = await medRepo.GetMedicoAsync(mp.medicoId);
                        mp.paciente = updatedPac;
                    }
                }
                await pacRepo.UpdatePacienteAsync(updatedPac);
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}