using CodedInputStream = Google.Protobuf.CodedInputStream;

namespace GameFrameWork.Network.MessageBase
{
    public abstract class MessageProcessor<T> : IMessageProcessor<T> where T : new()
    {
        //public abstract T CreateMessage(MessageBase msgBase);
        public abstract void Process(T message );
    }
}