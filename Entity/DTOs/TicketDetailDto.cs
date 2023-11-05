namespace app.Models 
{
    public class TicketDetailDto {
        public int Id {set; get;}
        public int ProgramId { get; set; }
        public string Name { get;set;}
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Inventory { get; set; }
        public DateTime Held_on { get; set; }
        public string Code { get;set;}
    }
}