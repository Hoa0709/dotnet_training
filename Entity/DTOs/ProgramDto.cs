namespace app.Models
{
    public class ProgramDto{
        public string Name { get; set; }
        public string Content { get; set; }
        public int Type_program { get; set; }
        public int Type_inoff { get; set; }
        public string Pathimage_list { get; set; }
        public DateTime Held_on { get; set; }
        public int ArtistId { set; get; }
    }

}