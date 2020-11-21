using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerBoostInfo {
        public ResTypes ResType;
        public int HouseLevel;
        public int RelicLevel;
        public double RelicBoost { get { return RelicLevel * .025; } }

        public double EnchantBoost { get { return GetEnchantBoost(); } }
        public long HouseBoost { get { return QueslarHelper.GetBoostedBoost(HouseLevel, 15); } }

        private House _House;
        private List<EquipmentEquipped> _Equipment;
        private Boosts _Boosts;

        public PartnerBoostInfo(House house, List<EquipmentEquipped> equipment, Boosts boosts, ResTypes resType) {
            UpdateSource(house, equipment, boosts, resType);
        }

        public void UpdateSource(House house, List<EquipmentEquipped> equipment, Boosts boosts, ResTypes resType) {
            _House = house;
            _Equipment = equipment;
            _Boosts = boosts;
            ResType = resType;
            RelicLevel = ResType.GetPartner(_Boosts); 
            HouseLevel = ResType.GetPartner(_House);
        }

        private double GetEnchantBoost() {
            double enchantBoost = 0;
            foreach (var x in _Equipment) {
                if (x.enchant_type == ResType.ToString().ToLower()) {
                    var toAdd = (double)(Math.Pow((double)x.enchant_value, 0.425) / 2);
                    if (x.player_id == x.enchant_ownership) {
                        toAdd *= 1.25;
                    }
                    enchantBoost += toAdd;
                }
            }
            return enchantBoost;
        }

    }
}
