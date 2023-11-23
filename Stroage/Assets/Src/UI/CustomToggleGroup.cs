using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public sealed class CustomToggleGroup : MonoBehaviour
    {
        private CustomToggle[] _toggles;
        [SerializeField]
        private bool _isMuliSelect = false;
        public bool IsMuliSelect
        {
            get { return _isMuliSelect; }
            set { _isMuliSelect = value; }
        }
        
        internal void SetToggle(CustomToggle toggle)
        {
            if (_toggles == null)
                return;
            foreach (var t in _toggles)
            {
                if (t == toggle)
                    continue;
                t.IsOn = false;
            }
        }
        public List<CustomToggle> GetOnToggles()
        {
            if (_toggles == null)
                return null;
            var list = new System.Collections.Generic.List<CustomToggle>();
            foreach (var t in _toggles)
            {
                if (t.IsOn)
                    list.Add(t);
            }
            return list;
        }
    }
}