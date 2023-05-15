namespace app.Models
{
    public class QRCodeInfo
    {
        public string Code { get; set; }
        public string  EventName { get; set; }
        public DateTime HeldOn { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public int NumberOfTickets { get; set; }
        public bool CheckIn { get; set; }
    }
}