namespace GameFrameWork.Network.MessageBase
{
    /// <summary>
    /// 消息处理者
    /// </summary>
    public interface IMessageProcessor<T>
    {
        //T CreateMessage(MessageBase segment);
        void Process(T message);
    }
}