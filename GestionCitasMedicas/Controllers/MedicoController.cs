using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Medico;
using GestionCitasMedicas.Dtos.Register;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("medicos")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService medService;
        private readonly ICitaService citaService;
        private readonly IMapper mapper;
        public MedicoController(IMedicoService medService, IMapper mapper, ICitaService citaService)
        {
            this.medService = medService;
            this.citaService = citaService;
            this.mapper = mapper;
        }

        // GET /medicos
        [HttpGet]
        public async Task<IEnumerable<MedicoDTO>> GetMedicos()
        {
            ICollection<Medico> meds = await medService.findAllAsync();
            return mapper.Map<ICollection<Medico>, ICollection<MedicoDTO>>(meds);
        }

        // Get /medicos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDTO>> GetMedico(long id)
        {
            Medico med = await medService.findByIdAsync(id);
            if (med is null)
            {
                return NotFound();
            }
            return mapper.Map<Medico, MedicoDTO>(med);
        }

        // Post /medicos/add
        [HttpPost("add")]
        public async Task<ActionResult<MedicoDTO>> CreateMedico(MedicoRegistroDTO medRegDto)
        {
            Medico med = mapper.Map<MedicoRegistroDTO, Medico>(medRegDto);
            var newId = await medService.saveAsync(med);
            if (newId == null)
                return BadRequest();
            return await GetMedico((long)newId);
        }

        // PUT /medicos/update
        [HttpPut("update")]
        public async Task<ActionResult<MedicoDTO>> UpdateMedico(MedicoDTO medDto)
        {
            Medico med = mapper.Map<MedicoDTO, Medico>(medDto);
            var updated = await medService.updateAsync(med);
            if (!updated)
                return NotFound();
            return await GetMedico(med.id);
        }
        
        // DELETE /medicos/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteMedico(long id)
        {
            Medico med = await medService.findByIdAsync(id);
            foreach (Cita c in med.citas) {
                await citaService.deleteAsync(c.id);
            }
            med.pacientes = new HashSet<MedicoPaciente>();
            med.citas = new HashSet<Cita>();
            await medService.updateAsync(med);
            if (await medService.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}