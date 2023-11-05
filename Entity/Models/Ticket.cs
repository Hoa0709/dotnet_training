using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Tickets")]
    public class Ticket {

        public int Id {set; get;}
        public int ProgramId {set; get;}
        
        [Column(TypeName="Money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Inventory { get; set; }
        public string Code { get;set;}
    }
}