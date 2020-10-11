using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Pet {
        public int id { get; set; }
        public int player_id { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public string name { get; set; }
        public string active_food { get; set; }
        public int efficiency_tier { get; set; }
        public int efficiency_lock { get; set; }
        public int auto_feed { get; set; }
        public int pet_id { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
        public int monster_strength { get; set; }
        public int monster_health { get; set; }
        public int monster_agility { get; set; }
        public int monster_dexterity { get; set; }
        public int meat { get; set; }
        public int iron { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
    }
}
