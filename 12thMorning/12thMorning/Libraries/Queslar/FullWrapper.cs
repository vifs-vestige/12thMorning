using System;
using _12thMorning.Libraries.Queslar.Fighters;
using _12thMorning.Libraries.Queslar.Partners;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar {
    public class FullWrapper {
        public Full BaseInfo;
        public PartnerWrapper PartnerInfo;
        public FighterWrapper FighterInfo;
        public InventoryWrapper InventoryInfo;
        public bool Vip;

        public FullWrapper(Full root) {
            BaseInfo = root;
            Vip = false;
            if (root.player.vip_time != "0000-00-00 00:00:00") {
                var viptime = DateTime.Parse(root.player.vip_time);
                if (viptime > DateTime.Now) {
                    Vip = true;
                }
            }
        }

        public void AddPartnerWrapper(int tax = 0) {
            PartnerInfo = new PartnerWrapper(this, tax);
            foreach (var partner in PartnerInfo.PartnerInfos.Values) {
                partner.UpdateBoosts();
            }
            PartnerInfo.UpdateBoosts();
        }

        public void AddInventoryWrapper() {
            InventoryInfo = new InventoryWrapper(BaseInfo.equipmentEquipped, BaseInfo.equipmentSlots);
        }
    }
}
