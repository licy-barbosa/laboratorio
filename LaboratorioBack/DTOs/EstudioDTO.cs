namespace LaboratorioBack.DTOs
{
    public class EstudioDTO
    {
        public PacienteDTO Paciente { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime Date { get; set; }
        public string NameEstudio { get; set; }
        public int Resultado { get; set; }
    }  

    public class FilterEstudioDTO
    {
        public string NameEstudio { get; set; }
        public string Desde { get; set; }
        public string Hast { get; set; }
    }
}