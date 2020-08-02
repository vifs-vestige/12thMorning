using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Internal;

namespace _12thMorning.Services {
    public class QueslarService {

        public void test() {

        }
    }

    public enum ResTypes {
        Meat=1, Iron=2, Wood=3, Stone=4
    }


    public class RootWrapper {
        public Root BaseInfo;
        public Partners PartnerInfo;
        public bool Vip;

        public RootWrapper(Root root) {
            BaseInfo = root;
            Vip = root.player.vip_time != null;
            PartnerInfo = new Partners(this);
            foreach(var partner in PartnerInfo.PartnerInfos.Values) {
                partner.UpdateBoosts();
            }
        }
    }

    public class Partners {
        public Dictionary<int, PartnerInfo> PartnerInfos = new Dictionary<int, PartnerInfo>();
        public int CurrentPrice;
        public int CurrentResHour;
        public int NewPrice;
        public int NewResHour;
        public int Tax = 0;
        private RootWrapper RootInfo;

        public Partners(RootWrapper info) {
            RootInfo = info;
            foreach (var partner in info.BaseInfo.partners) {
                PartnerInfos[partner.id] = new PartnerInfo(partner, info);
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
        public int ResHour;
        public string Display = "res";
        public PartnerTotal Current = new PartnerTotal();
        public PartnerTotal New = new PartnerTotal();
        private RootWrapper RootInfo;

        public PartnerInfo(Partner baseInfo, RootWrapper info) {
            RootInfo = info;
            BaseInfo = baseInfo;
            setType((ResTypes)baseInfo.action_id);
        }

        public void UpdateBoosts() {
            var vipBonus = RootInfo.Vip ? 1.10 : 1;
            Res = ((1 + ((Boost * .025) + HouseBoost + (Level / 100.0)) / 100.0) * (1 + (RootInfo.BaseInfo.village.boosts.mill) / 100.0) * (vipBonus)) * New.ResPre;
            if (RootInfo.PartnerInfo.Tax != 0) {
                Taxed = (int)Math.Floor(Res * (RootInfo.PartnerInfo.Tax / 100.0));
            }
            ResPostTax = (int) Math.Round(Res) - Taxed;
            ResHour = (int) Math.Floor((3600.0 / New.Seconds) * ResPostTax);
        }

        public void getResBoost() {
            var boost = 0;
            switch (ResType) {
                case ResTypes.Meat:
                    boost = RootInfo.BaseInfo.boosts.hunting;
                    HouseBoost = RootInfo.BaseInfo.house.pitchfork;
                    break;
                case ResTypes.Iron:
                    boost = RootInfo.BaseInfo.boosts.mining;
                    HouseBoost = RootInfo.BaseInfo.house.fountain;
                    break;
                case ResTypes.Wood:
                    boost = RootInfo.BaseInfo.boosts.woodcutting;
                    HouseBoost = RootInfo.BaseInfo.house.shed;
                    break;
                case ResTypes.Stone:
                    boost = RootInfo.BaseInfo.boosts.stonecarving;
                    HouseBoost = RootInfo.BaseInfo.house.tools;
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
        public int Seconds;
        public int Intelligence;
        public double IntPercent;
        public int TotalStats;
        public int ResPre;
        public int ResPerHourPre;
        public int PlayerStat;
        public int PartnerStat;
        public int TotalSpent;
        
        public void update() {
            Seconds = (int)Math.Ceiling(6.0 / (0.1 + Speed / (Speed + 2500.0)));
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

    public class Player {
        public string title { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public int access_level { get; set; }
        public int guest_account { get; set; }
        public int village_id { get; set; }
        public int party_id { get; set; }
        public DateTime vip_time { get; set; }
        public int toggle_vip_icon { get; set; }
        public DateTime qol_time { get; set; }
        public int toggle_qol_icon { get; set; }
        public int qol_destroy_item_toggle { get; set; }
        public string qol_destroy_item_rarity { get; set; }
    }

    public class Currency {
        public int id { get; set; }
        public int player_id { get; set; }
        public int gold { get; set; }
        public int credits { get; set; }
        public int relics { get; set; }
        public int meat { get; set; }
        public int iron { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int normal_wootz { get; set; }
        public int epic_wootz { get; set; }
        public int rare_wootz { get; set; }
        public int magical_wootz { get; set; }
        public int unique_wootz { get; set; }
        public int relic_wootz { get; set; }
        public int frog_toe { get; set; }
        public int dog_tongue { get; set; }
        public int lizard_leg { get; set; }
        public int wolf_tooth { get; set; }
        public int shattered_partner_gold { get; set; }
        public int shattered_fighter_gold { get; set; }
        public int shattered_battling_relics { get; set; }
        public int shattered_crafting_relics { get; set; }
        public int shattered_enchanting_relics { get; set; }
        public int shattered_partner_relics { get; set; }
        public int voting_points { get; set; }
    }

    public class House {
        public int id { get; set; }
        public int player_id { get; set; }
        public int chairs { get; set; }
        public int stove { get; set; }
        public int sink { get; set; }
        public int basket { get; set; }
        public int table { get; set; }
        public int couch { get; set; }
        public int carpet { get; set; }
        public int candlestick { get; set; }
        public int pitchfork { get; set; }
        public int shed { get; set; }
        public int fountain { get; set; }
        public int tools { get; set; }
        public int bed { get; set; }
        public int closet { get; set; }
        public int nightstand { get; set; }
        public int window { get; set; }
        public string current_building { get; set; }
        public DateTime building_timer { get; set; }
    }

    public class Skills {
        public int id { get; set; }
        public int player_id { get; set; }
        public int battling { get; set; }
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
        public int crafting { get; set; }
        public int enchanting { get; set; }
        public int battling_experience { get; set; }
        public int hunting_experience { get; set; }
        public int mining_experience { get; set; }
        public int woodcutting_experience { get; set; }
        public int stonecarving_experience { get; set; }
        public int crafting_experience { get; set; }
        public int enchanting_experience { get; set; }
    }

    public class Boosts {
        public int id { get; set; }
        public int player_id { get; set; }
        public int critChance { get; set; }
        public int critDamage { get; set; }
        public int multistrike { get; set; }
        public int healing { get; set; }
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
        public int luck { get; set; }
        public int power { get; set; }
        public int crafting { get; set; }
        public int consistency { get; set; }
        public int fortune { get; set; }
        public int capacity { get; set; }
        public int enchanting { get; set; }
        public int stability { get; set; }
        public int battle { get; set; }
        public int harvest { get; set; }
        public int craft { get; set; }
        public int enchant { get; set; }
    }

    public class Partner {
        public int id { get; set; }
        public int player_id { get; set; }
        public int action_id { get; set; }
        public string harvest_area { get; set; }
        public string name { get; set; }
        public int partner_id { get; set; }
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
        public int battling_experience { get; set; }
        public int hunting_experience { get; set; }
        public int mining_experience { get; set; }
        public int woodcutting_experience { get; set; }
        public int stonecarving_experience { get; set; }
        public int intelligence { get; set; }
        public int speed { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
    }

    public class Stats {
        public int id { get; set; }
        public int player_id { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
    }

    public class EquipmentEquipped {
        public int id { get; set; }
        public int player_id { get; set; }
        public string action_type { get; set; }
        public string item_type { get; set; }
        public string item_rarity { get; set; }
        public int stat_slots { get; set; }
        public int level_requirement { get; set; }
        public int damage { get; set; }
        public int defense { get; set; }
        public int strength { get; set; }
        public int health { get; set; }
        public int agility { get; set; }
        public int dexterity { get; set; }
        public int owned_by_guild { get; set; }
        public int guild_loaner_id { get; set; }
        public int crafted { get; set; }
        public int is_crafting { get; set; }
        public int crafted_actions_required { get; set; }
        public int crafted_actions_done { get; set; }
        public int equipped { get; set; }
        public int on_market { get; set; }
        public string name { get; set; }
        public int gear_set { get; set; }
        public int in_queue { get; set; }
        public int item_shop { get; set; }
        public string enchant_type { get; set; }
        public int total_stats { get; set; }
        public int? enchant_rarity { get; set; }
        public int? enchant_value { get; set; }
        public int type { get; set; }
        public int? enchant_level { get; set; }
    }

    public class EquipmentSlots {
        public int id { get; set; }
        public int player_id { get; set; }
        public int left_hand_level { get; set; }
        public int right_hand_level { get; set; }
        public int head_level { get; set; }
        public int body_level { get; set; }
        public int hands_level { get; set; }
        public int legs_level { get; set; }
        public int feet_level { get; set; }
    }

    public class Fighter {
        public int id { get; set; }
        public string name { get; set; }
        public int player_id { get; set; }
        public int row_placement { get; set; }
        public int column_placement { get; set; }
        [JsonPropertyName("class")]
        public string aaa { get; set; }
        public int fighter_id { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public int hit { get; set; }
        public int dodge { get; set; }
        public int defense { get; set; }
        public int crit_damage { get; set; }
    }

    public class PartyActions {
        public int id { get; set; }
        public int player_id { get; set; }
        public int party_id { get; set; }
        public int daily_actions_remaining { get; set; }
        public int daily_actions_max { get; set; }
        public int current_monster { get; set; }
        public int current_area { get; set; }
    }

    public class Actions {
        public int id { get; set; }
        public int player_id { get; set; }
        public int actions_remaining { get; set; }
        public string action_type { get; set; }
        public int default_actions { get; set; }
        public int monster_id { get; set; }
        public string harvest_type { get; set; }
        public string harvest_area { get; set; }
        public int area { get; set; }
        public int last_harvest_income { get; set; }
        public int last_battling_income { get; set; }
        public int has_started_actions { get; set; }
        public int highest_monster { get; set; }
    }

    public class Boosts2 {
        public int id { get; set; }
        public int village_id { get; set; }
        public int mill { get; set; }
        public int market { get; set; }
        public int stable { get; set; }
        public int well { get; set; }
        public int mason { get; set; }
        public int church { get; set; }
        public int warehouse { get; set; }
        public int tavern { get; set; }
    }

    public class Strengths {
        public int id { get; set; }
        public int village_id { get; set; }
        public int brave { get; set; }
        public int noble { get; set; }
        public int wealthy { get; set; }
        public int bold { get; set; }
        public int loyal { get; set; }
        public int swift { get; set; }
    }

    public class Village {
        public Boosts2 boosts { get; set; }
        public Strengths strengths { get; set; }
    }

    public class Kingdom {
    }

    public class Overview {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string name { get; set; }
        public int strength_available { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public int buildings { get; set; }
        public int members { get; set; }
        public int kingdom_id { get; set; }
        public int active_quest { get; set; }
        public int daily_quest_refreshes_used { get; set; }
    }

    public class ReturnTypeData {
        public string type { get; set; }
        public int playerId { get; set; }
    }

    public class Root {
        public Player player { get; set; }
        public Currency currency { get; set; }
        public House house { get; set; }
        public Skills skills { get; set; }
        public Boosts boosts { get; set; }
        public List<Partner> partners { get; set; }
        public Stats stats { get; set; }
        public List<EquipmentEquipped> equipmentEquipped { get; set; }
        public EquipmentSlots equipmentSlots { get; set; }
        public List<Fighter> fighters { get; set; }
        public PartyActions partyActions { get; set; }
        public Actions actions { get; set; }
        public Village village { get; set; }
        public Kingdom kingdom { get; set; }
        public Overview overview { get; set; }
        public ReturnTypeData returnTypeData { get; set; }
    }
}
