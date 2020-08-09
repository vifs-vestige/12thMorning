using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Stats {
        public int id { get; set; }
        public int player_id { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
    }
}
