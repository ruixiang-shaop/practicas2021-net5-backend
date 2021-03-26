using System;
using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IUsuarioService
    {
        public Usuario findByUsuario(String usuario);
    }
}