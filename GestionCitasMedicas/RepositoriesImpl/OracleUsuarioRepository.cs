using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Repositories;

namespace GestionCitasMedicas.RepositoriesImpl
{
    public class OracleUsuarioRepository : IUsuarioRepository
    {
        private readonly OracleContext _context;
        public OracleUsuarioRepository(OracleContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetUsuarioByUsuarioAsync(string usuario)
        {
            return await _context.OracleUsuario.Where(u => u.usuario.Equals(usuario)).FirstOrDefaultAsync();
        }
    }
}