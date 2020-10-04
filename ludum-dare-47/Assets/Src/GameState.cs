using System;
using UnityEngine;

namespace schw3de.ld47
{
    public class GameState : Singleton<GameState>
    {

        public decimal BaseSalery = 30;

        static GameState()
        {
            _dontDestroyOnLoad = true;
        }

        public LevelData CurrentLevel { get; set; }

        public decimal TotalSalary { get; set; }

        public decimal ArticlesScore { get; set; }

        public int CustomerSatisfactionScore { get; set; }

        public void ClearScore()
        {
            ArticlesScore = 0;
            CustomerSatisfactionScore = 0;
        }

        public decimal CalculateSalary()
        {
            var salery = BaseSalery - Math.Abs(ArticlesScore) * Math.Abs(CustomerSatisfactionScore);
            TotalSalary += salery;
            return salery;
        }
    }
}
