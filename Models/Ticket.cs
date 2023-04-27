using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Tickets")]
    public class Ticket {

        [Key]
        public int tid { get; set; }

        [Column(TypeName="Money")]
        public decimal price { get; set; }
        
        public int quantity { get; set; }
        public int inventory { get; set; }
    }
}