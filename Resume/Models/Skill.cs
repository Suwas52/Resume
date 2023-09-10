using System.ComponentModel.DataAnnotations;

namespace Resume.Models
{
    public class Skill
    {
        [Key]
        public int skill_id { get; set; }
        public string skill_title { get; set; }
        public string skills_name { get; set; }
        public int info_id { get; set; }
    }
}
