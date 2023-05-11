namespace app.Models 
{
    public class TicketDto {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProgramId { get; set; }
    }
}