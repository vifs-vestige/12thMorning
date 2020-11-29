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
        public Dictionary<ResTypes, PartnerTotalInfo> Totals = new Dictionary<ResTypes, PartnerTotalInfo>();

        public PartnerDifference Difference;

        public PartnerWrapper(FullWrapper info, int tax) {
            foreach(ResTypes res in Enum.GetValues(typeof(ResTypes))) {
                BoostsInfo[res] = new PartnerBoostInfo(info.BaseInfo.house, info.BaseInfo.equipmentEquipped, info.BaseInfo.boosts, res);
            }
            foreach(var pet in info.BaseInfo.pets) {
                PetsInfo[pet.id] = new PetInfo(pet);
            }
            GlobalInfo = new PartnerGlobalInfo(info.BaseInfo.kingdom, info.BaseInfo.village, tax, info.Vip);
            foreach (var partner in info.BaseInfo.partners) {
                PartnersInfo[partner.id] = new PartnerInfo(partner, info.BaseInfo.stats);
            }
            foreach (var partner in PartnersInfo) {
                PartnersIncomeInfo[partner.Key] = new PartnerIncomeInfo(partner.Value, BoostsInfo[partner.Value.ResType], GlobalInfo);
            }
            foreach (ResTypes res in Enum.GetValues(typeof(ResTypes))) {
                Totals[res] = new PartnerTotalInfo(res, PartnersIncomeInfo.Values.ToList(), PetsInfo.Values.ToList());
            }

            Difference = new PartnerDifference(PartnersInfo.Values.ToList(), Totals);
        }

        public void Update() {
        }

    }
}
