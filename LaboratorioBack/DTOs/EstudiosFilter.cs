namespace LaboratorioBack.DTOs
{
    public class EstudiosFilter
    {
        public int PageNumber { get; set; } = 1;
        private int RecordsPage { get; set; } = 10;

        internal PaginationDTO Pagination { 
            
            get {

                return new PaginationDTO { PageNumber = PageNumber, RecordsPage = RecordsPage };    
            }
        }

        public string? NameEstudio { get; set; }
        public DateTime? From { get; set; } = null;
        public DateTime? To { get; set; } = null;

    }
}
