using System;
using Game.GameStatus;
using GameFrameWork;
using GameFrameWork.Network;
using Scr.Game.Network;


namespace Game
{
    public class HangGame : GameFrameWork.Game<GameFSM,GameStatus.GameData>
    {

        private FrameEventController _event;
        public FrameEventController EventSystem;

        public GameServer _Server;
        public GameClient _Client;

        public void CreateServer()
        {
            if (_Client == null && _fsm.GetCurType() == GameStatusEnum.Game_GamePlay)
                _Server = GameServer.CreaterServer(ConnectListener);
        }

        public void CloseServer()
        {
            GameServer.CloseServer();
        }

        public void CreateClient(string ip,int port)
        {
            if (_Server == null && _fsm.GetCurType() == GameStatusEnum.Game_GamePlay)
                _Client = GameClient.CreateClient(ip,port);
        }

        private void ConnectListener(NetworkWorker worker)
        {
            
        }
        public override void Start()
        {
            _fsm.ChangeState(GameStatusEnum.Game_Init );
        }
    }
}