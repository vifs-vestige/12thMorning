using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class _12thMorningContext : IdentityDbContext {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<QueslarKeys> QueslarKeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var temp = ServerVersion.AutoDetect("Server=localhost;Database=12thmorning;Uid=12thmorning;");

            optionsBuilder.UseMySql(@"Server=localhost;Database=12thmorning;Uid=12thmorning;", temp);
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Can't be empty")]
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

    public class QueslarKeys {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string ApiKey { get; set; }
        [Required]
        public DateTime DateUpdated { get; set; }
        [Required]
        public string Data { get; set; }

    }
}
