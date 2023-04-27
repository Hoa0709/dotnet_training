using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("News")]
    public class News {

        [Key]
        public int nid { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName="ntext")] 
        public string content { get; set; }

        public string pathimage { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime postdate { get; set; }

        [StringLength(100)]
        public string author { get; set; }

    }
}