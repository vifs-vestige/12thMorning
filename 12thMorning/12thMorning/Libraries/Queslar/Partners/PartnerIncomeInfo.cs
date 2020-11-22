using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerIncomeInfo {
        public double Res { get { return ((1 + (BoostInfo.RelicBoost + BoostInfo.EnchantBoost + BoostInfo.HouseBoost + GlobalInfo.VillageBoost + (PartnerInfo.Level / 100.0)) / 100.0) * (VipBonus) * (1 + GlobalInfo.VIllageKingdomBoost + GlobalInfo.KingdomResourceBoost / 100.0)) * PartnerInfo.Res; } }
        public long Taxed { get { return (GlobalInfo.Tax != 0) ? (long)Math.Floor(Res * (GlobalInfo.Tax / 100.0)) : 0; } }
        public double ResPostTaxPerHour { get { return ResPerHour - TaxedPerHour; } }
        public double ResPerHour { get { return (long)Math.Floor((3600.0 / PartnerInfo.Seconds) * Res); } }
        public long TaxedPerHour { get { return (long)Math.Floor((3600.0 / PartnerInfo.Seconds) * Taxed); } }
        public ResTypes ResType;


        public PartnerInfo PartnerInfo;
        private PartnerBoostInfo BoostInfo;
        private PartnerGlobalInfo GlobalInfo;
        private double VipBonus { get { return GlobalInfo.Vip ? 1.1 : 1; } }

        public PartnerIncomeInfo(PartnerInfo partnerInfo, PartnerBoostInfo boostInfo, PartnerGlobalInfo globalInfo) {
            PartnerInfo = partnerInfo;
            ResType = partnerInfo.ResType;
            BoostInfo = boostInfo;
            GlobalInfo = globalInfo;
        }
    }
}
