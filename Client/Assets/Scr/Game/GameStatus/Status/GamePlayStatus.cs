using Game.GameData;
using GameFrameWork;

namespace Game.GameStatus
{
    public class GamePlayStatus: GameFrameWork.IState
    {
        public bool CanEnter(params object[] args)
        {
            return true;
        }

        public void Enter(params object[] args)
        {
            var data = FrameWork.GetGame<HangGame>().gameData;
            data._gamePlayData = new GamePlayData();
            data._gamePlayData._mainCharacterData = new MainCharacterData();
            //SceneCreater
        }

        public void Update()
        {
        }

        public bool CanRelease(params object[] args)
        {
            return true;
        }

        public void Release(params object[] args)
        {
        }
    }
}