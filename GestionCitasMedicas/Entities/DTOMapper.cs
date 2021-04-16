using System.Linq;
using AutoMapper;

namespace GestionCitasMedicas.Entities
{
    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<Diagnostico, Dtos.Diagnostico.DiagnosticoDTO>();
            CreateMap<Dtos.Diagnostico.DiagnosticoDTO, Diagnostico>();
            CreateMap<Cita, Dtos.Diagnostico.CitaDTO>();
            CreateMap<Dtos.Diagnostico.CitaDTO, Cita>();

            CreateMap<Cita, Dtos.Cita.CitaDTO>();
            CreateMap<Dtos.Cita.CitaDTO, Cita>();

            CreateMap<Medico, Dtos.Medico.MedicoDTO>()
                .ForMember(dto => dto.pacientes, opt => opt
                    .MapFrom(m => m.pacientes
                        .Select(mp => mp.paciente).ToList()));
            CreateMap<Dtos.Medico.MedicoDTO, Medico>()
                .ForMember(m => m.pacientes, opt => opt
                    .MapFrom(dto => dto.pacientes))
                .AfterMap((dto, m) => {
                    foreach(var p in m.pacientes)
                    {
                        p.medico = m;
                    }
                });

            CreateMap<Cita, Dtos.Medico.CitaDTO>();
            CreateMap<Dtos.Medico.CitaDTO, Cita>();

            CreateMap<Paciente, Dtos.Paciente.PacienteDTO>()
                .ForMember(dto => dto.medicos, opt => opt
                    .MapFrom(p => p.medicos
                        .Select(mp => mp.medico).ToList()));
            CreateMap<Dtos.Paciente.PacienteDTO, Paciente>()
                .ForMember(p => p.medicos, opt => opt
                    .MapFrom(dto => dto.medicos))
                .AfterMap((dto, p) => {
                    foreach(var m in p.medicos)
                    {
                        m.paciente = p;
                    }
                });
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
        }
    }
}