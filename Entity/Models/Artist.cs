using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Artists")]
    public class Artist {

        [Key]
        public int aid { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
        
        [Column(TypeName="ntext")] 
        public string decription { get; set; }
        [Column(TypeName="ntext")] 
        public string pathimage { get; set; }
    }
}