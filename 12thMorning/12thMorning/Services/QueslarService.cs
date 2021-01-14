using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using _12thMorning.Data;
using _12thMorning.Libraries.Queslar;
using _12thMorning.Libraries.Queslar.Enchanting;
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
        private _12thMorningContext DB { get { return new _12thMorningContext(); } }

        public void Unload() {
            ApiKey = "";
            Valid = false;
            FullWrapper = null;
            Values = new Dictionary<Type, BaseQueslar>();
        }

        public FullWrapper GetFullWrapper() {
            if (FullWrapper != null) {
                return FullWrapper;
            }

            if (Values.ContainsKey(typeof(Full)) && Values[typeof(Full)] != null) {
                FullWrapper = new FullWrapper((Full) Values[typeof(Full)]);
                FullWrapper.AddPartnerWrapper();
                return FullWrapper;
            } else { }

            return null;
        }

        public bool CheckIfInServer() {
            return DB.QueslarKeys.Where(x=>x.ApiKey == ApiKey || x.Username == FullWrapper.BaseInfo.player.username)
                     .Count() == 1;
        }

        public void AddToServer() {
            if (CheckIfInServer() == false) {
                var temp = new QueslarKeys();
                temp.ApiKey = ApiKey;
                temp.Data = System.Text.Json.JsonSerializer.Serialize(FullWrapper.BaseInfo);
                temp.DateUpdated = DateTime.Now;
                temp.Username = FullWrapper.BaseInfo.player.username;
                var db = DB;
                db.QueslarKeys.Add(temp);
                db.SaveChanges();
            }
        }

        public void RemoveFromServer() {
            if (CheckIfInServer() == true) {
                var temp = DB.QueslarKeys
                             .Where(x=>x.ApiKey == ApiKey || x.Username == FullWrapper.BaseInfo.player.username)
                             .First();
                var db = DB;
                db.Remove(temp);
                db.SaveChanges();
            }
        }

        public void UpdateOnServer() {
            if (CheckIfInServer() == true) {
                var temp = DB.QueslarKeys
                             .Where(x=>x.ApiKey == ApiKey || x.Username == FullWrapper.BaseInfo.player.username)
                             .First();
                temp.DateUpdated = DateTime.Now;
                temp.Data = System.Text.Json.JsonSerializer.Serialize(FullWrapper.BaseInfo);
                var db = DB;
                db.QueslarKeys.Update(temp);
                db.SaveChanges();
            }
        }

        public async Task<Double> GetPlayerForEnchantingService(string player) {
            var db = DB;
            var dbObjects = db.QueslarKeys.Where(x=>x.Username == player);
            Full info;
            if (dbObjects.Count() == 1) {
                var dbObject = dbObjects.First();
                if (dbObject.DateUpdated < DateTime.Now.AddHours(-1)) {
                    var client = new HttpClient();
                    info = await client.GetFromJsonAsync<Full>("https://queslar.com/api/player/full/" + dbObject.ApiKey
                    );
                    dbObject.Data = System.Text.Json.JsonSerializer.Serialize(info);
                    dbObject.DateUpdated = DateTime.Now;
                    db.QueslarKeys.Update(dbObject);
                    db.SaveChanges();
                } else {
                    info = System.Text.Json.JsonSerializer.Deserialize<Full>(dbObject.Data);
                }

                var wrapper = new FullWrapper(info);
                var enchantingAvg = new EnchantingAvg(wrapper);
                return enchantingAvg.CalculateAvg();
            }

            return 0.0;
        }

        public async Task<T> Update<T>() where T : BaseQueslar, new() { return await Update<T>(ApiKey); }

        public async Task<T> Update<T>(string apiKey) where T : BaseQueslar, new() {
            Valid = false;
            var client = new HttpClient();
            BaseQueslar info = new T();
            apiKey = apiKey.Trim();
            try {
                info = await client.GetFromJsonAsync<T>("https://queslar.com/api/" + info.ApiPath + apiKey);
                Values[typeof(T)] = info;
                if (info.GetType() == typeof(Full)) {
                    FullWrapper = new FullWrapper((Full) info);
                    FullWrapper.AddPartnerWrapper(Tax);
                }
            } catch (Exception e) {
                return null;
            }

            Valid = true;
            ApiKey = apiKey;
            return (T) info;
        }

        public int UpdateTax(string tax) {
            if (int.TryParse(tax, out Tax)) {
                return Tax;
            } else {
                return 0;
            }
        }

        public List<EnchantServiceInfo> CalcEnchantingInfo(string avg, string level, string cost) {
            if (cost == null || cost.Trim() == "") {
                cost = "0";
            }

            return EnchantServiceInfo.Build(double.Parse(avg), int.Parse(level), int.Parse(cost));
        }
    }
}