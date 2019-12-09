using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class _12thMorningContext : DbContext {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(
                @"Server=localhost;Database=12thmorning;Uid=12thmorning;");
        }

    }

    public class Blog {
        public int Id { get; set; }
        [Required]
        public string Post{ get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string MainTag { get; set; }

        [InverseProperty("Blog")]
        public List<Comment> Comments { get; set; }
    }

    public class Comment {
        public Comment() {
            Replies = new List<Comment>();
        }

        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
        public int? ReplyTo { get; set; }
        public DateTime DateAdded { get; set; }

        [InverseProperty("ParentComment")]
        public List<Comment> Replies { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        [ForeignKey("ReplyTo")]
        public Comment ParentComment { get; set; }

    }
}
