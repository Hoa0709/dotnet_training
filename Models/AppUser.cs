using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace app.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string FullName { get; set; }
        [PersonalData]

        public DateTime Birthday { get; set; }
        
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string IdentityNumber { get; set; }
    }
}