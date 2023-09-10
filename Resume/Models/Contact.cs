using System.ComponentModel.DataAnnotations;

namespace Resume.Models
{
    public class Contact
    {
        [Key]
        public int contact_id { get; set; }
        public string contact_title { get; set; }
        public string contact_name { get; set; }
        public string contact_link { get; set; }
        public int info_id { get; set; }
    }
}
