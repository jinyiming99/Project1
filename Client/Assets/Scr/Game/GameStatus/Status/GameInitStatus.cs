namespace Game.GameStatus
{
    public class GameInitStatus : GameFrameWork.IState<GameData>
    {
        public bool CanEnter(GameData data, params object[] args)
        {
            return true;
        }

        public void Enter(GameData data, params object[] args)
        {
            
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