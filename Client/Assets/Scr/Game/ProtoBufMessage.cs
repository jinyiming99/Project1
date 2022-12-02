using Google.Protobuf;

namespace GameFrameWork.Network.MessageBase
{
    public class ProtoBufMessageDistribute : MessageDistribute<Google.Protobuf.IMessage>
    {
        public void MessageBaseConvertMessageProcessor(MessageBase msgData)
        {
            if (Find(msgData.m_messageID, out var creater))
            {
                // var msg = creater.CreateMessage(msgData);
                // creater.Process(msg);
            }
        }
        // public static MessageBase ConvertProtoBuf2Message(short e,Google.Protobuf.IMessage message)
        // {
        //     MessageBase msg = new MessageBase();
        //     var arr = message.ToByteArray();
        //     msg.SetData(arr,arr.Length);
        //     msg.SetMessageID(e);
        //     msg.m_cmd = (short)MessageCommand.ProtoBuf_Message;
        //     return msg;
        // }
        public override void CreateMessageDic()
        {
            
        }
    }
}