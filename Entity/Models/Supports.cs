using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Supports")]
    public class Support {

        [Key]
        public int sid { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }
        
        [Column(TypeName="ntext")] 
        public string content { get; set; }
    }
}