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

        public decimal ArticlesFraud { get; set; }
        public decimal ArticlesLost { get; set; }

        public int CustomerSatisfactionScore { get; set; }

        public void ClearScore()
        {
            ArticlesFraud = 0;
            ArticlesLost = 0;
            CustomerSatisfactionScore = 0;
        }

        public void Reset()
        {
            ClearScore();
            TotalSalary = 0;
        }

        public decimal CalculateSalary()
        {
            var salery = BaseSalery - ArticlesLost - ArticlesFraud - CustomerSatisfactionScore * 10;
            TotalSalary += salery;
            return salery;
        }
    }
}
