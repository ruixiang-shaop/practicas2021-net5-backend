using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionCitasMedicas.Entities
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
        public DbSet<Diagnostico> OracleDiagnostico { get; set; }
        public DbSet<MedicoPaciente> OracleMedicoPaciente {get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // JoinedTable-inheritance for Usuario
            modelBuilder.Entity<Usuario>().ToTable("USUARIO");
            modelBuilder.Entity<Paciente>().ToTable("PACIENTE");
            modelBuilder.Entity<Medico>().ToTable("MEDICO");

            // FK cita->diagnostico
            modelBuilder.Entity<Cita>()
                .HasOne<Diagnostico>(c => c.diagnostico)
                .WithOne(d => d.cita)
                .HasForeignKey<Diagnostico>(d => d.citaId)
                .OnDelete(DeleteBehavior.Cascade);
            // FK cita -> medico
            modelBuilder.Entity<Cita>()
                .HasOne<Medico>(c => c.medico)
                .WithMany(m => m.citas)
                .HasForeignKey(c => c.medicoId)
                .OnDelete(DeleteBehavior.NoAction);
            // FK cita -> paciente
            modelBuilder.Entity<Cita>()
                .HasOne<Paciente>(c => c.paciente)
                .WithMany(p => p.citas)
                .HasForeignKey(c => c.pacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicoPaciente>().HasKey(
                mp => new { mp.medicoId, mp.pacienteId }
            );
            modelBuilder.Entity<MedicoPaciente>()
                .HasOne<Medico>(mp => mp.medico)
                .WithMany(m => m.pacientes)
                .HasForeignKey(mp => mp.medicoId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MedicoPaciente>()
                .HasOne<Paciente>(mp => mp.paciente)
                .WithMany(p => p.medicos)
                .HasForeignKey(mp => mp.pacienteId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
        new Microsoft.Extensions.Logging.LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}