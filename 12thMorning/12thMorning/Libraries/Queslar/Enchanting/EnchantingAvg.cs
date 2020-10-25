using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Libraries.Queslar.Enchanting {
    public class EnchantingAvg {
        private FullWrapper RootInfo;

        public EnchantingAvg(FullWrapper info) {
            RootInfo = info;
        }

        public double CalculateAvg() {
            var boosts = RootInfo.BaseInfo.boosts;
            var house = RootInfo.BaseInfo.house;
            var baseStat = ((boosts.capacity * .05) + QueslarHelper.GetBoostedBoost(house.couch, 15)) * 2.0 / 100.0;
            var consistency = (1.0 - 1.0 / (1.0 + ((boosts.stability * .05) + QueslarHelper.GetBoostedBoost(house.candlestick, 15)) / 100.0));
            var capacity = ((((boosts.enchanting_boost * .05) + QueslarHelper.GetBoostedBoost(house.carpet, 15))/100) + 1) * 2.0;
            var multiplier = (1.0 + (RootInfo.BaseInfo.skills.enchanting / 10000.0) + (QueslarHelper.GetBoostedBoost(RootInfo.BaseInfo.village.boosts.church, 20))/100.0) *
                ((RootInfo.Vip ? 1.1 : 1.0)) * (1.0 + (RootInfo.BaseInfo.kingdom.tiles.Count() * .05));
            var max = Math.Round(((baseStat + capacity) * multiplier) / 3.0, 3);
            var min = Math.Round(((capacity * consistency) * multiplier) / 3.0, 3);
            var doubleChance = Math.Round(((boosts.fortune * .035) + QueslarHelper.GetBoostedBoost(house.table, 15))/100.0,3);
            var avg = Math.Round(((max + min) / 2.0) * (1.0 + doubleChance), 3);
            return avg;
        }
    }
}
