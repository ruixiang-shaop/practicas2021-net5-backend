using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Repositories;

namespace GestionCitasMedicas.RepositoriesImpl
{
    public class OracleDiagnosticoRepository : IDiagnosticoRepository
    {
        private readonly OracleContext _context;
        public OracleDiagnosticoRepository(OracleContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Diagnostico>> GetDiagnosticosAsync()
        {
            return await _context.OracleDiagnostico.ToListAsync();
        }
        public async Task<Diagnostico> GetDiagnosticoAsync(long id)
        {
            return await _context.OracleDiagnostico.FindAsync(id);
        }
        public async Task<long> CreateDiagnosticoAsync(Diagnostico diag)
        {
            var diagCreated = await _context.OracleDiagnostico.AddAsync(diag);
            await _context.SaveChangesAsync();
            return diagCreated.Entity.id;
        }
        public async Task UpdateDiagnosticoAsync(Diagnostico diag)
        {
            var diagUpdated = _context.OracleDiagnostico.Update(diag);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDiagnosticoAsync(long id)
        {
            var diag = await _context.OracleDiagnostico.FindAsync(id);
            if (diag == null)
                await Task.CompletedTask;
            _context.OracleDiagnostico.Remove(diag);
            await _context.SaveChangesAsync();
        }



    }
}