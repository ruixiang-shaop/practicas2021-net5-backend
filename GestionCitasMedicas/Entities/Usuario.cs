namespace GestionCitasMedicas.Entities
{
    public abstract class Usuario
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
    }
}