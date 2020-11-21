using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerWrapper {
        public Dictionary<int, PartnerInfo> PartnersInfo = new Dictionary<int, PartnerInfo>();
        public Dictionary<ResTypes, PartnerBoostInfo> BoostsInfo = new Dictionary<ResTypes, PartnerBoostInfo>();
        public Dictionary<int, PetInfo> PetsInfo = new Dictionary<int, PetInfo>();
        public PartnerGlobalInfo GlobalInfo;

        public Dictionary<int, PartnerIncomeInfo> PartnersIncomeInfo = new Dictionary<int, PartnerIncomeInfo>();

        public long TotalSpent;
        public int VillageLevel;

        public PartnerWrapper(FullWrapper info, int tax) {
            foreach(ResTypes res in Enum.GetValues(typeof(ResTypes))) {
                BoostsInfo[res] = new PartnerBoostInfo(info.BaseInfo.house, info.BaseInfo.equipmentEquipped, info.BaseInfo.boosts, res);
            }
            foreach(var pet in info.BaseInfo.pets) {
                PetsInfo[pet.id] = new PetInfo(pet);
            }
            GlobalInfo = new PartnerGlobalInfo(info.BaseInfo.kingdom, info.BaseInfo.village, tax, info.Vip);
            foreach (var partner in info.BaseInfo.partners) {
                var temp = new PartnerInfo(partner, info.BaseInfo.stats);
                PartnersInfo[partner.id] = temp;
            }

            UpdatePartnerIncome();
            Update();
        }

        public void Update() {
            foreach(var partner in PartnersInfo.Values) {
                partner.Update();
            }
            UpdateTotalSpent();
            foreach (var pet in PetsInfo.Values) {
                pet.Update();
            }
            foreach(var boost in BoostsInfo.Values) {
                boost.Update();
            }
            GlobalInfo.Update();
            foreach(var partner in PartnersIncomeInfo.Values) {
                partner.Update();
            }
        }

        public void UpdatePartnerIncome() {
            PartnersIncomeInfo = new Dictionary<int, PartnerIncomeInfo>();
            var petsInfo = new Dictionary<ResTypes, List<PetInfo>>();
            foreach(var pet in PetsInfo) {
                if(!petsInfo.ContainsKey(pet.Value.ResType)) {
                    petsInfo[pet.Value.ResType] = new List<PetInfo>();
                }
                petsInfo[pet.Value.ResType].Add(pet.Value);
            }
            foreach(var partner in PartnersInfo) {
                var resType = partner.Value.ResType;
                PartnersIncomeInfo[partner.Key] = new PartnerIncomeInfo(partner.Value, BoostsInfo[resType], petsInfo[resType], GlobalInfo);
            }
        }

        private void UpdateTotalSpent() {
            TotalSpent = 0;
            foreach (var partner in PartnersInfo.Values) {
                TotalSpent = partner.Spent;
            }
        }

    }
}
