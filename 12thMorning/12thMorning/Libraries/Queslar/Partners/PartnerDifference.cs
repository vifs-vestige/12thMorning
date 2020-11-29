using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerDifference {
        public PartnerDiffInfo Old;
        public PartnerDiffInfo New { get { return _new.Update(); } }
        private PartnerDiffInfo _new;

        public PartnerDifference(List<PartnerInfo> partnersInfo, Dictionary<ResTypes, PartnerTotalInfo> totals) {
            Old = new PartnerDiffInfo(partnersInfo, totals);
            _new = new PartnerDiffInfo(partnersInfo, totals);
        }
    }

    public class PartnerDiffInfo {
        public long Spent;
        public double ResDay;
        public double MeatHour;
        public double IronHour;
        public double WoodHour;
        public double StoneHour;

        private List<PartnerInfo> PartnersInfo;
        private Dictionary<ResTypes, PartnerTotalInfo> Totals;


        public PartnerDiffInfo(List<PartnerInfo> partnersInfo, Dictionary<ResTypes, PartnerTotalInfo> totals) {
            PartnersInfo = partnersInfo;
            Totals = totals;
            Update();
        }

        internal PartnerDiffInfo Update() {
            Spent = PartnersInfo.Sum(x => x.Spent);
            ResDay = Totals.Values.Sum(x => x.Outcome) * 24;
            MeatHour = Totals[ResTypes.meat].Outcome;
            IronHour = Totals[ResTypes.iron].Outcome;
            WoodHour = Totals[ResTypes.wood].Outcome;
            StoneHour = Totals[ResTypes.stone].Outcome;
            return this;
        }
    }
}
