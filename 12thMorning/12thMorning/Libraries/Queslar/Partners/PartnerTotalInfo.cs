using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using _12thMorning.Models.Queslar;

namespace _12thMorning.Libraries.Queslar.Partners {
    public class PartnerTotalInfo {
        public ResTypes ResType;
        public double Res;
        public long Taxed;
        public long Pets;


        private List<PartnerIncomeInfo> PartnersIncome;
        private List<PetInfo> PetsInfo;


        public PartnerTotalInfo(ResTypes res, List<PartnerIncomeInfo> partners, List<PetInfo> pets) {
            PartnersIncome = new List<PartnerIncomeInfo>();
            PetsInfo = new List<PetInfo>();
            ResType = res;

            foreach(var partner in partners) {
                if(partner.ResType == res) {
                    PartnersIncome.Add(partner);
                }
            }
            foreach(var pet in pets) {
                if(pet.ResType == res) {
                    PetsInfo.Add(pet);
                }
            }
        }

        public void update() {
            Res = 0;
            Taxed = 0;
            Pets = 0;
            foreach(var partner in PartnersIncome) {
                Res += partner.Res;
                Taxed += partner.Taxed;
            }
            foreach(var pet in PetsInfo) {
                Pets += pet.PetFoodPerHour;
            }
        }


    }
}
