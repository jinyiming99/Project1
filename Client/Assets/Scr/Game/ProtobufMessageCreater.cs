using GameFrameWork.Network.MessageBase;
using Google.Protobuf;

namespace Scr.Game
{
    public class ProtobufMessageCreater<T> : IMessageCreater<T> where T : Google.Protobuf.IMessage,new()
    {
        private CodedInputStream stream;
        public T CreateMessage(MessageBase msgBase)
        {
            T t = new T();
            stream = new CodedInputStream(msgBase.m_data);
            t.MergeFrom(stream);
            return t;
        }
    }
}