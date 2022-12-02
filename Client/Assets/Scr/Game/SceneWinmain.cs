using System;
using Game;
using GameFrameWork;
using GameFrameWork.DebugTools;
using GameFrameWork.Network;
using GameFrameWork.Network.Client;
using Scr.Game;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SceneWinmain : MonoBehaviour
{    

    private void Awake()
    {
        FrameWork.CreateFrameWork<HangGame>(new FrameWorkConfig()).Init();





    }
}
