using System.Linq;
using AutoMapper;
using GestionCitasMedicas.Dtos;

namespace GestionCitasMedicas.Entities
{
    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<Diagnostico, Dtos.Diagnostico.DiagnosticoDTO>();
            CreateMap<Dtos.Diagnostico.DiagnosticoDTO, Diagnostico>();
            CreateMap<Diagnostico, Dtos.Diagnostico.CreateDiagnosticoDTO>();
            CreateMap<Dtos.Diagnostico.CreateDiagnosticoDTO, Diagnostico>();
            CreateMap<Cita, Dtos.Diagnostico.CitaDTO>();
            CreateMap<Dtos.Diagnostico.CitaDTO, Cita>();

            CreateMap<Cita, Dtos.Cita.CitaDTO>();
            CreateMap<Dtos.Cita.CitaDTO, Cita>();
            CreateMap<Cita, Dtos.Cita.CreateCitaDTO>();
            CreateMap<Dtos.Cita.CreateCitaDTO, Cita>();

            CreateMap<Medico, Dtos.Medico.MedicoDTO>()
                .ForMember(dto => dto.pacientes, opt => opt
                    .MapFrom(m => m.pacientes
                        .Select(mp => mp.paciente).ToList()));
            CreateMap<Dtos.Medico.MedicoDTO, Medico>()
                .ForMember(m => m.pacientes, opt => opt
                    .MapFrom(dto => dto.pacientes
                        .Select(p => new MedicoPaciente{medicoId=dto.id, pacienteId=p.id})));

            CreateMap<Cita, Dtos.Medico.CitaDTO>();
            CreateMap<Dtos.Medico.CitaDTO, Cita>();

            CreateMap<Paciente, Dtos.Paciente.PacienteDTO>()
                .ForMember(dto => dto.medicos, opt => opt
                    .MapFrom(p => p.medicos
                        .Select(mp => mp.medico).ToList()));
            CreateMap<Dtos.Paciente.PacienteDTO, Paciente>()
                .ForMember(p => p.medicos, opt => opt
                    .MapFrom(dto => dto.medicos
                        .Select(m => new MedicoPaciente{medicoId=m.id, pacienteId=dto.id})));

            CreateMap<Cita, Dtos.Paciente.CitaDTO>();
            CreateMap<Dtos.Paciente.CitaDTO, Cita>();

            CreateMap<Dtos.Register.PacienteRegistroDTO, Paciente>();
            CreateMap<Dtos.Register.MedicoRegistroDTO, Medico>();
            
            // Only-like mapping
            CreateMap<Paciente, Dtos.PacienteOnlyDTO>();
            CreateMap<Dtos.PacienteOnlyDTO, Paciente>();
            CreateMap<Medico, Dtos.MedicoOnlyDTO>();
            CreateMap<Dtos.MedicoOnlyDTO, Medico>();
            CreateMap<Diagnostico, Dtos.DiagnosticoOnlyDTO>();
            CreateMap<Dtos.DiagnosticoOnlyDTO, Diagnostico>();

            // many-to-many
            CreateMap<MedicoPaciente, MedicoOnlyDTO>();
            CreateMap<MedicoPaciente, PacienteOnlyDTO>();
            CreateMap<MedicoOnlyDTO, MedicoPaciente>();
            CreateMap<PacienteOnlyDTO, MedicoPaciente>();
        }
    }
}