using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class BlogDataAccess {
        private _12thMorningContext db = new _12thMorningContext();

        public List<Blog> GetAll(int page, int size) {
            try {
                return db.Blog.AsQueryable().Skip(page).Take(size).ToList();
            } catch {
                throw;
            }
        }

    }
}
