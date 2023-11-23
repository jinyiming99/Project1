using UI;
using UnityEngine;

namespace Src
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        CustomButton _customButton;
        [SerializeField]
        CustomButton _customButton2;

        private bool _enable1 = true;
        void Start()
        {
            _customButton.ClickAction=() =>
            {
                Debug.Log("Click");
            };

            _customButton2.ClickAction = () =>
            {
                _enable1 = !_enable1;
                _customButton.IsWorking = _enable1;
            };
        }
    }
}