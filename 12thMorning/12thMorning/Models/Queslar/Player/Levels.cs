using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Levels {

        public int id { get; set; }
        public int player_id { get; set; }
        public int battling { get; set; }
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
        public int crafting { get; set; }
        public int enchanting { get; set; }
        public int battling_experience { get; set; }
        public int hunting_experience { get; set; }
        public int mining_experience { get; set; }
        public int woodcutting_experience { get; set; }
        public int stonecarving_experience { get; set; }
        public int crafting_experience { get; set; }
        public int enchanting_experience { get; set; }
    }
}
