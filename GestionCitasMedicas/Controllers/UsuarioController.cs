using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionCitasMedicas.Dtos;
using GestionCitasMedicas.Dtos.Medico;
using GestionCitasMedicas.Dtos.Paciente;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCitasMedicas.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService service;
        private readonly IMapper mapper;
        public UsuarioController(IUsuarioService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost("usuarios/usuarioExists")]
        public async Task<string> CheckIfUsuarioExists(string usuario)
        {
            Usuario user = await service.findByUsuarioAsync(usuario);
            if (user == null)
                return "false";
            return "true";
        }

        [HttpPost("auth")]
        public async Task<dynamic> Login(LoginDTO login)
        {
            Usuario user = await service.findByUsuarioAsync(login.usuario);
            if (user == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized, "La combinación de usuario y contraseña es incorrecta");
            }
            if (!user.clave.Equals(login.clave)) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized, "La combinación de usuario y contraseña es incorrecta");
            }
            if (user is Medico) {
                return Ok(mapper.Map<Medico, MedicoDTO>((Medico) user));
            } else {
                return Ok(mapper.Map<Paciente, PacienteDTO>((Paciente) user));
            }
        }
    }
}