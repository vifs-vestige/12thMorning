using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.AbstractModels;
using _12thMorning.Models.Queslar.Enums;
using _12thMorning.Models.Queslar.Player;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace _12thMorning.Libraries.Queslar {
    public static class QueslarHelper {

        public static long GetBoostedBoost(long level, long perLevel) {
            var levelMod = (long)Math.Floor(level / perLevel + 0.0);
            return levelMod * (levelMod + 1) / 2 * perLevel + ((level % perLevel) * (levelMod + 1));
        }

        public static int GetStat<T>(this ResTypes resType, T thing)
            where T : IStats => resType switch
            {
                ResTypes.meat => thing.strength,
                ResTypes.iron => thing.health,
                ResTypes.wood => thing.agility,
                ResTypes.stone => thing.dexterity
            };

        public static int GetPartner<T>(this ResTypes resType, T thing)
            where T : IPartners => resType switch
        {
            ResTypes.meat => thing.hunting,
            ResTypes.iron => thing.mining,
            ResTypes.wood => thing.woodcutting,
            ResTypes.stone => thing.stonecarving
        };

        public static InventoryTypes FromInventory(string item_type) => item_type switch
        {
            "weapon" => InventoryTypes.left_hand_level,
            "dagger" => InventoryTypes.right_hand_level,
            "shield" => InventoryTypes.right_hand_level,
            "helmet" => InventoryTypes.right_hand_level,
            "armor" => InventoryTypes.right_hand_level,
            "gloves" => InventoryTypes.right_hand_level,
            "leggings" => InventoryTypes.right_hand_level,
            "boots" => InventoryTypes.right_hand_level,
        };
    }
}
