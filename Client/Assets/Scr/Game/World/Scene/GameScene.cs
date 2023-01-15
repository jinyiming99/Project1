namespace Game.World.Scene
{
    public class GameScene
    {
        private World m_world;
        public void Create()
        {
            m_world = new World();
            m_world.LoadScene();
        }
    }
}