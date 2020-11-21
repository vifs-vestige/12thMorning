using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.AbstractModels;

namespace _12thMorning.Models.Queslar.Player {
    public class Boosts : IPartners {
        public int id { get; set; }
        public int player_id { get; set; }
        public int critChance { get; set; }
        public int critDamage { get; set; }
        public int multistrike { get; set; }
        public int healing { get; set; }
        [JsonPropertyName("hunting_boost")]
        public int hunting { get; set; }
        [JsonPropertyName("mining_boost")]
        public int mining { get; set; }
        [JsonPropertyName("woodcutting_boost")]
        public int woodcutting { get; set; }
        [JsonPropertyName("stonecarving_boost")]
        public int stonecarving { get; set; }
        public int luck { get; set; }
        public int power { get; set; }
        public int crafting { get; set; }
        public int consistency { get; set; }
        public int fortune { get; set; }
        public int capacity { get; set; }
        public int enchanting_boost { get; set; }
        public int stability { get; set; }
        public int battle { get; set; }
        public int harvest { get; set; }
        public int craft { get; set; }

    }
}
