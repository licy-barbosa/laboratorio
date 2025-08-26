using LaboratorioBack.Model;

namespace LaboratorioBack.Models
{
    public class Estudio
    {
        public int EstudioId { get; set; }
        public DateTime Date { get; set; }
        public string NameEstudio { get; set; }
        public int Resultado { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
    }
}
