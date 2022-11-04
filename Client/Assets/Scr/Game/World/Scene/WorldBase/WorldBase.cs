namespace Game.World
{
    public abstract class WorldBase
    {
        public abstract void CreateWorld(WorldCreateData data);
        public abstract void ReleaseWorld();
    }
}