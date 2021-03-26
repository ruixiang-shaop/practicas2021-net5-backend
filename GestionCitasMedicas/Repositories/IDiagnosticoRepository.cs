using System;
using System.Collections.Generic;
using GestionCitasMedicas.Entities;

namespace GestionCitasMedicas.Repositories
{
    public interface IDiagnosticoRepository
    {
        Diagnostico GetDiagnostico(long id);
        IEnumerable<Diagnostico> GetDiagnosticos();
        void CreateDiagnostico(Diagnostico diag);
        void UpdateDiagnostico(Diagnostico diag);
        void DeleteDiagnostico(long id);
    }
}
