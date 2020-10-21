using System;
using System.Collections.Generic;
using System.Linq;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerWrapper {
        public Dictionary<int, PartnerInfo> PartnerInfos = new Dictionary<int, PartnerInfo>();
        public long CurrentPrice;
        public long CurrentResHour;
        public long NewPrice;
        public long NewResHour;
        public int Tax = 0;
        private FullWrapper RootInfo;
        public int KingdomBonus;
        public Dictionary<ResTypes, PartnerFinal> FinalPartners;

        public PartnerWrapper(FullWrapper info, int tax) {
            RootInfo = info;
            Tax = tax;
            foreach (var partner in info.BaseInfo.partners) {
                PartnerInfos[partner.id] = new PartnerInfo(partner, info);
            }
            if (RootInfo.BaseInfo.kingdom.tiles != null) {
                foreach (var kingdomTiles in RootInfo.BaseInfo.kingdom.tiles) {
                    if (kingdomTiles.resource_one_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_one_value;
                    }
                    if (kingdomTiles.resource_two_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_two_value;
                    }
                    if (kingdomTiles.resource_three_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_three_value;
                    }
                }
            }
            FinalPartners = new Dictionary<ResTypes, PartnerFinal>();
            foreach (ResTypes resType in Enum.GetValues(typeof(ResTypes))) {
                FinalPartners.Add(resType, new PartnerFinal(info.BaseInfo.pets.Where(x => x.active_food == resType.ToString().ToLower()).Sum(x => x.efficiency_tier + 1)));
            }
            UpdateTotals();
        }

        public void SetRes(int id, ResTypes res) {
            PartnerInfos[id].setType(res);
            UpdateTotals();
        }

        public void updateNew() {
            foreach (var partner in PartnerInfos.Values) {
                partner.New.update();
            }
            UpdateTotals();
            UpdateBoosts();
        }

        public void UpdateTotals() {
            NewPrice = 0;
            NewResHour = 0;
            CurrentPrice = 0;
            CurrentResHour = 0;
            foreach (var partner in PartnerInfos.Values) {
                NewPrice += partner.New.TotalSpent;
                NewResHour += partner.New.ResPerHourPre;
                CurrentPrice += partner.Current.TotalSpent;
                CurrentResHour += partner.Current.ResPerHourPre;
            }
        }

        public void UpdateBoosts() {

            foreach (var x in FinalPartners.Values) {
                x.clear();
            }
            foreach (var partner in PartnerInfos.Values) {
                partner.UpdateBoosts();
                FinalPartners[partner.ResType].add(partner);
            }

            foreach (var x in FinalPartners.Values) {
                x.finalize();
            }
        }
    }
}
