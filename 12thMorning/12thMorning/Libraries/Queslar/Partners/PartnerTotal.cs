using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerTotal {
        public long Speed;
        public double Seconds;
        public long Intelligence;
        public double IntPercent;
        public long TotalStats;
        public long ResPre;
        public long ResPerHourPre;
        public long PlayerStat;
        public long PartnerStat;
        public long TotalSpent;

        public void update() {
            Seconds = Math.Round((6.0 / (0.1 + Speed / (Speed + 2500.0)) * 3) * 100) / 100;
            IntPercent = 20.0 + Intelligence / (Intelligence + 250.0) * 100.0;
            TotalStats = (long)Math.Round(IntPercent / 100.0 * PlayerStat) + PartnerStat;
            ResPre = ((long)Math.Floor(TotalStats / 100.0)) + 1;
            ResPre *= 3;
            ResPerHourPre = (long)Math.Floor((3600.0 / Seconds) * ResPre);
            TotalSpent = (((Intelligence * (Intelligence + 1)) / 2) + ((Speed * (Speed + 1)) / 2)) * 10000;
        }

        public void update(PartnerInfo partnerInfo, long speed, long intelligence) {
            PlayerStat = partnerInfo.PlayerStat;
            PartnerStat = partnerInfo.Stats;
            Speed = speed;
            Intelligence = intelligence;
            update();
        }
    }
}
