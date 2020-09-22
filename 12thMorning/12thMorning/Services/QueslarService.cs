using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Internal;
using _12thMorning.Models.Queslar.Player;
using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using System.Security.Principal;
using _12thMorning.Models.Queslar;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;

namespace _12thMorning.Services {
    public class QueslarService {
        public string ApiKey = "";
        public bool Valid = false;
        public Dictionary<Type, BaseQueslar> Values = new Dictionary<Type, BaseQueslar>();
        private FullWrapper FullWrapper;
        public string LocalApiKey { get { return "Queslar.ApiKey"; } }

        public void Unload() {
            ApiKey = "";
            Valid = false;
            FullWrapper = null;
            Values = new Dictionary<Type, BaseQueslar>();
        }

        public FullWrapper GetFullWrapper() {
            if(FullWrapper != null) {
                return FullWrapper;
            }
            if(Values[typeof(Full)] != null) {
                FullWrapper = new FullWrapper((Full)Values[typeof(Full)]);
                return FullWrapper;
            } else {
            }
            return null;
        }

        public async Task<T> Update<T>() where T: BaseQueslar, new() {
            return await Test<T>(ApiKey);
        }

        public async Task<T> Test<T>(string apiKey) where T : BaseQueslar, new() {
            var client = new HttpClient();
            BaseQueslar info = new T();
            apiKey = apiKey.Trim();
            try {
                info = await client.GetFromJsonAsync<T>("https://queslar.com/api/" + info.ApiPath + apiKey);
                Values[typeof(T)] = info;
                if(info.GetType() == typeof(Full)) {
                    FullWrapper = new FullWrapper((Full)info);
                }
            }
            catch (Exception e) {
                return null;
            }
            Valid = true;
            ApiKey = apiKey;
            return (T)info;
        }

        public bool Parse<T>(string key, string data) where T: BaseQueslar, new() {
            try {
                BaseQueslar info = JsonSerializer.Deserialize<T>(data);
                Values[typeof(T)] = info;
                if (info.GetType() == typeof(Full)) {
                    FullWrapper = new FullWrapper((Full)info);
                }
                ApiKey = key;
                Valid = true;
                return true;
            } catch(Exception e) {
                return false;
            }
        }
    }

    public enum ResTypes {
        Meat=1, Iron=2, Wood=3, Stone=4
    }

    public class FullWrapper {
        public Full BaseInfo;
        public Partners PartnerInfo;
        public FighterWrapper FighterInfo;
        public bool Vip;

        public FullWrapper(Full root) {
            BaseInfo = root;
            Vip = false;
            if(root.player.vip_time != "0000-00-00 00:00:00") {
                var viptime = DateTime.Parse(root.player.vip_time);
                if(viptime > DateTime.Now) {
                    Vip = true;
                }
            }
            PartnerInfo = new Partners(this);
            foreach(var partner in PartnerInfo.PartnerInfos.Values) {
                partner.UpdateBoosts();
            }
            //FighterInfo = new FighterWrapper(this);
        }
    }

