using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class BlogDataAccess {
        private _12thMorningContext db = new _12thMorningContext();

        public List<Blog> GetAll(int page, int size) {
            try {
                return db.Blog.AsQueryable().Skip(page*size).Take(size).ToList();
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

        public int GetCount() {
            return db.Blog.Count();
        }

    }
}
