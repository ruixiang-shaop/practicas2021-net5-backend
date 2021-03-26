using GestionCitasMedicas.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionCitasMedicas.Repositories
{
    public class OracleContext : DbContext
    {
        public OracleContext(DbContextOptions<OracleContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> OracleUsuario { get; set; }
        public DbSet<Medico> OracleMedico { get; set; }
        public DbSet<Paciente> OraclePaciente { get; set; }
        public DbSet<Cita> OracleCita { get; set; }
        public DbSet<Diagnostico> OracleDiagnostico{ get; set; }
    }
}