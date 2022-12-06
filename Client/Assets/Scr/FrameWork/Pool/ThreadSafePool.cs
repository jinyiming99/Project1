using System.Collections.Concurrent;

namespace GameFrameWork.Pool
{
    public class ThreadSafePool<T> : IPool<T>
    {
        private ConcurrentStack<T> m_data;
        public int GetLength()
        {
            return m_data.Count;
        }

        public T Pop()
        {
            m_data.TryPop(out var t);
            return t;
        }

        public void Push(T t)
        {
            m_data.Push(t);
        }

        public void Release()
        {

        }
    }
}