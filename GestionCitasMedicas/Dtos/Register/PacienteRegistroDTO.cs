namespace GestionCitasMedicas.Dtos.Register
{
    public class PacienteRegistroDTO
    {
        public string usuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string nss { get; set; }
        public string numTarjeta { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }
}