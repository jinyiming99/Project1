using System.Collections.Generic;

namespace GameFrameWork.Network.MessageBase
{
    using Google.Protobuf;
    public abstract class MessageConvert<T> :IMessageConvert where T:IMessageCreater
    {
        private Dictionary<int, IMessageCreater> m_createrDic = new Dictionary<int, IMessageCreater>();
        
        public abstract void CreateMessageDic();

        public byte[] ConvertToBytes(Google.Protobuf.IMessage message)
        {
            var data = message.ToByteArray();
            return data;
        }

        // public MessageType ConvertToMessage<MessageType>(DataSegment data)
        // {
        //     data.
        // }
    }
}