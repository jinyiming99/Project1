using System;
using Game.GameStatus;
using GameFrameWork;
using GameFrameWork.Network;
using Game.Network;


namespace Game
{
    public class HangGame : GameFrameWork.Game<GameFSM,GameStatus.GameData>
    {
        private FrameEventController _event;
        public FrameEventController EventSystem;

        private GameNetwork _gameNetwork = new GameNetwork();
        public GameNetwork gameNetwork => _gameNetwork;

        public HangGame()
        {
            
        }
        public override void Start()
        {
            _fsm.ChangeState(GameStatusEnum.Game_Init );
        }
    }
}