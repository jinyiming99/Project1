namespace GameFrameWork.Containers
{
    public abstract class SwapContainer<T> where T:class
    {
        
        protected T[]     m_lists = new T[2];
        /// <summary>
        /// 1线程添加删除操作的数据表
        /// </summary>
        protected T       m_workingDataContainer   = null;
        public T WorkingData => m_workingDataContainer;
        
        protected T       m_waitingDataContainer   = null;
        public T WaitingData => m_waitingDataContainer;

        public SwapContainer()
        {
            m_workingDataContainer = m_lists[0];
            m_waitingDataContainer = m_lists[1];
        }
        public virtual void Swap()
        {
            lock (m_lists)
            {
                var temp = m_workingDataContainer;
                m_workingDataContainer = m_waitingDataContainer;
                m_waitingDataContainer = temp;
            }
        }

        public abstract int WorkingCount();
        public abstract int WaitingCount();

        public virtual void Release()
        {
            m_waitingDataContainer = null;
            m_workingDataContainer = null;
        }
        
    }
}