using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {
    public class House {
        public int id { get; set; }
        public int player_id { get; set; }
        public int chairs { get; set; }
        public int stove { get; set; }
        public int sink { get; set; }
        public int basket { get; set; }
        public int table { get; set; }
        public int couch { get; set; }
        public int carpet { get; set; }
        public int candlestick { get; set; }
        public int pitchfork { get; set; }
        public int shed { get; set; }
        public int fountain { get; set; }
        public int tools { get; set; }
        public int bed { get; set; }
        public int closet { get; set; }
        public int nightstand { get; set; }
        public int window { get; set; }
        public string current_building { get; set; }
        public DateTime building_timer { get; set; }
    }
}
