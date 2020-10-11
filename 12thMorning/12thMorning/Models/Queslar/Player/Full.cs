using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Player {

    public class Full : BaseQueslar {
        public override string ApiPath { get { return "player/full/"; } }
        //public override string LocalKey { get { return "Queslar.Players.Full"; } }
        public new const string LocalKey = "Queslar.Players.Full";
        public Player player { get; set; }
        public Currency currency { get; set; }
        public House house { get; set; }
        public Levels skills { get; set; }
        public Boosts boosts { get; set; }
        public List<Partner> partners { get; set; }
        public Stats stats { get; set; }
        public List<EquipmentEquipped> equipmentEquipped { get; set; }
        public EquipmentSlots equipmentSlots { get; set; }
        public List<Fighter> fighters { get; set; }
        public PlayerFighterData playerFighterData { get; set; }
        public Party partyActions { get; set; }
        public Actions actions { get; set; }
        public Village village { get; set; }
        public Kingdom kingdom { get; set; }
        public Overview overview { get; set; }
        public List<Pet> pets { get; set; }
    }

    public class Player {
        public string title { get; set; }
        public string username { get; set; }
        public int id { get; set; }
        public int access_level { get; set; }
        public int guest_account { get; set; }
        public int village_id { get; set; }
        public int party_id { get; set; }
        public string vip_time { get; set; }
        public int toggle_vip_icon { get; set; }
        public string qol_time { get; set; }
        public int toggle_qol_icon { get; set; }
        public int qol_destroy_item_toggle { get; set; }
        public string qol_destroy_item_rarity { get; set; }
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
        public string fighterClass { get; set; }
        public int fighter_id { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public int hit { get; set; }
        public int dodge { get; set; }
        public int defense { get; set; }
        public int crit_damage { get; set; }
    }

    public class PlayerFighterData {
        public int id { get; set; }
        public int player_id { get; set; }
        public int daily_attacks_used { get; set; }
        public int fighters { get; set; }
        public int elo { get; set; }
        public int dungeon_level { get; set; }
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
}
