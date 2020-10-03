using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace schw3de.ld47
{
    public class CashierRegister : Singleton<CashierRegister>
    {
        [SerializeField]
        private TextMeshProUGUI _articlesText;

        List<string> _articles = new List<string>();

        public void AddArticle(Article article)
        {
            _articlesText.text += article + Environment.NewLine;
        }
    }
}
