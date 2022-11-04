namespace GameFrameWork
{
    public interface IResourceStorage
    {
        T LoadResource<T>(ResourceRequirement requirement) where T : UnityEngine.Object;
        void LoadResourceAsync<T>(ResourceRequirement requirement,System.Action<T> action) where T : UnityEngine.Object;
    }
}