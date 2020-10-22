using System;
using System.Collections.Generic;

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

        private EnchantServiceInfo(double avg, int level, int cost) {
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

        public static List<EnchantServiceInfo> Build(double avg, int level, int cost) {
            var temp = (int) Math.Round(Math.Sqrt(level) * 4 * 3);
            var lowerLevel = (int)Math.Round(Math.Pow((temp - .5) / 3 / 4, 2));
            var upperLevel = (int)Math.Round(Math.Pow((temp + .5) / 3 / 4, 2)) + 1;
            var returnList = new List<EnchantServiceInfo>();
            returnList.Add(new EnchantServiceInfo(avg, level, cost));
            returnList.Add(new EnchantServiceInfo(avg, lowerLevel, cost));
            returnList.Add(new EnchantServiceInfo(avg, upperLevel, cost));

            return returnList;
        }
    }
}
