using UnityEngine;

namespace MegaSkill.Main
{
    public static class ScoreCalculator
    {
        public static int Calculate() {
            return Mathf.RoundToInt(10000f * DataManager.main.successCount /
                                    (1 + FinishMenuManager.duration + FinishMenuManager.attempts +
                                     DataManager.main.failCount));
        }
    }
}
