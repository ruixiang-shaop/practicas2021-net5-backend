using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Services
{
    public interface IUsuarioService
    {
        public Task<Usuario> findByUsuarioAsync(String usuario);
    }
}