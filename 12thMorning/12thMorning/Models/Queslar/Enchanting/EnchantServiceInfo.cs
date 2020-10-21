using System;

namespace _12thMorning.Models.Queslar.Enchanting {
    public class EnchantServiceInfo {
        public double Avg;
        public int Level;
        public int Cost;
        public int ActionCount = 0;
        public int RelicCost;
        public int ResCost;
        public double Value;
        public double OwnershipValue;
        public double ActionValue;
        public double OwnershipActionValue;
        public double SpeedValue;
        public double OwnershipSpeedValue;

        public EnchantServiceInfo(double avg, int level, int cost) {
            Avg = avg;
            Level = level;
            Cost = cost;
            ActionCount = (int)Math.Round(Math.Sqrt(level) * 4 * 3);
            var rawValue = ActionCount * .75 * Avg;
            RelicCost = ActionCount * Cost;
            ResCost = (int)Math.Round(250 + (Level * 2.5));
            Value = Math.Pow(rawValue, .425) / 2;
            OwnershipValue = Math.Round(Value * 1.25, 3);
            Value = Math.Round(Value, 3);
            ActionValue = rawValue / 10;
            OwnershipActionValue = Math.Round(ActionValue * 1.25);
            ActionValue = Math.Round(ActionValue);
            SpeedValue = Math.Pow(rawValue, .8) / 100;
            OwnershipSpeedValue = Math.Round(SpeedValue * 1.25, 3);
            SpeedValue = Math.Round(SpeedValue, 3);
        }
    }
}
