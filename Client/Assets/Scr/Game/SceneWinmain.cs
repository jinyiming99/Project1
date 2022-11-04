using System;
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
        FrameWorkManagers.Init();
        FrameWorkManagers.InitNetWork( ConnectProtoType.TCP,NetworkManagerWorkType.Client);

        try
        {
            TcpClient.ConnectServer("127.0.0.1",8080);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        
    }
}
