using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Kingdom {
        public List<Titles> titles = new List<Titles>();
    }

    public class Titles {
        public int id;
        public string type;
        public int kingdom_id;
        public string resource_one_type;
        public string resource_two_type;
        public string resource_three_type;
        public int reources_one_value;
        public int reources_two_value;
        public int reources_three_value;
    }
}
