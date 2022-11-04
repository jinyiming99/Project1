namespace Game.World.Scene
{
    public class SceneCreater
    {
        public static GameScene CreateScene()
        {
            GameScene scene = new GameScene();
            scene.Create();
            return scene;
        }
    }
}