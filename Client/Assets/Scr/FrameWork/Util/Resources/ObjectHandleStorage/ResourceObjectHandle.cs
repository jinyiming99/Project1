namespace GameFrameWork
{
    public class ResourceObjectHandle<T> : IResourceHandle where T :UnityEngine.Object
    {
        private T m_data;
        public T Data => m_data;

        private int m_count;

        public int Count => m_count;

        public ResourceObjectHandle(T data)
        {
            m_data = data;
            m_count = 0;
        }
    }
}