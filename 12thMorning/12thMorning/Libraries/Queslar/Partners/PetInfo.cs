using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PetInfo {
        public ResTypes ResType;
        public int Tier;
        public long PetFood;
        public long PetFoodPerHour;
        public string Name;

        private Pet _Pet;

        public PetInfo(Pet pet) {
            UpdateSource(pet);
            Update();
        }

        public void UpdateSource(Pet pet) {
            _Pet = pet;
            Tier = pet.efficiency_tier;
            Name = pet.name;
            ResType = (ResTypes)Enum.Parse(typeof(ResTypes), pet.active_food);
        }

        public void SetResType(ResTypes res) {
            ResType = res;
        }

        public void Update() {
            PetFood = QueslarHelper.GetBoostedBoost(Tier, 10) + 1;
            PetFoodPerHour = PetFood * 600;
        }


    }
}
