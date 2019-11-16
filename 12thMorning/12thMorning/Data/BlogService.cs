using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class BlogService {
        private BlogDataAccess BlogData = new BlogDataAccess();

        public Task<List<Blog>> GetBlogList(int page, int size) {
            return Task.FromResult(BlogData.GetAll(page, size));
        }
    }
}
