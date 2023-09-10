using System.ComponentModel.DataAnnotations;

namespace Resume.Models
{
    public class Education
    {
        [Key]
        public int education_id { get; set; }
        public string school_name { get; set; }
        public string date { get; set; }
        public string university_name { get; set; }
        public int info_id { get; set; }
    }
}
