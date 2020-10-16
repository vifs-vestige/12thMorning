using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Models.Queslar.Wrappers.Fighters {
    public class FighterInfo {
        public string classType;
        public int Hp;
        public int Def;
        public int Damage;
        public double Crit;
        public int Hit;
        public int Dodge;

        public FighterInfo(Fighter fighter, int numberOfFighters) {
            classType = fighter.fighterClass;
            Hp = (int)Math.Floor((fighter.health * 100 + 500) * (1 + numberOfFighters * .25));
            Def = (int)Math.Floor((fighter.defense * 10 + 25) * (1 + numberOfFighters * .25));
            Damage = (int)Math.Floor((fighter.damage * 25 + 100) * (1 + numberOfFighters * .25));
            Crit = (int)Math.Floor((fighter.crit_damage * .25) * (1 + numberOfFighters * .25));
            Hit = (int)Math.Floor((fighter.hit * 50 + 50) * (1 + numberOfFighters * .25));
            Dodge = (int)Math.Floor((fighter.dodge * 10 + 50) * (1 + numberOfFighters * .25));
        }

    }
}
