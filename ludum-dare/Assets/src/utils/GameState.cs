using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld.utils
{
    public static class GameState
    {
        private const string Survived = "Survived";
        private const string LastSurvived = "LastSurvived";

        public static int GetSurvivedTotalSeconds()
            => PlayerPrefs.GetInt(Survived, -1);

        public static void SetSurvivedTotalSeconds(int totalSeconds)
            => PlayerPrefs.SetInt(Survived, totalSeconds);

        public static int GetLastSurvivedTotalSeconds()
            => PlayerPrefs.GetInt(LastSurvived, -1);

        public static void SetLastSurvivedTotalSeconds(int totalSeconds)
            => PlayerPrefs.SetInt(LastSurvived, totalSeconds);
    }
}
