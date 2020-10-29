using System;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerInfo {
        public ResTypes ResType;
        public Partner BaseInfo;
        public int Level;
        public long Stats;
        public long PlayerStat;
        public long TotalStat;
        public double Res;
        public long Taxed;
        public long ResPostTax;
        public double Boost;
        public int HouseBoost;
        public int BoostedHouseBoost;
        public int BoostedVillageBoost;
        public double EnchantBoost;
        public long ResHour;
        public long TaxedHour;
        public string Display = "res";
        public PartnerTotal Current = new PartnerTotal();
        public PartnerTotal New = new PartnerTotal();
        private FullWrapper RootInfo;

        public PartnerInfo(Partner baseInfo, FullWrapper info) {
            RootInfo = info;
            BaseInfo = baseInfo;
            setType((ResTypes)baseInfo.action_id);
            EnchantBoost = 0;
            foreach (var x in info.BaseInfo.equipmentEquipped) {
                if (x.enchant_type == ResType.ToString().ToLower()) {
                    var toAdd = (double)(Math.Pow((double)x.enchant_value, 0.425) / 2);
                    if (x.player_id == x.item_ownership) {
                        toAdd *= 1.25;
                    }
                    EnchantBoost += toAdd;
                }
            }
            EnchantBoost = Math.Round(EnchantBoost, 2);
        }

        public void UpdateBoosts() {
            var vipBonus = RootInfo.Vip ? 1.10 : 1;
            BoostedHouseBoost = QueslarHelper.GetBoostedBoost(HouseBoost, 15);
            BoostedVillageBoost = QueslarHelper.GetBoostedBoost(RootInfo.BaseInfo.village.boosts.mill, 20);
            var kingdomVillageBoost = Math.Round((BoostedVillageBoost * RootInfo.BaseInfo.kingdom.GetBoost("village")) / 10000.0, 2);

            Res = ((1 + ((Boost * .025) + EnchantBoost + BoostedHouseBoost + BoostedVillageBoost + (Level / 100.0)) / 100.0) *  (vipBonus) * (1 + kingdomVillageBoost + RootInfo.PartnerInfo.KingdomBonus / 100.0)) * New.ResPre;

            if (RootInfo.PartnerInfo.Tax != 0) {
                Taxed = (int)Math.Floor(Res * (RootInfo.PartnerInfo.Tax / 100.0));
            }
            else {
                Taxed = 0;
            }
            ResPostTax = (int)Math.Round(Res) - Taxed;
            ResHour = (int)Math.Floor((3600.0 / New.Seconds) * ResPostTax);
            TaxedHour = (int)Math.Floor((3600.0 / New.Seconds) * Taxed);
        }

        public void UpdateStats() {

            Current.update(this, BaseInfo.speed, BaseInfo.intelligence);
            New.update(this, BaseInfo.speed, BaseInfo.intelligence);
        }

        public void getResBoost() {
            var boost = 0;
            switch (ResType) {
                case ResTypes.Meat:
                    boost = RootInfo.BaseInfo.boosts.hunting_boost;
                    HouseBoost = RootInfo.BaseInfo.house.pitchfork;
                    break;
                case ResTypes.Iron:
                    boost = RootInfo.BaseInfo.boosts.mining_boost;
                    HouseBoost = RootInfo.BaseInfo.house.fountain;
                    break;
                case ResTypes.Wood:
                    boost = RootInfo.BaseInfo.boosts.woodcutting_boost;
                    HouseBoost = RootInfo.BaseInfo.house.tools;
                    break;
                case ResTypes.Stone:
                    boost = RootInfo.BaseInfo.boosts.stonecarving_boost;
                    HouseBoost = RootInfo.BaseInfo.house.shed;
                    break;
            }
            Boost = boost;
        }

        public void setType(ResTypes res) {
            ResType = res;
            Display = res.ToString();
            switch (res) {
                case ResTypes.Meat:
                    PlayerStat = RootInfo.BaseInfo.stats.strength;
                    Level = BaseInfo.hunting;
                    Stats = BaseInfo.strength;
                    break;
                case ResTypes.Iron:
                    PlayerStat = RootInfo.BaseInfo.stats.health;
                    Level = BaseInfo.mining;
                    Stats = BaseInfo.health;
                    break;
                case ResTypes.Wood:
                    PlayerStat = RootInfo.BaseInfo.stats.agility;
                    Level = BaseInfo.woodcutting;
                    Stats = BaseInfo.agility;
                    break;
                case ResTypes.Stone:
                    PlayerStat = RootInfo.BaseInfo.stats.dexterity;
                    Level = BaseInfo.stonecarving;
                    Stats = BaseInfo.dexterity;
                    break;
            }

            Current.update(this, BaseInfo.speed, BaseInfo.intelligence);
            New.update(this, BaseInfo.speed, BaseInfo.intelligence);
            getResBoost();


        }
    }
}
