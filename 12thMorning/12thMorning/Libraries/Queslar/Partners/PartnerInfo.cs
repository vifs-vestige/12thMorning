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

        public double Seconds { get { return Math.Round((6.0 / (0.1 + Speed / (Speed + 2500.0)) * 3) * 100) / 100; } }
        public double IntPercent { get { return 20.0 + Intelligence / (Intelligence + 250.0) * 100.0; } }
        public long TotalStats { get { return (long)Math.Round(IntPercent / 100.0 * PlayerStat) + PartnerStat; } }
        public long Res { get { return ((long) (Math.Floor(TotalStats / 100.0)) + 1) * 3; } }
        public long ResPerHour { get { return (long)Math.Floor((3600.0 / Seconds) * Res); } }
        public long Spent { get { return (((Intelligence * (Intelligence + 1)) / 2) + ((Speed * (Speed + 1)) / 2)) * 10000; } }
        public string Name { get { return _Partner.name; } }

        private Partner _Partner;
        private Stats _Stats;

        public PartnerInfo(Partner partner, Stats stats) {
            UpdateSource(partner, stats);
        }

        public void UpdateSource(Partner partner, Stats stats) {
            _Partner = partner;
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
        }

    }
}
