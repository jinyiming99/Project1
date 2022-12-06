using System;
using UnityEngine;

namespace GameFrameWork.UI
{

    public abstract class UIControllerBase<T> :  GameObjectUnit ,IUIControllerInterface
                                                where T : UIViewBase  
    {
        private T _view;
        private bool _bShowing = false;

        [SerializeField] private UIPopTypeEnum _popType;

        protected abstract string UIResource { get; }

        protected void LoadResource()
        {
            FrameWork.GetFrameWork().Components.ResourceManager.LoadGameObject(UIResource,LoadOver);
        }

        protected virtual void LoadOver(GameObject go)
        {
            if (go == null)
                return;
            _view = go.GetComponent<T>();
            _view.transform.parent = m_transform;
            _view.transform.localPosition = Vector3.zero;
        }

        protected void ReleaseResource()
        {
            FrameWork.GetFrameWork().Components.ResourceManager.ReleaseAsset<T>(_view);
        }

        public virtual void CreatePanel()
        {
            LoadResource();
        }

        public virtual void ShowPanel(params object[] args)
        {
            _bShowing = true;
            Show();
            _view?.Show();
        }

        public virtual  void HidePanel()
        {
            _bShowing = false;
            _view?.Hide();
        }

        public virtual void ReleasePanel()
        {
            _bShowing = false;
            ReleaseResource();
        }

        public bool GetActive()
        {
            return _bShowing;
        }

        public UIPopTypeEnum GetPopType()
        {
            return _popType;
        }
    }
}