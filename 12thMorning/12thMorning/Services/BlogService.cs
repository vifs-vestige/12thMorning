using System;
using System.Collections.Generic;
using System.Linq;
using _12thMorning.Data;
using _12thMorning.Libraries;
using Microsoft.AspNetCore.Components;


namespace _12thMorning.Services {
    public class BlogService {
        private _12thMorningContext DB {
            get { return new _12thMorningContext(); }
        }

//comment color too dark :(
        #region get blog(s)
        public List<Blog> GetBlogPreviewList(int page, int size, string type, DateTime month) {
            return GetQueryBlogs(type, month)
                .OrderByDescending(x => x.Id)
                .Skip(page * size)
                .Take(size)
                .AsEnumerable()
                .Select(x => 
                    new Blog { Id = x.Id, Title = x.Title, DateAdded = x.DateAdded, Post = GetPreviewPost(x.Post)}
                )
                .ToList();
        }

        public MarkupString GetFullPreview(string post) {
            return (MarkupString)BBCode.ParsePost(post);
        }

        public Blog GetBlogFull(int id) {
            var blog = DB.Blog.First(x => x.Id == id);
            blog.Post = BBCode.ParsePost(blog.Post);
            return blog;
        }

        public Blog GetBlogRaw(int id) {
            return DB.Blog.First(x => x.Id == id);
        }
        #endregion

        #region paging
        public int GetPages(int size, string type, DateTime month) {
            return (int)Math.Ceiling((double)GetQueryBlogs(type, month).Count() / size);
        }

        public List<DateTime> GetBlogMonths(string type) {
            return GetQueryBlogs(type)
                .Select(x => new DateTime(x.DateAdded.Year, x.DateAdded.Month, 1))
                .Distinct()
                .AsEnumerable()
                .OrderBy(x => x)
                .ToList();
        }

        public List<int?> GetBlogNextAndPrevious(int id, string type) {
            var blogs = GetQueryBlogs(type)
                .OrderBy(x => x.Id)
                .ToList();

            var current = blogs.IndexOf(blogs.First(x => x.Id == id));
            int? next = null, previous = null;

            if (current != 0)
                previous = blogs[current - 1].Id;
            if (current + 1 < blogs.Count())
                next = blogs[current + 1].Id;

            return new List<int?>(){ next, previous };
        }

        #endregion

        public void UpdateBlog(Blog blog) {
            if (Startup.IsDev) {
                var db = DB;
                if (blog.Id == 0) {
                    blog.DateAdded = DateTime.Now;
                    blog.Id = DB.Blog.Max(x => x.Id) + 1;
                    db.Blog.Add(blog);
                } else
                    db.Blog.Update(blog);
                db.SaveChanges();
            }
        }

        private string GetPreviewPost(string post) {
            post = BBCode.ParsePost(post);
            var temp = post.Split("<br />", StringSplitOptions.RemoveEmptyEntries);

            return temp[0] + "<br /><br />" + (temp.Count() > 1 ? temp[1] : "");
        }


        private IQueryable<Blog> GetQueryBlogs(string type, DateTime? month = null) {
            IQueryable<Blog> blog;
            if (type == "dev")
                blog = DB.Blog.Where(x => x.MainTag == "Dev" || x.MainTag == "Mixed");
            else if (type == "personal")
                blog = DB.Blog.Where(x => x.MainTag == "Personal" || x.MainTag == "Mixed");
            else
                blog = DB.Blog.AsQueryable();
            if (month != null && ((DateTime)month).Ticks != 0 ) {
                var MonthEnd = ((DateTime)month).AddMonths(1).AddTicks(-1);
                blog = blog.Where(x => x.DateAdded >= month && x.DateAdded <= MonthEnd);
            }
            return blog;
        }
        
    }
}
