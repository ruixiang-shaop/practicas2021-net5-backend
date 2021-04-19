using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Paciente;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("pacientes")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService service;
        private readonly IMedicoService medicoService;
        private readonly IMapper mapper;
        public PacienteController(IPacienteService service, IMedicoService medicoService, IMapper mapper)
        {
            this.service = service;
            this.medicoService = medicoService;
            this.mapper = mapper;
        }

        // GET /pacientes
        [HttpGet]
        public async Task<IEnumerable<PacienteDTO>> GetPacientes()
        {
            ICollection<Paciente> pacs = await service.findAllAsync();
            return mapper.Map<ICollection<Paciente>, ICollection<PacienteDTO>>(pacs);
        }

        // Get /pacientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(long id)
        {
            Paciente pac = await service.findByIdAsync(id);
            if (pac is null)
            {
                return NotFound();
            }
            return mapper.Map<Paciente, PacienteDTO>(pac);
        }

        // Post /pacientes/add
        [HttpPost("add")]
        public async Task<ActionResult<PacienteDTO>> CreatePaciente(PacienteDTO pacDto)
        {
            Paciente pac = mapper.Map<PacienteDTO, Paciente>(pacDto);
            Medico med = await medicoService.getRandom();
            pac.addMedico(med);
            med.addPaciente(pac);
            var idPacNew = await service.saveAsync(pac);
            if (idPacNew == null)
                return BadRequest();
            return await GetPaciente((long)idPacNew);
        }

        // PUT /pacientes/update
        [HttpPut("update")]
        public async Task<ActionResult<PacienteDTO>> UpdatePaciente(PacienteDTO pacDto)
        {
            Paciente pac = mapper.Map<PacienteDTO, Paciente>(pacDto);
            var updated = await service.updateAsync(pac);
            if (!updated)
                return NotFound();
            return await GetPaciente(pac.id);
        }
        
        // DELETE /pacientes/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            if (await service.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}