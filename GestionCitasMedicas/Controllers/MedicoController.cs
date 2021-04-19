using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Medico;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("medicos")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService service;
        private readonly IMapper mapper;
        public MedicoController(IMedicoService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET /medicos
        [HttpGet]
        public async Task<IEnumerable<MedicoDTO>> GetMedicos()
        {
            ICollection<Medico> meds = await service.findAllAsync();
            return mapper.Map<ICollection<Medico>, ICollection<MedicoDTO>>(meds);
        }

        // Get /medicos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDTO>> GetMedico(long id)
        {
            Medico med = await service.findByIdAsync(id);
            if (med is null)
            {
                return NotFound();
            }
            return mapper.Map<Medico, MedicoDTO>(med);
        }

        // Post /medicos/add
        [HttpPost("add")]
        public async Task<ActionResult<MedicoDTO>> CreateMedico(MedicoDTO medDto)
        {
            Medico med = mapper.Map<MedicoDTO, Medico>(medDto);
            var newId = await service.saveAsync(med);
            if (newId == null)
                return BadRequest();
            Medico createdMedico = await service.findByIdAsync((long)newId);
            return mapper.Map<Medico, MedicoDTO>(createdMedico);
        }

        // PUT /medicos/update
        [HttpPut("update")]
        public async Task<ActionResult<MedicoDTO>> UpdateMedico(MedicoDTO medDto)
        {
            Medico med = mapper.Map<MedicoDTO, Medico>(medDto);
            var updated = await service.updateAsync(med);
            if (!updated)
                return NotFound();
            return await GetMedico(med.id);
        }
        
        // DELETE /medicos/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            if (await service.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}