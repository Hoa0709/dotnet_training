using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Groups")]
    public class Group {

        [Key]
        public int gid { get; set; }
        
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        
        [Column(TypeName="ntext")] 
        public string decription { get; set; }
    }
}