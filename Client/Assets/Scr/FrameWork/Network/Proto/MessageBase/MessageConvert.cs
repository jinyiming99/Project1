using System.Collections.Generic;

namespace GameFrameWork.Network.MessageBase
{

    /// <summary>
    /// 消息处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MessageDistribute :IMessageDistribute
    {
        protected static Dictionary<int, IMessageProcessor> m_createrDic = new Dictionary<int, IMessageProcessor>();
        
        public abstract void CreateMessageDic();

        public bool Find(int index,out IMessageProcessor creater)
        {
            return m_createrDic.TryGetValue(index, out creater);
        }


        // public MessageType ConvertToMessage<MessageType>(DataSegment data)
        // {
        //     data.
        // }
    }
}