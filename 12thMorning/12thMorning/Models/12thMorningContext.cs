using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class _12thMorningContext : DbContext {
        public DbSet<Blog> Blog { get; set; }

        public _12thMorningContext(DbContextOptions<_12thMorningContext> a) : base(a) {
        }

        public _12thMorningContext() {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(
                @"Server=localhost;Database=12thmorning;Uid=12thmorning;");
        }

    }

    public class Blog {
        public int Id { get; set; }
        public int PostNumber { get; set; }
        [Required]
        public string Post{ get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string MainTag { get; set; }
    }
}
