using bbsharp;
using bbsharp.Renderers.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Libraries {
    public class BBCode {


        public static string RenderCode(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<pre class=\"prettyprint linenums:1\">" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "</pre>";
        }
        public static string RenderStrikethough(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<del>" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "</del>";
        }

        public static string RenderImg(BBCodeNode Node, bool ThrowOnError, object LookupTable) {
            return "<img src='" + Node.Children.ToHtml(ThrowOnError, LookupTable) + "'/>";
        }

        public static string ParsePost(string post) {
            try {
                var doc = BBCodeDocument.Load(post, false);

                var LookupTable = HtmlRenderer.DefaultLookupTable.ToList();
                LookupTable.Remove(LookupTable.First(x => x.Key == "code"));
                LookupTable.Remove(LookupTable.First(x => x.Key == "i"));
                LookupTable.Remove(LookupTable.First(x => x.Key == "img"));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("code", BBCode.RenderCode));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("s", BBCode.RenderStrikethough));
                LookupTable.Add(new KeyValuePair<string, HtmlRendererCallback>("img", BBCode.RenderImg));

                return doc.Children.ToHtml(false, LookupTable.ToArray());
            }
            catch {
                return post.Replace("\n", "<br />");
            }
        }


    }
}
