using CodedInputStream = Google.Protobuf.CodedInputStream;

namespace GameFrameWork.Network.MessageBase
{
    public abstract class MessageProcessor<T> : IMessageCreater where T : Google.Protobuf.IMessage,new()
    {
        protected T m_message;

        protected static int m_messageID;

        public void CreateMessage(DataSegment segment)
        {
            m_message = new T();
            m_message.MergeFrom(new CodedInputStream(segment.m_data));
        }

        public abstract void Process();
    }
}