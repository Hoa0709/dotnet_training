using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace app.Models 
{  
    [Table("BookTickets")]
    public class BookTicket  
    {  
        [Key]
        public int Id { get; set; } 
        public string FullName { get; set; } 
        public string IdentityNumber { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime CreateAt { get; set; } 
        public string Code { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public bool TakeTicket { get; set; }
    }  
}  