using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Locations")]
    public class Location {

        [Key]
        public int lid { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }
        
        public string summary { get; set; }

        [Column(TypeName="ntext")] 
        public string content { get; set; }

        public string pathimage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime postdate { get; set; }

        [StringLength(100)]
        public string author { get; set; }
        
        public float latitude { get; set; }
        public float longtitude { get; set; }

    }
}