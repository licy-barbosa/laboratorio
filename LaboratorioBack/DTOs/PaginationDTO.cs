namespace LaboratorioBack.DTOs
{

    //video 108
    public class PaginationDTO
    {
        public int PageNumber { get; set; } = 1;
        private int recordsPage { get; set; } = 10;
        private readonly int MaximumQuantityPage = 100;

        public int RecordsPage {
            get { return recordsPage; }
            set { recordsPage = (value > MaximumQuantityPage) ? MaximumQuantityPage : value; }
        }
    }
}