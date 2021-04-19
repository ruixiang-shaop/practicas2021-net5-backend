using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Repositories;

namespace GestionCitasMedicas.RepositoriesImpl
{
    public class OracleCitaRepository : ICitaRepository
    {
        private readonly OracleContext _context;
        public OracleCitaRepository(OracleContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cita>> GetCitasAsync()
        {
            return await _context.OracleCita.ToListAsync();
        }
        public async Task<Cita> GetCitaAsync(long id)
        {
            return await _context.OracleCita.FindAsync(id);
        }
        public async Task<long> CreateCitaAsync(Cita cita)
        {
            var citaCreated = await _context.OracleCita.AddAsync(cita);
            await _context.SaveChangesAsync();
            return citaCreated.Entity.id;
        }
        public async Task UpdateCitaAsync(Cita cita)
        {
            var citaUpdated = _context.OracleCita.Update(cita);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCitaAsync(long id)
        {
            var cita = await _context.OracleCita.FindAsync(id);
            if (cita == null)
                await Task.CompletedTask;
            _context.OracleCita.Remove(cita);
            await _context.SaveChangesAsync();
        }
    }
}