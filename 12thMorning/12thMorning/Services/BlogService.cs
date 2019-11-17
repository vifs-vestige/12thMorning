using bbsharp;
using bbsharp.Easy;
using bbsharp.Renderers.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace _12thMorning.Data {
    public class BlogService {
        private BlogDataAccess BlogData = new BlogDataAccess();

        public Task<List<Blog>> GetBlogList(int page, int size) {
            return Task.FromResult(BlogData.GetAll(page, size));
        }

        public Blog GetBlog(int id) {
            return BlogData.Get(id);
        }

        public int GetPages(int size) {
            return (int)Math.Ceiling((double)BlogData.GetCount() / size);
        }

        public void UpdateBlog(Blog blog) {
            BlogData.Update(blog);
        }

        public string ParsePost(string post) {
            var doc = BBCodeDocument.Load(post, false);

            var LookupTable = HtmlRenderer.DefaultLookupTable.ToList();
            LookupTable.Remove(LookupTable.First(x => x.Key == "code"));
            LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("code", RenderCode));

            return doc.Children.ToHtml(false, LookupTable.ToArray());
        }

        public string GetPreviewPost(string post) {
            post = ParsePost(post);
            var temp = post.Split("<br /><br />");
            return temp[0] + "<br /><br />" + temp[1];
        }

        public static string RenderCode(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<pre class=\"prettyprint linenums:1\">" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "</pre>";
        }


    }
}
