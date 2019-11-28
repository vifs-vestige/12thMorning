using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _12thMorning.Data {
    public class BlogDataAccess {
        private _12thMorningContext db = new _12thMorningContext();

        public List<Blog> GetAll(int page, int size, string type = "") {
            return GetQueryBlogs(type).OrderByDescending(x => x.PostNumber).Skip(page * size).Take(size).Select(x => (Blog)x.Clone()).ToList();
        }

        internal List<DateTime> GetMonths(string type) {
            return GetQueryBlogs(type).Select(x => new DateTime(x.DateAdded.Year, x.DateAdded.Month, 1))
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

        public int GetCount(string type = "") {
            IQueryable<Blog> blog;
            if (type == "dev")
                blog = db.Blog.Where(x => x.MainTag == "Dev");
            else if (type == "personal")
                blog = db.Blog.Where(x => x.MainTag == "Personal");
            else
                blog = db.Blog.AsQueryable<Blog>();
            return blog.Count();
        }

        private IQueryable<Blog> GetQueryBlogs(string type) {
            try {
                IQueryable<Blog> blog;
                if (type == "dev")
                    blog = db.Blog.Where(x => x.MainTag == "Dev" || x.MainTag == "Mixed");
                else if (type == "personal")
                    blog = db.Blog.Where(x => x.MainTag == "Personal" || x.MainTag == "Mixed");
                else
                    blog = db.Blog.AsQueryable();
                return blog;
            }
            catch {
                throw;
            }
        }

    }
}
