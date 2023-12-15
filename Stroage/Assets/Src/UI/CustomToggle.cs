using System;
using NaughtyAttributes;
using UI.Base;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class CustomToggle  : Base.PressComponentBase ,ICustomLoopComponent
    {
        public CustomToggleGroup Group;
        [SerializeField]
        protected bool _isOn = false;
        
        ICustomComponentStateChange _stateChange;
        
        private Action<bool> _onValueChanged;
        public Action<bool> OnValueChanged
        {
            get { return _onValueChanged; }
            set { _onValueChanged = value; }
        }

        protected override bool IsSelected => _isOn;

        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                if (_isOn != value)
                {
                    SetToggleState(value);
                    _onValueChanged?.Invoke(_isOn);
                }
            }
        }

        private void SetValue(bool isOn)
        {

        }
        
        

        #region MonoBehavior Function

        protected override void Awake()
        {
            base.Awake();
            //RegisterToggle();
            if (Group.IsMuliSelect)
            {
                SetToggleState(_isOn);
            }
        }

        private void OnEnable()
        {
            RegisterToggle();
        }
        
        private void OnDisable()
        {
            UnRegisterToggle();
        }
        
        private void OnDestroy()
        {
            //UnRegisterToggle();
        }

        #endregion
        
        
        private void RegisterToggle()
        {
            if (Group == null)
                return;
            Group.RegisterToggle(this);
        }
        
        private void UnRegisterToggle()
        {
            if (Group == null)
                return;
            Group.UnRegisterToggle(this);
        }

        internal void SetToggleState(bool on)
        {
            _isOn = on;
            SetState(_isOn ? UIComponentStates.selected : _isPointerEnter ? UIComponentStates.hightLight : UIComponentStates.normal);
            SetValue(_isOn);
        }

        protected override void OnClick()
        {
            if (!gameObject.activeInHierarchy)
                return;
            if (Group.IsMuliSelect)
            {
                IsOn = !IsOn;
            }
            else
            {
                if (IsOn)
                    return;
                IsOn = true;
                Group.SetToggle(this);
            }
        }

        private int _index = 0;
        public int GetIndex()
        {
            return _index;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
    }
}