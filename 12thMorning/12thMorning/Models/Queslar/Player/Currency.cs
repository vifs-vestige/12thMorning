using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class Currency {
        public int id { get; set; }
        public int player_id { get; set; }
        public int gold { get; set; }
        public int credits { get; set; }
        public int relics { get; set; }
        public int meat { get; set; }
        public int iron { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int normal_wootz { get; set; }
        public int epic_wootz { get; set; }
        public int rare_wootz { get; set; }
        public int magical_wootz { get; set; }
        public int unique_wootz { get; set; }
        public int relic_wootz { get; set; }
        public int frog_toe { get; set; }
        public int dog_tongue { get; set; }
        public int lizard_leg { get; set; }
        public int wolf_tooth { get; set; }
        public int shattered_partner_gold { get; set; }
        public int shattered_fighter_gold { get; set; }
        public int shattered_battling_relics { get; set; }
        public int shattered_crafting_relics { get; set; }
        public int shattered_enchanting_relics { get; set; }
        public int shattered_partner_relics { get; set; }
        public int voting_points { get; set; }
    }
}
