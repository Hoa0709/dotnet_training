using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Artists")]
    public class Artist {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Column(TypeName="ntext")] 
        public string Decription { get; set; }

        [Column(TypeName="ntext")] 
        public string Pathimage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatAt { get; set; }
    }
}