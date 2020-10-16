using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Wrappers.Fighters {
    public class Mob {
        public int Level;
        public int Hp;
        public int Def;
        public int Damage;
        public double Crit;
        public int Hit;
        public int Dodge;

        public Mob(int level) {
            Level = level;
            level = level - 1;
            Hp = 500 + level * 400;
            Def = 30 + level * 10;
            Damage = 100 + level * 40;
            Crit = .25 + level * .25;
            Hit = 80 + level * 30;
            Dodge = Hit;
        }
    }
}
