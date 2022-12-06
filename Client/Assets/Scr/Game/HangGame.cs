using System;
using Game.GameStatus;
using GameFrameWork;
using GameFrameWork.Network;
using GameFrameWork.Network.Server;

namespace Game
{
    public class HangGame : GameFrameWork.Game
    {
        private GameFSM _fsm ;
        public override void Init()
        {
            _fsm = new GameFSM();
        }

        public override void Start()
        {
            _fsm.ChangeState(GameStatusEnum.Game_Init );
        }

        public override void Resume()
        {
            
        }

        public override void Release()
        {
            _fsm.Release();
        }

        public override void Update()
        {
            _fsm.Update();
        }

        public override void Pause()
        {
            
        }
    }
}