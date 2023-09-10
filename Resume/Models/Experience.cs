﻿using System.ComponentModel.DataAnnotations;

namespace Resume.Models
{
    public class Experience
    {
        [Key]
        public int experience_id { get; set; }
        public string name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string website_link { get; set; }
        public string frontend_skill { get; set; }
        public string backend_skill { get; set; }
        public int info_id { get; set; }
    }
}
