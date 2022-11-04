using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFrameWork.SingleInstance;
using UnityEngine;

namespace GameFrameWork
{
    public class CoroutineLoader : MonoSingleInstanceDontDestroy<CoroutineLoader>
    {
        public static void Do(Func<IEnumerator> enumerator)
        {
            if (s_instance == null)
                return;
            s_instance.StartCoroutine(enumerator());
        }
        protected override void OnRelease()
        {
            StopAllCoroutines();
        }
    }
}
