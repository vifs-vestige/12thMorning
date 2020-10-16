using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.Wrappers.Partners {
    public class PartnerFinal {
        public long PetFood = 0;
        public long TaxedPerHour;
        public long PetFoodPerHour = 0;
        private long PrePetPerHour;
        public long FinalPerHour;

        public PartnerFinal(long petFood) {
            PetFood = petFood;
            updatePetPerHour();
        }

        public void clear() {
            TaxedPerHour = 0;
            PrePetPerHour = 0;
        }

        public void add(PartnerInfo partnerInfo) {
            TaxedPerHour += partnerInfo.TaxedHour;
            PrePetPerHour += partnerInfo.ResHour;

        }

        public void updatePetPerHour() {
            PetFoodPerHour = PetFood * 600;
            FinalPerHour = PrePetPerHour - PetFoodPerHour;
        }

        public void finalize() {
            FinalPerHour = PrePetPerHour - PetFoodPerHour;
        }
    }
}
