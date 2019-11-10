using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class _12thMorningContext : DbContext {
        public DbSet<Test> Test { get; set; }

        public _12thMorningContext(DbContextOptions<_12thMorningContext> a) : base(a) {
        }

        public _12thMorningContext() {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(
                @"Server=localhost;Database=12thmorning;Uid=12thmorning;");
        }

    }

    public class Test {
        public int Id { get; set; }
        public int Number{ get; set; }
    }
}
