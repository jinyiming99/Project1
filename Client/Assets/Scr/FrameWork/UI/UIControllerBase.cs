using System;
using UnityEngine;

namespace GameFrameWork.UI
{

    public abstract class UIControllerBase : GameObjectUnit ,IUIControllerInterface
    {

        private bool _bShowing = false;

        [SerializeField] protected UIPopTypeEnum _popType;

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        public virtual void ShowPanel(params object[] args)
        {
            Show();

        }

        public virtual void HidePanel()
        {
            Hide();

        }

        public virtual void ReleasePanel()
        {
            _bShowing = false;
        }
        

        public UIPopTypeEnum GetPopType()
        {
            return _popType;
        }
    }
}