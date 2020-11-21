using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.AbstractModels;

namespace _12thMorning.Models.Queslar.Player {
    public class Partner : IStats, IPartners {
        public int id { get; set; }
        public int player_id { get; set; }
        public int action_id { get; set; }
        public string harvest_area { get; set; }
        public string name { get; set; }
        public int partner_id { get; set; }
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
        public int battling_experience { get; set; }
        public int hunting_experience { get; set; }
        public int mining_experience { get; set; }
        public int woodcutting_experience { get; set; }
        public int stonecarving_experience { get; set; }
        public int intelligence { get; set; }
        public int speed { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
    }

}
