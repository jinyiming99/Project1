namespace GameFrameWork
{
    public interface ITerrainCreater<T> where T:ITerrain
    {
        T Create();
    }
}