using LaboratorioBack.Model;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioBack.Models
{
    public class Genero
    {
        public int GeneroId { get; set; }
        [Required(ErrorMessage = "El Genero {0} es requerido")]
        public string NameGenero { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }
    }
}
