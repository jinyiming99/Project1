using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class TestNode : MonoBehaviour, ICustomLoopNode<int>
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void SetData(int data)
        {
            Debug.Log($"data = {data}");
            _text.text = data.ToString();
        }
    }
}