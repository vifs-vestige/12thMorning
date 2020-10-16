using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Wrappers.Fighters {
    public class FighterWrapper {
        public List<Mob> Mobs;
        public List<FighterInfo> Fighters;
        private FullWrapper RootInfo;
        public double RoundsToWin = 0;
        public double RoundsToLose = 0;
        public double WinChance = 0;


        public FighterWrapper(FullWrapper info) {
            RootInfo = info;
            SetupMobs();
            SetupFighters();
            DoMath();
        }

        public void SetupMobs() {
            Mobs = new List<Mob>();
            var mobs = Math.Ceiling((RootInfo.BaseInfo.playerFighterData.dungeon_level + 1) / 50.0);
            for (int i = 0; i < mobs; i++) {
                Mobs.Add(new Mob(RootInfo.BaseInfo.playerFighterData.dungeon_level - (i * 25)));
            }
        }

        public void SetupFighters() {
            Fighters = new List<FighterInfo>();
            foreach (var fighter in RootInfo.BaseInfo.fighters) {
                Fighters.Add(new FighterInfo(fighter, RootInfo.BaseInfo.fighters.Count));
            }
        }

        public void DoMath() {
            double mobDamage = 0;
            double mobHp = 0;
            double fighterDamage = 0;
            double fighterHp = 0;
            foreach (var mob in Mobs) {
                //todo get hit and def from knight, if they don't use knight, get fucked
                double hitRate = mob.Hit / (mob.Hit + 7200.0);
                double baseDamage = (mob.Damage - 70);
                double afterToHit = baseDamage * hitRate;
                double afterCrit = (afterToHit * ((1 + mob.Crit) * .01)) + afterToHit;
                //assume priest for now
                if (mobDamage == 0) {
                    afterCrit = afterCrit / 1.1;
                }
                mobDamage += afterCrit;
                mobHp += mob.Hp;
            }
            foreach (var fighter in Fighters) {
                if (fighter.classType == "healer") {
                    mobDamage -= (long)Math.Floor(fighter.Damage * .75);
                }
                else if (fighter.classType == "knight") {
                    mobDamage = (long)Math.Floor(mobDamage * .6);
                    fighterHp += fighter.Hp;
                }
                else if (fighter.classType == "cavalry") {
                    double toHit = (fighter.Hit * 2.0) / ((fighter.Hit * 2) + Mobs.First().Dodge);
                    var damage = (fighter.Damage - Mobs.First().Def) * toHit;
                    fighterDamage += damage;
                }
            }
            RoundsToWin = mobHp / fighterDamage;
            RoundsToLose = fighterHp / mobDamage;
            WinChance = RoundsToLose / RoundsToWin;
        }
    }
}
