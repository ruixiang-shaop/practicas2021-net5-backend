using System.Collections.Generic;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByUsuarioAsync(string usuario);
    }
}
