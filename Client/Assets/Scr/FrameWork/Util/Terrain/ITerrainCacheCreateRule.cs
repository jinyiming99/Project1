namespace GameFrameWork
{
    public interface ITerrainCacheCreateRule<T> where T :ITerrainCachaData
    {
        void Parse(ref T data);
    }
}