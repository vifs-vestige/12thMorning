using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using _12thMorning.Libraries.Queslar;
using _12thMorning.Models.Queslar;
using _12thMorning.Models.Queslar.Enchanting;
using _12thMorning.Models.Queslar.Player;

namespace _12thMorning.Services {
    public class QueslarService {
        public string ApiKey = "";
        public bool Valid = false;
        public Dictionary<Type, BaseQueslar> Values = new Dictionary<Type, BaseQueslar>();
        private FullWrapper FullWrapper;
        public int Tax = 0;
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
                FullWrapper.AddPartnerWrapper();
                return FullWrapper;
            } else {
            }
            return null;
        }

        public async Task<T> Update<T>() where T: BaseQueslar, new() {
            return await Update<T>(ApiKey);
        }

        public async Task<T> Update<T>(string apiKey) where T : BaseQueslar, new() {
            Valid = false;
            var client = new HttpClient();
            BaseQueslar info = new T();
            apiKey = apiKey.Trim();
            try {
                info = await client.GetFromJsonAsync<T>("https://queslar.com/api/" + info.ApiPath + apiKey);
                Values[typeof(T)] = info;
                if(info.GetType() == typeof(Full)) {
                    FullWrapper = new FullWrapper((Full)info);
                    FullWrapper.AddPartnerWrapper(Tax);
                }
            }
            catch (Exception e) { 
                return null;
            }
            Valid = true;
            ApiKey = apiKey;
            return (T)info;
        }

        public int UpdateTax(string tax) {
            if (int.TryParse(tax, out Tax)) {
                return Tax;
            } else {
                return 0;
            }
        }

        public List<EnchantServiceInfo> CalcEnchantingInfo(string avg, string level, string cost) {
            if(cost == null || cost.Trim() == "") {
                cost = "0";
            }
            return EnchantServiceInfo.Build(double.Parse(avg), int.Parse(level), int.Parse(cost));
        }
    }


}
