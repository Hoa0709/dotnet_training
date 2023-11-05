using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models 
{
    [Table("Locations")]
    public class Location {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        public string Summary { get; set; }

        [Column(TypeName="ntext")] 
        public string Content { get; set; }

        public string Pathimage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatAt { get; set; }
        
        public float Latitude { get; set; }
        public float Longtitude { get; set; }

    }
}