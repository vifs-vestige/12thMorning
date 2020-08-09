using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Party {

        public int id { get; set; }
        public int player_id { get; set; }
        public int party_id { get; set; }
        public int daily_actions_remaining { get; set; }
        public int daily_actions_max { get; set; }
        public int current_monster { get; set; }
        public int current_area { get; set; }
    }
}
