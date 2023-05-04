namespace app.Models
{
    public class ProgramDto{
        public string name { get; set; }
        public string content { get; set; }
        public int type_inoff { get; set; }
        public int type_program { get; set; }
        public decimal price { get; set; }
        public string pathimage_list { get; set; }
        public DateTime held_on { get; set; }
        public int l_id { set; get; }
        public int g_id { set; get; }
        public int u_id { set; get; }

    }

}