namespace GameFrameWork.Network.MessageBase
{
    public interface IMessageCreater
    {
        void CreateMessage(DataSegment segment);
    }
}