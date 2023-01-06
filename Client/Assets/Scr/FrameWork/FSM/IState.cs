namespace GameFrameWork
{
    /// <summary>
    /// ״̬��״̬�Ľӿ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState
    {
        /// <summary>
        /// �Ƿ���Խ���״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool CanEnter(params object[] args);
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        void Enter(params object[] args);
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="data"></param>
        void Update();
        /// <summary>
        /// �Ƿ�����ͷ�״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool CanRelease(params object[] args);
        /// <summary>
        /// �ͷ�״̬
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        void Release(params object[] args);
    }
}


