namespace GameFrameWork
{
    /// <summary>
    /// ״̬��״̬�Ľӿ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T>
    {
        /// <summary>
        /// �Ƿ���Խ���״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool CanEnter(T data, params object[] args);
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        void Enter(T data, params object[] args);
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="data"></param>
        void Update(T data);
        /// <summary>
        /// �Ƿ�����ͷ�״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool CanRelease(T data, params object[] args);
        /// <summary>
        /// �ͷ�״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        void Release(T data, params object[] args);
    }
}


