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

        public Task<List<Blog>> GetBlogList(int page, int size, string type = "") {
            return Task.FromResult(BlogData.GetAll(page, size, type));
        }

        public Blog GetBlog(int id) {
            return BlogData.Get(id);
        }

        public int GetPages(int size, string type = "") {
            return (int)Math.Ceiling((double)BlogData.GetCount(type) / size);
        }

        public void UpdateBlog(Blog blog) {
            BlogData.Update(blog);
        }

        public string ParsePost(string post) {
            try {
                var doc = BBCodeDocument.Load(post, false);

                var LookupTable = HtmlRenderer.DefaultLookupTable.ToList();
                LookupTable.Remove(LookupTable.First(x => x.Key == "code"));
                LookupTable.Remove(LookupTable.First(x => x.Key == "i"));
                LookupTable.Remove(LookupTable.First(x => x.Key == "img"));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("code", RenderCode));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("s", RenderStrikethough));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("img", RenderImg));

                return doc.Children.ToHtml(false, LookupTable.ToArray());
            } catch {
                return post.Replace("\n", "<br />");
            }
        }

        public string GetPreviewPost(string post) {
            post = ParsePost(post);
            var temp = post.Split("<br />", StringSplitOptions.RemoveEmptyEntries);
            return temp[0] + "<br /><br />" + temp[1];
        }

        public static string RenderCode(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<pre class=\"prettyprint linenums:1\">" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "</pre>";
        }
        public static string RenderStrikethough(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<del>" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "</del>";
        }

        public static string RenderImg(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<img src='" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "'/>";
        }


    }
}
