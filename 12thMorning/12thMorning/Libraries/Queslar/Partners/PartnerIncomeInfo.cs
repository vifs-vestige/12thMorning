using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerIncomeInfo {
        public double Res;
        public long Taxed;
        public long ResPostTax;
        public ResTypes ResType;


        public PartnerInfo PartnerInfo;
        private PartnerBoostInfo BoostInfo;
        private PartnerGlobalInfo GlobalInfo;

        public PartnerIncomeInfo(PartnerInfo partnerInfo, PartnerBoostInfo boostInfo, PartnerGlobalInfo globalInfo) {
            PartnerInfo = partnerInfo;
            ResType = partnerInfo.ResType;
            BoostInfo = boostInfo;
            GlobalInfo = globalInfo;
            Update();
        }

        public void Update() {
            var vipBonus = GlobalInfo.Vip ? 1.1 : 1;
            Res = ((1 + ((BoostInfo.RelicBoost) + BoostInfo.EnchantBoost + BoostInfo.HouseBoost + GlobalInfo.VillageBoost + (PartnerInfo.Level / 100.0)) / 100.0) * (vipBonus) * (1 + GlobalInfo.KingdomVillageBoost + GlobalInfo.KingdomResourceBoost / 100.0)) * PartnerInfo.ResPerHour;

            if(GlobalInfo.Tax != 0) {
                Taxed = (long) Math.Floor(Res * (GlobalInfo.Tax / 100.0));
            } else {
                Taxed = 0;
            }

            ResPostTax = (long)Math.Round(Res) - Taxed;
        }
    }
}
