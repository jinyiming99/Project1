namespace GameFrameWork.Network.MessageBase
{
    public interface IMessage
    {
        DataSegment GetData();

        bool TrySetData(DataSegment data);

        void ClearData();
    }
}