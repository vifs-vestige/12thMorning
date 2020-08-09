using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar {
    public abstract class BaseQueslar {
        public abstract string ApiPath { get; }
        //public abstract string LocalKey { get; }
        public const string LocalKey = "";
        public int id { get; set; }
        public int player_id { get; set; }
        public ReturnTypeData returnTypeData { get; set; }
    }

    public class ReturnTypeData {
        public string type { get; set; }
        public int playerId { get; set; }
    }
}
