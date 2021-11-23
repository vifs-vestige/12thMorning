using Microsoft.EntityFrameworkCore;

namespace TwelfthMorning.Data
{
    public class BlogService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public BlogService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<Blog> GetBlogPreviewList(int page, int size)
        {
            return _contextFactory.CreateDbContext().Blog
                .AsQueryable()
                .OrderByDescending(x => x.Id)
                .Skip(page * size)
                .Take(size)
                .AsEnumerable()
                .Select(x =>
                   new Blog { Id = x.Id, Title = x.Title, DateAdded = x.DateAdded, Post = GetPreviewPost(x.Post) }
                ).ToList();
        }

        private string GetPreviewPost(string post)
        {
            //post = BBCode.ParsePost(post);
            var temp = post.Split("<br />", StringSplitOptions.RemoveEmptyEntries);

            return temp[0] + "<br /><br />" + (temp.Count() > 1 ? temp[1] : "");
        }
    }
}
