namespace app.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentityNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}