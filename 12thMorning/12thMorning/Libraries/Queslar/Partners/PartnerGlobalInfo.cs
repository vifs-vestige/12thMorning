using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerGlobalInfo {
        public int Tax;
        public bool Vip;
        public long KingdomResourceBoost;
        public int KingdomVillageBoost;
        public int VIllageKingdomBoost { get { return (int)Math.Round((VillageBoost * KingdomVillageBoost) / 10000.0, 2); } }
        public int VillageLevel;
        public long VillageBoost { get { return QueslarHelper.GetBoostedBoost(VillageLevel, 20); } }

        private Kingdom _Kingdom;
        private Village _Village;


        public PartnerGlobalInfo(Kingdom kingdom, Village village, int tax, bool vip) {
            UpdateSource(kingdom, village, tax, vip);
        }

        public void UpdateSource(Kingdom kingdom, Village village, int tax, bool vip) {
            _Kingdom = kingdom;
            _Village = village;
            Tax = tax;
            Vip = vip;
            VillageLevel = village.boosts.mill;
            KingdomVillageBoost = kingdom.GetBoost("village");
            KingdomResourceBoost = kingdom.GetBoost("resource");
        }
    }
}
