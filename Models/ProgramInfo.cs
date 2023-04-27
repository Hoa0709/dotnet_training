using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    [Table("Programs")]
    public class ProgramInfo{

        [Key]
        public int pid { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public int type_inoff { get; set; }
        public int type_program { get; set; }
        public string md5 { get; set; }

        [Column(TypeName = "ntext")]
        public string pathimage_list { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime create_at { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime held_on { get; set; }

        public int l_id { set; get; }
        public int g_id { set; get; }
        public int u_id { set; get; }
    }
}