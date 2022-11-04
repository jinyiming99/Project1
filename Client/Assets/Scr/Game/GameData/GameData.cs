namespace Game.GameData
{
    public class GameData
    {
        public string m_seedString;
        public long m_seed;

        public WorldData m_world;
        public GameData()
        {
            
        }

        public GameData(string seedString)
        {
            m_seedString = seedString;
            m_seed = m_seedString.GetHashCode();
        }
        public long GetSeed() => m_seed;
    }
}