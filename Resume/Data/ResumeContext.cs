using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resume.Models;

namespace Resume.Data
{
    public class ResumeContext : DbContext
    {
        public ResumeContext (DbContextOptions<ResumeContext> options)
            : base(options)
        {
        }

        public DbSet<Resume.Models.Information> Information { get; set; } = default!;

        public DbSet<Resume.Models.Education>? Education { get; set; }

        public DbSet<Resume.Models.Experience>? Experience { get; set; }

        public DbSet<Resume.Models.Project>? Project { get; set; }

        public DbSet<Resume.Models.Skill>? Skill { get; set; }

        public DbSet<Resume.Models.Contact>? Contact { get; set; }
    }
}
