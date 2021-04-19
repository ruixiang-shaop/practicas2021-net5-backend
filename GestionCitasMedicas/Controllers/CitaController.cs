using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos.Cita;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("citas")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService service;
        private readonly IMapper mapper;
        public CitaController(ICitaService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET /citas
        [HttpGet]
        public async Task<IEnumerable<CitaDTO>> GetCitas()
        {
            ICollection<Cita> citas = await service.findAllAsync();
            return mapper.Map<ICollection<Cita>, ICollection<CitaDTO>>(citas);
        }

        // Get /citas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDTO>> GetCita(long id)
        {
            Cita cita = await service.findByIdAsync(id);
            if (cita is null)
            {
                return NotFound();
            }
            return mapper.Map<Cita, CitaDTO>(cita);
        }

        // Post /citas/add
        [HttpPost("add")]
        public async Task<ActionResult<CitaDTO>> CreateCita(CitaDTO citaDto)
        {
            Cita cita = mapper.Map<CitaDTO, Cita>(citaDto);
            Cita citaNew = await service.saveAsync(cita);
            if (citaNew != null)
                return mapper.Map<Cita, CitaDTO>(citaNew);
            return BadRequest();
        }

        // PUT /citas/update
        [HttpPut("update")]
        public async Task<ActionResult<CitaDTO>> UpdateCita(CitaDTO citaDto)
        {
            Cita cita = mapper.Map<CitaDTO, Cita>(citaDto);
            Cita citaUpdated = await service.updateAsync(cita);
            if (citaUpdated == null)
                return NotFound();
            return mapper.Map<Cita, CitaDTO>(citaUpdated);
        }
        
        // DELETE /citas/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            if (await service.deleteAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}