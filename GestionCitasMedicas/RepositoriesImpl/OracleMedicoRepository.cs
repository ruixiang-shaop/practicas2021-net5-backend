using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Entities;
using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Repositories;

namespace GestionCitasMedicas.RepositoriesImpl
{
    public class OracleMedicoRepository : IMedicoRepository
    {
        private readonly OracleContext _context;
        public OracleMedicoRepository(OracleContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Medico>> GetMedicosAsync()
        {
            return await _context.OracleMedico.ToListAsync();
        }
        public async Task<Medico> GetMedicoAsync(long id)
        {
            return await _context.OracleMedico.FindAsync(id);
        }
        public async Task<long> CreateMedicoAsync(Medico med)
        {
            var medCreated = await _context.OracleMedico.AddAsync(med);
            await _context.SaveChangesAsync();
            return medCreated.Entity.id;
        }
        public async Task UpdateMedicoAsync(Medico med)
        {
            var medUpdated = _context.OracleMedico.Update(med);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMedicoAsync(long id)
        {
            var med = await _context.OracleMedico.FindAsync(id);
            if (med == null)
                await Task.CompletedTask;
            _context.OracleMedico.Remove(med);
            await _context.SaveChangesAsync();
        }

        public async Task<Medico> GetMedicoRandomAsync()
        {
            return await _context.OracleMedico.OrderBy(m => Guid.NewGuid()).FirstOrDefaultAsync();
        }
    }
}