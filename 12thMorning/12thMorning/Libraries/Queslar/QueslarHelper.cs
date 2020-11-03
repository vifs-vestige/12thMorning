using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.Enums;

namespace _12thMorning.Libraries.Queslar {
    public class QueslarHelper {

        public static long GetBoostedBoost(long level, long perLevel) {
            var levelMod = (long)Math.Floor(level / perLevel + 0.0);
            return levelMod * (levelMod + 1) / 2 * perLevel + ((level % perLevel) * (levelMod + 1));
        }

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
