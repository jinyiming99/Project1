namespace GameFrameWork
{
    public class GameFrameWork 
    {
        public static Game s_game = null;

        public static void CreateGame<T>() where T : Game
        {
            FrameWorkManagers.Init();
            s_game = GameObjectUnit.CreateNewGameObject<T>();
            s_game.Init();
        }
        
    }
}