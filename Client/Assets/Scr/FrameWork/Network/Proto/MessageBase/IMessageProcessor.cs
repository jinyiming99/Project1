namespace GameFrameWork.Network.MessageBase
{
    /// <summary>
    /// 消息处理者
    /// </summary>
    public interface IMessageProcessor
    {
        int MessageID { get; }
        void CreateMessage(MessageBase segment);

        void Sort();
        void Processor();

        void ReleaseMessage();
    }
}