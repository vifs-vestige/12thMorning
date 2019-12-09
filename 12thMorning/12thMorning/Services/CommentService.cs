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
            return DB.Comment
                .Include(x => x.Replies)
                .ThenInclude(x => x.Replies)
                .Where(x => x.BlogId == Id && x.ReplyTo == null)
                .ToList();
        }
    }
}
