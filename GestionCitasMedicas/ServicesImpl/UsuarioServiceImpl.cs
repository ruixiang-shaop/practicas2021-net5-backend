using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.Services;

namespace GestionCitasMedicas.ServicesImpl
{
    public class UsuarioServiceImpl : IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        public UsuarioServiceImpl(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Usuario> findByUsuarioAsync(string usuario)
        {
            try {
                return await repository.GetUsuarioByUsuarioAsync(usuario);
            } catch (Exception) {
                return null;
            }
        }
    }
}