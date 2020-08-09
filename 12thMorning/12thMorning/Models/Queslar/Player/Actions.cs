using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Actions {
        public int id { get; set; }
        public int player_id { get; set; }
        public int actions_remaining { get; set; }
        public string action_type { get; set; }
        public int default_actions { get; set; }
        public int monster_id { get; set; }
        public string harvest_type { get; set; }
        public string harvest_area { get; set; }
        public int area { get; set; }
        public int last_harvest_income { get; set; }
        public int last_battling_income { get; set; }
        public int has_started_actions { get; set; }
        public int highest_monster { get; set; }
    }
}
