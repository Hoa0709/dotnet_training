namespace app.Models
{
    public class UserRolesModel
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}