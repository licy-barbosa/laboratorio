using LaboratorioBack.Models;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioBack.Model
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        
        [Required]
        public string Nombre { get; set; } = null!;
        public int Generoid { get; set; }

        //[NSS]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(11, ErrorMessage = "El campo {0} debe de tener {1} tantos caracteres")]
        public long NSS { get; set; }

        [StringLength(18, MinimumLength = 18, ErrorMessage = "La Curp debe tener exactamente 18 caracteres.")]
        [Required]
        public string CURP { get; set; }

        [Required]
        public short Edad { get; set; }

        public bool Status { get; set; } = true;

        public Genero Genero { get; set; }

        public ICollection<Estudio> Estudios { get; set; }
    }
}