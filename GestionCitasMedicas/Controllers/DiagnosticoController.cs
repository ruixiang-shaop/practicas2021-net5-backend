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
        private readonly IDiagnosticoService diagService;
        private readonly IMapper mapper;
        public DiagnosticoController(IDiagnosticoService diagService, IMapper mapper)
        {
            this.diagService = diagService;
            this.mapper = mapper;
        }

        // GET /diagnosticos
        [HttpGet]
        public async Task<IEnumerable<DiagnosticoDTO>> GetDiagnosticos()
        {
            ICollection<Diagnostico> diags = await diagService.findAllAsync();
            return mapper.Map<ICollection<Diagnostico>, ICollection<DiagnosticoDTO>>(diags);
        }

        // Get /diagnosticos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoDTO>> GetDiagnostico(long id)
        {
            Diagnostico diag = await diagService.findByIdAsync(id);
            if (diag is null)
            {
                return NotFound();
            }
            return mapper.Map<Diagnostico, DiagnosticoDTO>(diag);
        }

        // Post /diagnosticos/add
        [HttpPost("add")]
        public async Task<ActionResult<DiagnosticoDTO>> CreateDiagnostico(CreateDiagnosticoDTO diagDto)
        {
            Diagnostico diag = mapper.Map<CreateDiagnosticoDTO, Diagnostico>(diagDto);
            var newDiagId = await diagService.saveAsync(diag);
            if (newDiagId == null)
                return BadRequest();
            return await GetDiagnostico((long)newDiagId);
        }

        // PUT /diagnosticos/update
        [HttpPut("update")]
        public async Task<ActionResult<DiagnosticoDTO>> UpdateDiagnostico(DiagnosticoDTO diagDto)
        {
            Diagnostico diag = mapper.Map<DiagnosticoDTO, Diagnostico>(diagDto);
            var updated = await diagService.updateAsync(diag);
            if (!updated)
                return NotFound();
            return await GetDiagnostico(diag.id);
        }
        
        // DELETE /diagnosticos/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDiagnostico(long id)
        {
            if (await diagService.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}