namespace TwelfthMorning.Data
{
    public class BlogService
    {

        public BlogService()
        {
        }

        public List<Blog> GetBlogPreviewList(int page, int size)
        {
            return null;
            //return ApplicationDbContext.Context.Blog
            //    .AsQueryable()
            //    .OrderByDescending(x => x.Id)
            //    .Skip(page * size)
            //    .Take(size)
            //    .AsEnumerable()
            //    .Select(x =>
            //       new Blog { Id = x.Id, Title = x.Title, DateAdded = x.DateAdded, Post = GetPreviewPost(x.Post) }
            //    ).ToList();
        }

        private string GetPreviewPost(string post)
        {
            //post = BBCode.ParsePost(post);
            var temp = post.Split("<br />", StringSplitOptions.RemoveEmptyEntries);

            return temp[0] + "<br /><br />" + (temp.Count() > 1 ? temp[1] : "");
        }
    }
}
