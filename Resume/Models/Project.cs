using System.ComponentModel.DataAnnotations;

namespace Resume.Models
{
    public class Project
    {
        [Key]
        public int project_id { get; set; }
        public string project_title { get; set; }
        public string project_link { get; set; }
        public string stack_name { get; set; }
        public string description { get; set; }
        public int info_id { get; set; }
    }
}
