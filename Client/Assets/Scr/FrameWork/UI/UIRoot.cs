using System;
using UnityEngine;

namespace GameFrameWork.UI
{
    public class UIRoot : MonoBehaviour
    {
        public static UIRoot root;

        private void Awake()
        {
            root = this;
        }

        private void OnDestroy()
        {
            root = null;
        }
    }
}