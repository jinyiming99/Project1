using UnityEngine;

namespace GameFrameWork.UI
{
    public class UIControllerBase<T> : GameObjectUnit where T : UIViewBase
    {
        private T view;

        public override void Show()
        {
            view.Show();
        }

        public override void Hide()
        {
            view.Hide();
        }
    }
}