using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Unions")]
    public class Union {

        [Key]
        public int uid { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
        
        [Column(TypeName="ntext")] 
        public string decription { get; set; }
    }
}