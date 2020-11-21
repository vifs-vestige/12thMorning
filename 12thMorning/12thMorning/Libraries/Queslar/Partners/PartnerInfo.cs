using System;
using System.Reflection.Metadata.Ecma335;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar.Partners {

    public class PartnerInfo {
        public ResTypes ResType;
        public long Speed;
        public long Intelligence;
        public long Level;
        public long PlayerStat;
        public long PartnerStat;

        public double Seconds;
        public double IntPercent;
        public long TotalStats;
        public long Res;
        public long ResPerHour;
        public long Spent;
        public string Name;

        private Partner _Partner;
        private Stats _Stats;

        public PartnerInfo(Partner partner, Stats stats) {
            UpdateSource(partner, stats);
        }

        public void UpdateSource(Partner partner, Stats stats) {
            _Partner = partner;
            Name = partner.name;
            _Stats = stats;
            Speed = partner.speed;
            Intelligence = partner.intelligence;
            SetResType((ResTypes)partner.action_id);
        }

        public void SetResType(ResTypes res) {
            ResType = res;
            PlayerStat = res.GetStat(_Stats);
            PartnerStat = res.GetStat(_Partner);
            Level = res.GetPartner(_Partner);
            Update();
        }

        public void Update() {
            Seconds = Math.Round((6.0 / (0.1 + Speed / (Speed + 2500.0)) * 3) * 100) / 100;
            IntPercent = 20.0 + Intelligence / (Intelligence + 250.0) * 100.0;
            TotalStats = (long)Math.Round(IntPercent / 100.0 * PlayerStat) + PartnerStat;
            Res = ((long)Math.Floor(TotalStats / 100.0)) + 1;
            Res *= 3;
            ResPerHour = (long)Math.Floor((3600.0 / Seconds) * Res);
            Spent = (((Intelligence * (Intelligence + 1)) / 2) + ((Speed * (Speed + 1)) / 2)) * 10000;
        }
    }
}
