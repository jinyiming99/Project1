using System;
using System.Collections.Generic;
using UI;
using UnityEngine;


public class Test2 : MonoBehaviour
{
    [SerializeField]
    private CustomLoopView _view;

    
    private void Start()
    {
        List<int> _data = new List<int>();
        for (int i = 0 ; i < 103; ++i)
        {
            _data.Add(i);
        }
        _view.SetData<int>(_data);
    }
}
