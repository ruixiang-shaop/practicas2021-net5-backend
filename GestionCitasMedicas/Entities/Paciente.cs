using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCitasMedicas.Entities
{
    [Table("PACIENTE")]
    public class Paciente : Usuario
    {
        [Required]
        [Column("NSS")]
        public string nss { get; set; }
        [Required]
        [Column("NUM_TARJETA")]
        public string numTarjeta { get; set; }
        [Required]
        [Column("TELEFONO")]
        public string telefono { get; set; }
        [Required]
        [Column("DIRECCION")]
        public string direccion { get; set; }
        public virtual ICollection<MedicoPaciente> medicos { get; set; }
        public virtual ICollection<Cita> citas { get; set; }
        
        public void addCita(Cita c)
        {
            citas.Add(c);
        }
        public bool removeCita(Cita c)
        {
            return citas.Remove(c);
        }
        public void addMedico(Medico m)
        {
            medicos.Add(new MedicoPaciente{paciente = this, medico = m});
        }
        public bool removeMedico(Medico m)
        {
            return medicos.Remove(new MedicoPaciente{paciente = this, medico = m});
        }
    }
}