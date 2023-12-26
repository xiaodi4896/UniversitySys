using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dxxt.Models;

namespace dxxt.Data
{
    public class dxxtContext : DbContext
    {
        public string? connectionString;
        public dxxtContext (DbContextOptions<dxxtContext> options)
            : base(options)
        {
            connectionString = Database.GetConnectionString();
        }

        public DbSet<dxxt.Models.Lecturer> Lecturer { get; set; } = default!;

        public DbSet<dxxt.Models.Score> Score { get; set; } = default!;

        public DbSet<dxxt.Models.Student> Student { get; set; } = default!;

        public DbSet<dxxt.Models.Subject> Subject { get; set; } = default!;

        public DbSet<ScoreStuSub> ScoreStuSub { get; set; } = default!;

        public DbSet<SubjectLec> SubjectLec { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreStuSub>().HasNoKey();
            modelBuilder.Entity<SubjectLec>().HasNoKey();
        }
    }
}
