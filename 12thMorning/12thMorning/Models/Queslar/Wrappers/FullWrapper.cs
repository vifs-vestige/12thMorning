using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.Player;
using _12thMorning.Models.Queslar.Wrappers.Fighters;

namespace _12thMorning.Models.Queslar.Wrappers {
    public class FullWrapper {
        public Full BaseInfo;
        public PartnerWrapper PartnerInfo;
        public FighterWrapper FighterInfo;
        public bool Vip;

        public FullWrapper(Full root, int tax = 0) {
            BaseInfo = root;
            Vip = false;
            if (root.player.vip_time != "0000-00-00 00:00:00") {
                var viptime = DateTime.Parse(root.player.vip_time);
                if (viptime > DateTime.Now) {
                    Vip = true;
                }
            }
            PartnerInfo = new PartnerWrapper(this, tax);
            foreach (var partner in PartnerInfo.PartnerInfos.Values) {
                partner.UpdateBoosts();
            }
            PartnerInfo.UpdateBoosts();
        }
    }
}
