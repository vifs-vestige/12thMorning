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
        public double RelicBoost;

        public double EnchantBoost;
        public long HouseBoost;

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
            SetResType(resType);
        }

        public void SetResType(ResTypes res) {
            ResType = res;
            RelicLevel = res.GetPartner(_Boosts);
            HouseLevel = res.GetPartner(_House);
            EnchantBoost = 0;
            foreach (var x in _Equipment) {
                if (x.enchant_type == ResType.ToString().ToLower()) {
                    var toAdd = (double)(Math.Pow((double)x.enchant_value, 0.425) / 2);
                    if (x.player_id == x.enchant_ownership) {
                        toAdd *= 1.25;
                    }
                    EnchantBoost += toAdd;
                }
            }
            EnchantBoost = Math.Round(EnchantBoost, 2);
            Update();
        }

        public void Update() {
            HouseBoost = QueslarHelper.GetBoostedBoost(HouseLevel, 15);
            RelicBoost = RelicLevel * .025;
        }
    }
}
