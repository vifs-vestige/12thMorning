using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Village {
        public VillageBuildings boosts { get; set; }
        public VillageStrengths strengths { get; set; }
    }

    public class VillageBuildings {
        public int id { get; set; }
        public int village_id { get; set; }
        public int mill { get; set; }
        public int market { get; set; }
        public int stable { get; set; }
        public int well { get; set; }
        public int mason { get; set; }
        public int church { get; set; }
        public int warehouse { get; set; }
        public int tavern { get; set; }
    }

    public class VillageStrengths {
        public int id { get; set; }
        public int village_id { get; set; }
        public int brave { get; set; }
        public int noble { get; set; }
        public int wealthy { get; set; }
        public int bold { get; set; }
        public int loyal { get; set; }
        public int swift { get; set; }
    }
}
