using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Models.Queslar.AbstractModels {
    public interface IPartners {
        public int hunting { get; set; }
        public int mining { get; set; }
        public int woodcutting { get; set; }
        public int stonecarving { get; set; }
    }
}
