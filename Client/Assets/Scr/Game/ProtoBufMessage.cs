using Google.Protobuf;

namespace GameFrameWork.Network.MessageBase
{
    public static class ProtoBufMessage
    {
        public static MessageBase ConvertProtoBuf2Message(short e,Google.Protobuf.IMessage message)
        {
            MessageBase msg = new MessageBase();
            var arr = message.ToByteArray();
            msg.SetData(arr,arr.Length);
            msg.SetMessageID(e);
            msg.m_cmd = (short)MessageCommand.ProtoBuf_Message;
            return msg;
        }
    }
}