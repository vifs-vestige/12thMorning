using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12thMorning.Libraries.Queslar {
    public class QueslarHelper {

        public static int GetBoostedBoost(int level, int perLevel) {
            var levelMod = (int)Math.Floor(level / perLevel + 0.0);
            return levelMod * (levelMod + 1) / 2 * perLevel + ((level % perLevel) * (levelMod + 1));
        }
    }
}