    #region fighters

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
            var mobs = Math.Ceiling((RootInfo.BaseInfo.playerFighterData.dungeon_level + 1 ) / 50.0);
            for (int i = 0; i < mobs; i++) {
                Mobs.Add(new Mob(RootInfo.BaseInfo.playerFighterData.dungeon_level - (i * 25)));
            }
        }

        public void SetupFighters() {
            Fighters = new List<FighterInfo>();
            foreach(var fighter in RootInfo.BaseInfo.fighters) {
                Fighters.Add(new FighterInfo(fighter, RootInfo.BaseInfo.fighters.Count));
            }
        }

        public void DoMath() {
            double mobDamage = 0;
            double mobHp = 0;
            double fighterDamage = 0;
            double fighterHp = 0;
            foreach(var mob in Mobs) {
                //todo get hit and def from knight, if they don't use knight, get fucked
                double hitRate = mob.Hit / (mob.Hit + 7200.0);
                double baseDamage = (mob.Damage - 70);
                double afterToHit = baseDamage * hitRate;
                double afterCrit = (afterToHit * ((1+mob.Crit)*.01)) + afterToHit;
                //assume priest for now
                if(mobDamage == 0) {
                    afterCrit = afterCrit / 1.1;
                }
                mobDamage += afterCrit;
                mobHp += mob.Hp;
            }
            foreach(var fighter in Fighters) {
                if(fighter.classType == "healer") {
                    mobDamage -= (int) Math.Floor(fighter.Damage * .75);
                } else if(fighter.classType == "knight") {
                    mobDamage = (int)Math.Floor(mobDamage * .6);
                    fighterHp += fighter.Hp;
                } else if(fighter.classType == "cavalry") {
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
            Hp = (int) Math.Floor((fighter.health * 100 + 500) * (1 + numberOfFighters * .25));
            Def = (int) Math.Floor((fighter.defense * 10 + 25) * (1 + numberOfFighters * .25));
            Damage = (int) Math.Floor((fighter.damage * 25 + 100) * (1 + numberOfFighters * .25));
            Crit = (int) Math.Floor((fighter.crit_damage * .25) * (1 + numberOfFighters * .25));
            Hit = (int) Math.Floor((fighter.hit * 50 + 50) * (1 + numberOfFighters * .25));
            Dodge = (int) Math.Floor((fighter.dodge * 10 + 50) * (1 + numberOfFighters * .25));
        }

    }

    #endregion

    #region partners

    public class Partners {
        public Dictionary<int, PartnerInfo> PartnerInfos = new Dictionary<int, PartnerInfo>();
        public int CurrentPrice;
        public int CurrentResHour;
        public int NewPrice;
        public int NewResHour;
        public int Tax = 0;
        private FullWrapper RootInfo;
        public int KingdomBonus;

        public Partners(FullWrapper info) {
            RootInfo = info;
            foreach (var partner in info.BaseInfo.partners) {
                PartnerInfos[partner.id] = new PartnerInfo(partner, info);
            }
            if (RootInfo.BaseInfo.kingdom.tiles != null) {
                foreach (var kingdomTiles in RootInfo.BaseInfo.kingdom.tiles) {
                    if (kingdomTiles.resource_one_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_one_value;
                    }
                    if (kingdomTiles.resource_two_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_two_value;
                    }
                    if (kingdomTiles.resource_three_type == "resource") {
                        KingdomBonus += (int)kingdomTiles.resource_three_value;
                    }
                }
            }
            UpdateTotals();
        }

        public void SetRes(int id, ResTypes res) {
            PartnerInfos[id].setType(res);
            UpdateTotals();
        }

        public void updateNew() {
            foreach (var partner in PartnerInfos.Values) {
                partner.New.update();
            }
            UpdateTotals();
            UpdateBoosts();
        }
        
        public void UpdateTotals() {
            NewPrice = 0;
            NewResHour = 0;
            CurrentPrice = 0;
            CurrentResHour = 0;
            foreach (var partner in PartnerInfos.Values) {
                NewPrice += partner.New.TotalSpent;
                NewResHour += partner.New.ResPerHourPre;
                CurrentPrice += partner.Current.TotalSpent;
                CurrentResHour += partner.Current.ResPerHourPre;
            }
        }

        public void UpdateBoosts() {
            foreach (var partner in PartnerInfos.Values) {
                partner.UpdateBoosts();
            }
        }
    }

    public class PartnerInfo {
        public ResTypes ResType;
        public Partner BaseInfo;
        public int Level;
        public int Stats;
        public int PlayerStat;
        public int TotalStat;
        public double Res;
        public int Taxed;
        public int ResPostTax;
        public double Boost;
        public int HouseBoost;
        public double EnchantBoost;
        public int ResHour;
        public string Display = "res";
        public PartnerTotal Current = new PartnerTotal();
        public PartnerTotal New = new PartnerTotal();
        private FullWrapper RootInfo;

        public PartnerInfo(Partner baseInfo, FullWrapper info) {
            RootInfo = info;
            BaseInfo = baseInfo;
            setType((ResTypes)baseInfo.action_id);
            EnchantBoost = 0;
            foreach(var x in info.BaseInfo.equipmentEquipped) {
                var temp = ResType.ToString();
                if(x.enchant_type == ResType.ToString().ToLower()) {
                    //EnchantBoost += (double) (x.enchant_value / 2.0 / 100.0);
                    EnchantBoost += (double)(Math.Pow((double)x.enchant_value, 0.925) / 100);
                }
            }
        }

        public void UpdateBoosts() {
            var vipBonus = RootInfo.Vip ? 1.10 : 1;
            Res = ((1 + ((Boost * .025)+ EnchantBoost + HouseBoost + RootInfo.BaseInfo.village.boosts.mill + (Level / 100.0)) / 100.0) * (vipBonus) * (1+RootInfo.PartnerInfo.KingdomBonus/100.0)) * New.ResPre;
            if (RootInfo.PartnerInfo.Tax != 0) {
                Taxed = (int)Math.Floor(Res * (RootInfo.PartnerInfo.Tax / 100.0));
            }
            ResPostTax = (int) Math.Round(Res) - Taxed;
            ResHour = (int) Math.Floor((3600.0 / New.Seconds) * ResPostTax);
        }

        public void UpdateStats() {

            Current.update(this, BaseInfo.speed, BaseInfo.intelligence);
            New.update(this, BaseInfo.speed, BaseInfo.intelligence);
        }

        public void getResBoost() {
            var boost = 0;
            switch (ResType) {
                case ResTypes.Meat:
                    boost = RootInfo.BaseInfo.boosts.hunting_boost;
                    HouseBoost = RootInfo.BaseInfo.house.pitchfork;
                    break;
                case ResTypes.Iron:
                    boost = RootInfo.BaseInfo.boosts.mining_boost;
                    HouseBoost = RootInfo.BaseInfo.house.fountain;
                    break;
                case ResTypes.Wood:
                    boost = RootInfo.BaseInfo.boosts.woodcutting_boost;
                    HouseBoost = RootInfo.BaseInfo.house.tools;
                    break;
                case ResTypes.Stone:
                    boost = RootInfo.BaseInfo.boosts.stonecarving_boost;
                    HouseBoost = RootInfo.BaseInfo.house.shed;
                    break;
            }
            Boost = boost;
        }

        public void setType(ResTypes res) {
            ResType = res;
            Display = res.ToString();
            switch(res) {
                case ResTypes.Meat:
                    PlayerStat = RootInfo.BaseInfo.stats.strength;
                    Level = BaseInfo.hunting;
                    Stats = BaseInfo.strength;
                    break;
                case ResTypes.Iron:
                    PlayerStat = RootInfo.BaseInfo.stats.health;
                    Level = BaseInfo.mining;
                    Stats = BaseInfo.health;
                    break;
                case ResTypes.Wood:
                    PlayerStat = RootInfo.BaseInfo.stats.agility;
                    Level = BaseInfo.woodcutting;
                    Stats = BaseInfo.agility;
                    break;
                case ResTypes.Stone:
                    PlayerStat = RootInfo.BaseInfo.stats.dexterity;
                    Level = BaseInfo.stonecarving;
                    Stats = BaseInfo.dexterity;
                    break;
            }

            Current.update(this, BaseInfo.speed, BaseInfo.intelligence);
            New.update(this, BaseInfo.speed, BaseInfo.intelligence);
            getResBoost();


        }
    }

    public class PartnerTotal {
        public int Speed;
        public double Seconds;
        public int Intelligence;
        public double IntPercent;
        public int TotalStats;
        public int ResPre;
        public int ResPerHourPre;
        public int PlayerStat;
        public int PartnerStat;
        public int TotalSpent;
        
        public void update() {
            Seconds = Math.Truncate((6.0 / (0.1 + Speed / (Speed + 2500.0)))*100)/100;
            IntPercent = 20.0 + Intelligence / (Intelligence + 250.0) * 100.0;
            TotalStats = (int)Math.Round(IntPercent / 100.0 * PlayerStat) + PartnerStat;
            ResPre = ((int)Math.Floor(TotalStats / 100.0)) + 1;
            ResPerHourPre = (int)Math.Floor((3600.0 / Seconds) * ResPre);
            TotalSpent = (((Intelligence * (Intelligence + 1)) / 2) + ((Speed * (Speed + 1)) / 2)) * 10000;
        }

        public void update(PartnerInfo partnerInfo, int speed, int intelligence) {
            PlayerStat = partnerInfo.PlayerStat;
            PartnerStat = partnerInfo.Stats;
            Speed = speed;
            Intelligence = intelligence;
            update();
        }
    }

    #endregion

}
