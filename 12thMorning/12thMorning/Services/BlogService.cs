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

        public Task<List<Blog>> GetBlogPreviewList(int page, int size, string type = "") {
            var blogs = BlogData.GetAll(page, size, type);
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

        public int GetPages(int size, string type = "") {
            return (int)Math.Ceiling((double)BlogData.GetCount(type) / size);
        }

        //public List<DateTime> GetBlogDates(string type = "") {
        //    return BlogData.GetDates(type);
        //}

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
