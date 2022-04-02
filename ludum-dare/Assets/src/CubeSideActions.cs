using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schw3de.ld
{
    public class CubeSideActions
    {
        public Action<CubeSide> OnLeftClick { get; set; }
        public Action<CubeSide> OnRightClick { get; set; }
        public Action<CubeSide> OnCountdownChanged { get; set; }

        public CubeSideActions(Action<CubeSide> onLeftClick, Action<CubeSide> onRightClick, Action<CubeSide> onCountdownChanged)
        {
            OnLeftClick = onLeftClick;
            OnRightClick = onRightClick;
            OnCountdownChanged = onCountdownChanged;
        }
    }
}
