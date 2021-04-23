using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionCitasMedicas.Repositories;
using GestionCitasMedicas.RepositoriesImpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Oracle.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using GestionCitasMedicas.Entities;
using GestionCitasMedicas.Services;
using GestionCitasMedicas.ServicesImpl;
using Microsoft.Extensions.Options;

namespace GestionCitasMedicas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddAutoMapper(typeof(DTOMapper));
            
            services.AddDbContext<OracleContext>(opt => opt
                    .UseLazyLoadingProxies()
                    .UseOracle(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDiagnosticoRepository, OracleDiagnosticoRepository>();
            services.AddScoped<IDiagnosticoService, DiagnosticoServiceImpl>();
            services.AddScoped<ICitaRepository, OracleCitaRepository>();
            services.AddScoped<ICitaService, CitaServiceImpl>();
            services.AddScoped<IMedicoRepository, OracleMedicoRepository>();
            services.AddScoped<IMedicoService, MedicoServiceImpl>();
            services.AddScoped<IPacienteRepository, OraclePacienteRepository>();
            services.AddScoped<IPacienteService, PacienteServiceImpl>();
            services.AddScoped<IUsuarioRepository, OracleUsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioServiceImpl>();

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestionCitasMedicas", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionCitasMedicas v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
