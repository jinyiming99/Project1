using System;
using Game.GameStatus;
using GameFrameWork;
using GameFrameWork.Network;
using GameFrameWork.Network.Server;

namespace Game
{
    public class HangGame : GameFrameWork.Game<GameFSM>
    {
        //private GameFrameWork.Network.Client.TcpClient m_client;

        public override void Init()
        {
            base.Init();
        }

        public override void Release()
        {
            //m_client.Release();
        }

        private void Update()
        {
            m_fsm.Update(); 
        }
    }
}