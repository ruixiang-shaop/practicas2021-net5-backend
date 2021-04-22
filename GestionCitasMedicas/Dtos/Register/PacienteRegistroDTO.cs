using System.ComponentModel.DataAnnotations;

namespace GestionCitasMedicas.Dtos.Register
{
    public class PacienteRegistroDTO
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string clave { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellidos { get; set; }
        [Required]
        public string nss { get; set; }
        [Required]
        public string numTarjeta { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string direccion { get; set; }
    }
}