namespace GestionCitasMedicas.Entities
{
    public class Diagnostico
    {
        public long id { get; set; }
        public string valoracionEspecialista { get; set; }
        public string enfermedad { get; set; }
        
        public Cita cita { get; set; }
    }
}