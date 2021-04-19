using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Diagnostico;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("diagnosticos")]
    public class DiagnosticoController : ControllerBase
    {
        private readonly IDiagnosticoService service;
        private readonly IMapper mapper;
        public DiagnosticoController(IDiagnosticoService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET /diagnosticos
        [HttpGet]
        public async Task<IEnumerable<DiagnosticoDTO>> GetDiagnosticos()
        {
            ICollection<Diagnostico> diags = await service.findAllAsync();
            return mapper.Map<ICollection<Diagnostico>, ICollection<DiagnosticoDTO>>(diags);
        }

        // Get /diagnosticos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoDTO>> GetDiagnostico(long id)
        {
            Diagnostico diag = await service.findByIdAsync(id);
            if (diag is null)
            {
                return NotFound();
            }
            return mapper.Map<Diagnostico, DiagnosticoDTO>(diag);
        }

        // Post /diagnosticos/add
        [HttpPost("add")]
        public async Task<ActionResult<DiagnosticoDTO>> CreateDiagnostico(DiagnosticoDTO diagDto)
        {
            Diagnostico diag = mapper.Map<DiagnosticoDTO, Diagnostico>(diagDto);
            Diagnostico diagNew = await service.saveAsync(diag);
            if (diagNew != null)
                return mapper.Map<Diagnostico, DiagnosticoDTO>(diagNew);
            return BadRequest();
        }

        // PUT /diagnosticos/update
        [HttpPut("update")]
        public async Task<ActionResult<DiagnosticoDTO>> UpdateDiagnostico(DiagnosticoDTO diagDto)
        {
            Diagnostico diag = mapper.Map<DiagnosticoDTO, Diagnostico>(diagDto);
            Diagnostico diagUpdated = await service.updateAsync(diag);
            if (diagUpdated == null)
                return NotFound();
            return mapper.Map<Diagnostico, DiagnosticoDTO>(diagUpdated);
        }
        
        // DELETE /diagnosticos/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            if (await service.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}