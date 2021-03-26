using System;
using System.Collections.Generic;
using System.Linq;
using GestionCitasMedicas.Dtos;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    [Route("diagnosticos")]
    public class DiagnosticoController : ControllerBase
    {
        private readonly IDiagnosticoRepository repository;

        public DiagnosticoController(IDiagnosticoRepository repository)
        {
            this.repository = repository;
        }

        // GET /diagnosticos
        [HttpGet]
        public IEnumerable<Diagnostico> GetDiagnosticos()
        {
            //TODO mapping
            var diags = repository.GetDiagnosticos().Select(item => item);
            return diags;
        }

        // Get /diagnosticos/{id}
        [HttpGet("{id}")]
        public ActionResult<Diagnostico> GetDiagnostico(long id)
        {
            var diag = repository.GetDiagnostico(id);
            if (diag is null)
            {
                return NotFound();
            }
            return diag;
        }

        // Post /diagnosticos/add
        [HttpPost("add")]
        public ActionResult<Diagnostico> CreateDiagnostico(Diagnostico diag)
        {
            //TODO DTO
            Diagnostico diagNew = new();

            repository.CreateDiagnostico(diag);
            
            return CreatedAtAction(nameof(GetDiagnostico), new { id = diag.id}, diag);
        }

        // PUT /diagnosticos/update
        [HttpPut("update")]
        public ActionResult UpdateDiagnostico(Diagnostico diag)
        {
            return NoContent();
        }
        
        // DELETE /diagnosticos/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            return NoContent();
        }
    }
}