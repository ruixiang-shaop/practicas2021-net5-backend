using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Paciente;
using GestionCitasMedicas.Dtos.Register;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("pacientes")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService pacService;
        private readonly IMedicoService medicoService;
        private readonly ICitaService citaService;
        private readonly IMapper mapper;
        public PacienteController(IPacienteService pacService, IMedicoService medicoService, IMapper mapper,
            ICitaService citaService)
        {
            this.pacService = pacService;
            this.medicoService = medicoService;
            this.citaService = citaService;
            this.mapper = mapper;
        }

        // GET /pacientes
        [HttpGet]
        public async Task<IEnumerable<PacienteDTO>> GetPacientes()
        {
            ICollection<Paciente> pacs = await pacService.findAllAsync();
            return mapper.Map<ICollection<Paciente>, ICollection<PacienteDTO>>(pacs);
        }

        // Get /pacientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(long id)
        {
            Paciente pac = await pacService.findByIdAsync(id);
            if (pac is null)
            {
                return NotFound();
            }
            return mapper.Map<Paciente, PacienteDTO>(pac);
        }

        // Post /pacientes/add
        [HttpPost("add")]
        public async Task<ActionResult<PacienteDTO>> CreatePaciente(PacienteRegistroDTO pacRegDto)
        {
            Paciente pac = mapper.Map<PacienteRegistroDTO, Paciente>(pacRegDto);
            var idPacNew = await pacService.saveAsync(pac);
            if (idPacNew == null)
                return BadRequest();
            // Always add a random Medico to Paciente
            Medico med = await medicoService.getRandom();
            med.addPaciente(pac);
            await medicoService.updateAsync(med);
            return await GetPaciente((long)idPacNew);
        }

        // PUT /pacientes/update
        [HttpPut("update")]
        public async Task<ActionResult<PacienteDTO>> UpdatePaciente(PacienteDTO pacDto)
        {
            Paciente pac = mapper.Map<PacienteDTO, Paciente>(pacDto);
            var updated = await pacService.updateAsync(pac);
            if (!updated)
                return NotFound();
            return await GetPaciente(pac.id);
        }
        
        // DELETE /pacientes/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeletePaciente(long id)
        {
            Paciente pac = await pacService.findByIdAsync(id);
            foreach (Cita c in pac.citas) {
                await citaService.deleteAsync(c.id);
            }
            pac.medicos = new HashSet<MedicoPaciente>();
            pac.citas = new HashSet<Cita>();
            await pacService.updateAsync(pac);
            if (await pacService.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}