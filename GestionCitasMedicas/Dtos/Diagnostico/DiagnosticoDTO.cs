namespace GestionCitasMedicas.Dtos.Diagnostico
{
    public class DiagnosticoDTO
    {
        public long id { get; set; }
        public string valoracionEspecialista { get; set; }
        public string enfermedad { get; set; }
        public CitaDTO cita { get; set; }
    }
}