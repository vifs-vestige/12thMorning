using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.Enums;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar {
    public class InventoryWrapper {
        public List<EquipmentEquipped> Equipment;
        public List<InventoryStats> Stats;
        public int TotalTotalStats;
        public int TotalDamage;
        public int TotalDefense;
        public int TotalStrength;
        public int TotalHealth;
        public int TotalAgility;
        public int TotalDex;
        public Dictionary<string, double> TotalEnchant;

        public InventoryWrapper(List<EquipmentEquipped> gear, EquipmentSlots upgrades) {
            Equipment = gear;
            Stats = new List<InventoryStats>();
            TotalEnchant = new Dictionary<string, double>();
            foreach(var x in gear) {
                var temp = new InventoryStats(x, upgrades);
                Stats.Add(temp);
                TotalTotalStats += temp.TotalStats;
                TotalDamage += temp.Damage;
                TotalDefense += temp.Defense;
                TotalStrength += temp.Strength;
                TotalHealth += temp.Health;
                TotalAgility += temp.Agility;
                TotalDex += temp.Dex;
                if(TotalEnchant.ContainsKey(temp.EnchantType)) {
                    TotalEnchant[temp.EnchantType] += temp.EnchantValue;
                } else if (temp.EnchantType != "") {
                    TotalEnchant[temp.EnchantType] = temp.EnchantValue;
                }
            }
        }
    }

    public class InventoryStats {
        public EquipmentEquipped RootInfo;
        public int Level;
        public int TotalStats;
        public int Damage;
        public int Defense;
        public int Strength;
        public int Health;
        public int Agility;
        public int Dex;
        public int Upgrade;
        public string EnchantType = "";
        public double EnchantValue = 0;

        public InventoryStats(EquipmentEquipped info, EquipmentSlots upgrades) {
            RootInfo = info;
            var temp2 = QueslarHelper.FromInventory(info.item_type).ToString();
            var temp3 = (typeof(EquipmentSlots)).GetProperty(temp2);
            Upgrade = (int)temp3.GetValue(upgrades);
            if (info.enchant_value != null) {
                var temp = info.enchant_type;
                if (temp == "actions") {
                    EnchantValue = (int)info.enchant_value / 10;
                }
                else if (temp == "enchanting" || temp == "crafting") {
                    EnchantValue = Math.Pow((double)info.enchant_value, .8) / 100 / 100;
                }
                else {
                    EnchantValue = Math.Pow((double)info.enchant_value, 0.425) / 2 / 100;
                }

                if (info.enchant_ownership == info.player_id) {
                    EnchantValue = Math.Round(EnchantValue * 1.25, 5);
                }
                else {
                    EnchantValue = Math.Round(EnchantValue, 5);
                }
                EnchantType = info.enchant_type;
            }
            TotalStats = (int) Math.Round(info.total_stats / 1.04);
            Damage = (int) Math.Round(info.damage / 1.04);
            Defense = (int) Math.Round(info.defense/ 1.04);
            Strength = (int) Math.Round(info.strength / 1.04);
            Health = (int) Math.Round(info.health / 1.04);
            Agility = (int) Math.Round(info.agility/ 1.04);
            Dex = (int) Math.Round(info.dexterity / 1.04);

        }
    }

}
