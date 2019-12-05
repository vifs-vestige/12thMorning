using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _12thMorning.Data {
    public class BlogDataAccess {
        private _12thMorningContext db = new _12thMorningContext();

        public List<Blog> GetAll(int page, int size, string type, DateTime month) {
            return GetQueryBlogs(type, month).OrderByDescending(x => x.PostNumber).Skip(page * size).Take(size).Select(x => (Blog)x.Clone()).ToList();
        }

        public List<DateTime> GetMonths(string type) {
            return GetQueryBlogs(type, new DateTime()).Select(x => new DateTime(x.DateAdded.Year, x.DateAdded.Month, 1))
                .Distinct().AsEnumerable().OrderBy(x => x).ToList();
        }

        public Blog Get(int id) {
            try {
                return (Blog)db.Blog.First(x => x.PostNumber == id).Clone();
            } catch {
                return null;
            }
        }

        public void Update(Blog blog) {
            if (Startup.IsDev) {
                if (blog.Id == 0) {
                    blog.DateAdded = DateTime.Now;
                    blog.PostNumber = db.Blog.Max(x => x.PostNumber) + 1;
                }

                db.Blog.Update(blog);
                db.SaveChanges();
            }
        }

        public List<int?> GetNextAndPrevious(int id, string type) {
            var blogs = GetQueryBlogs(type, new DateTime()).OrderBy(x => x.PostNumber).ToList();
            var current = blogs.IndexOf(blogs.First(x => x.PostNumber == id));
            int? next = null;
            int? previous = null;
            if (current != 0)
                previous = blogs[current - 1].PostNumber;
            if (current+1 < blogs.Count())
                next = blogs[current + 1].PostNumber;
            return new List<int?>(){next, previous };
        }

        public int GetCount(string type, DateTime month) {
            return GetQueryBlogs(type, month).Count();
        }

        private IQueryable<Blog> GetQueryBlogs(string type, DateTime month) {
            try {
                IQueryable<Blog> blog;
                if (type == "dev")
                    blog = db.Blog.Where(x => x.MainTag == "Dev" || x.MainTag == "Mixed");
                else if (type == "personal")
                    blog = db.Blog.Where(x => x.MainTag == "Personal" || x.MainTag == "Mixed");
                else
                    blog = db.Blog.AsQueryable();
                if(month.Ticks != 0) {
                    var MonthEnd = month.AddMonths(1).AddTicks(-1);
                    blog = blog.Where(x => x.DateAdded >= month && x.DateAdded <= MonthEnd);
                }
                return blog;
            }
            catch {
                throw;
            }
        }
    }
}
