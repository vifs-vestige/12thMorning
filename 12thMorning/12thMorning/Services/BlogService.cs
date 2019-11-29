using _12thMorning.Data;
using _12thMorning.Libraries;
using bbsharp;
using bbsharp.Easy;
using bbsharp.Renderers.Html;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace _12thMorning.Services {
    public class BlogService {
        private BlogDataAccess BlogData = new BlogDataAccess();

        #region get blog(s)
        public Task<List<Blog>> GetBlogPreviewList(int page, int size, string type, DateTime month) {
            var blogs = BlogData.GetAll(page, size, type, month);
            blogs.ForEach(x => x.Post = GetPreviewPost(x.Post));
            return Task.FromResult(blogs);
        }

        public MarkupString GetBlogPost(string post) {
            return (MarkupString)BBCode.ParsePost(post);
        }

        public Blog GetBlogFull(int id) {
            var blog = BlogData.Get(id);
            blog.Post = BBCode.ParsePost(blog.Post);
            return blog;
        }

        public Blog GetBlogRaw(int id) {
            return BlogData.Get(id);
        }
        #endregion

        #region paging
        public int GetPages(int size, string type, DateTime month) {
            return (int)Math.Ceiling((double)BlogData.GetCount(type, month) / size);
        }

        public List<DateTime> GetBlogMonths(string type = "") {
            return BlogData.GetMonths(type);
        }

        public List<int?> GetBlogNextAndPrevious(int id, string type) {
            return BlogData.GetNextAndPrevious(id, type);
        }

        #endregion

        public void UpdateBlog(Blog blog) {
            BlogData.Update(blog);
        }

        private string GetPreviewPost(string post) {
            post = BBCode.ParsePost(post);
            var temp = post.Split("<br />", StringSplitOptions.RemoveEmptyEntries);

            return temp[0] + "<br /><br />" + (temp.Count() > 1 ? temp[1] : "");
        }




    }
}
