using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Kingdom {
        public List<Tiles> tiles { get; set; }

        public int GetBoost(string type) {
            var bonus = 0;
            foreach (var kingdomTiles in tiles) {
                if (kingdomTiles.resource_one_type == type) {
                    bonus += (int)kingdomTiles.resource_one_value;
                }
                if (kingdomTiles.resource_two_type == type) {
                    bonus += (int)kingdomTiles.resource_two_value;
                }
                if (kingdomTiles.resource_three_type == type) {
                    bonus += (int)kingdomTiles.resource_three_value;
                }
            }
            return bonus;
        }
    }

    public class Tiles {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int kingdom_id { get; set; }
        public string resource_one_type { get; set; }
        public string resource_two_type { get; set; }
        public string resource_three_type { get; set; }
        public int? resource_one_value { get; set; }
        public int? resource_two_value { get; set; }
        public int? resource_three_value { get; set; }
        public DateTime captured_time { get; set; }
    }
}
