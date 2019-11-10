using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Data {
    public class BlogService {

        public int getDb() {
            var item = 777;
            using (var db = new _12thMorningContext()) {
                item = db.Test.First().Number;
            }
            return item;
        }
    }
}
