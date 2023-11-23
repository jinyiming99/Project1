using System;
using UI.Base;

namespace UI
{
    public class CustomToggle  : Base.PressComponentBase
    {
        public CustomToggleGroup Group;
        protected bool _isOn;
        
        private Action<bool> _onValueChanged;
        public Action<bool> OnValueChanged
        {
            get { return _onValueChanged; }
            set { _onValueChanged = value; }
        }
        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                if (_isOn != value)
                {
                    SetState(_isOn ? UIComponentStates.selected : _isPointerEnter ? UIComponentStates.hightLight : UIComponentStates.normal);
                    _isOn = value;
                    _onValueChanged?.Invoke(_isOn);
                }

            }
        }
        
        protected override void OnClick()
        {
            if (Group == null)
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
    }
}