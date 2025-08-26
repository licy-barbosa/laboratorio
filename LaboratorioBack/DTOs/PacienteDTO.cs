using System.ComponentModel.DataAnnotations;

namespace LaboratorioBack.DTOs
{
    public class PacienteDTO
    {
        public int? PacienteId { get; set; }
        public string Nombre { get; set; }
        public int Generoid { get; set; }
        public long NSS { get; set; }
        public string CURP { get; set; } 
        public short Edad { get; set; }
        public GeneroDTO Genero { get; set; }
    }
}