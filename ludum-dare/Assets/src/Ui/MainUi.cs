using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace schw3de.ld.Ui
{
    public class MainUi : Singleton<MainUi>
    {
        public TextMeshProUGUI SurviedText;
        private Canvas _canvas;
        private UiActions _uiActions;

        private new void Awake()
        {
            base.Awake();
            _canvas = GetComponent<Canvas>();
            _canvas.gameObject.SetActive(false);
        }

        public void Init(UiActions uiActions)
        {
            _uiActions = uiActions;
        }

        public void SetSurvived(string survived)
        {
            SurviedText.text = survived;
        }
    }
}
