namespace Game.GameStatus
{
    public class GamePlayStatus: GameFrameWork.IState<GameData>
    {
        public bool CanEnter(GameData data, params object[] args)
        {
            return true;
        }

        public void Enter(GameData data, params object[] args)
        {
            //SceneCreater
        }

        public void Update(GameData data)
        {
        }

        public bool CanRelease(GameData data, params object[] args)
        {
            return true;
        }

        public void Release(GameData data, params object[] args)
        {
        }
    }
}