using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar {
    public interface IStats {
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
    }
}
