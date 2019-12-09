using System;
using System.Collections.Generic;
using System.Linq;
using _12thMorning.Data;
using _12thMorning.Libraries;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace _12thMorning.Services {
    public class CommentService {
        private _12thMorningContext DB {
            get { return new _12thMorningContext(); }
        }

        public List<Comment> Get(int Id) {
            //var temp = DB.Comment
            //    .Include(x => x.Replies)
            //    .ThenInclude(x => x.Replies)
            //    .Where(x => x.BlogId == Id && x.ReplyTo == null)
            //    .ToList();
            //var temp2 = "a";
            //return temp;

            var rootComments = DB.Comment
                .Where(x => x.BlogId == Id && x.ReplyTo == null)
                .ToList();

            foreach(var comment in rootComments) {
                GetReplies(comment);
            }
            return rootComments;
        }

        private Comment GetReplies(Comment comment) {
            var replies = DB.Comment.Where(x => x.ReplyTo == comment.Id).ToList();
            comment.Replies = replies;
            foreach (var reply in comment.Replies) {
                GetReplies(reply);
            }
            return comment;
        }
    }
}
