using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class BlogDataAccess {
        private _12thMorningContext db = new _12thMorningContext();

        public List<Blog> GetAll(int page, int size, string type = "") {
            try {
                IQueryable<Blog> blog;
                if (type == "dev")
                    blog = db.Blog.Where(x => x.MainTag == "Dev" || x.MainTag == "Mixed");
                else if (type == "personal")
                    blog = db.Blog.Where(x => x.MainTag == "Personal" || x.MainTag == "Mixed");
                else
                    blog = db.Blog.AsQueryable<Blog>();
                return blog.OrderByDescending(x => x.PostNumber).Skip(page*size).Take(size).ToList();
            } catch {
                throw;
            }
        }

        public Blog Get(int id) {
            try {
                return db.Blog.First(x => x.PostNumber == id);
            } catch {
                return null;
            }
        }

        public void Update(Blog blog) {
            if(Startup.IsDev) {
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

    }
}
