using System.Text.Json;

namespace app.Models{
    public class ErrorDetails
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}