using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    [Table("Programs")]
    public class ProgramInfo{

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int Type_inoff { get; set; }
        public int Type_program { get; set; }

        [Column(TypeName = "ntext")]
        public string Pathimage_list { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Create_at { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Held_on { get; set; }

        public int ArtistId { set; get; }
    }
}